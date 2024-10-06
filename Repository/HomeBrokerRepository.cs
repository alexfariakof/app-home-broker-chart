using CsvHelper;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using HomeBroker.Domain.Charts.Agreggates.Factory;
using HtmlAgilityPack;
using Repository.Interfaces;
using System.Globalization;
using System.Text;

namespace Repository;

/// <summary>
/// Repositório responsável por obter dados históricos do Magazine Luiza.
/// </summary>
public class HomeBrokerRepository : IHomeBrokerRepository
{
    private readonly IMagazineLuizaHistoryPriceFactory _historyPriceFactory;

    /// <summary>
    /// Converte uma data para o formato de carimbo de data/hora Unix, utilizado pela página da Magazine Luiza.
    /// </summary>
    /// <param name="date">A data a ser convertida.</param>
    /// <returns>O carimbo de data/hora Unix correspondente à data fornecida.</returns>
    private static long ToUnixTimestamp(DateTime date)
    {
        return (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
    }

    /// <summary>
    /// Baixa o conteúdo de uma URL assincronamente usando o cliente HttpClient.
    /// </summary>
    /// <param name="url">A URL do qual o conteúdo será baixado.</param>
    /// <returns>Uma tarefa representando a operação assíncrona, contendo o conteúdo baixado da URL.</returns>
    private static async Task<string> DownloadContentAsync(string url)
    {
        using (var httpClientHandler = new HttpClientHandler())
        {
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            using (var httpClient = new HttpClient(httpClientHandler))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                httpClient.DefaultRequestHeaders.Connection.TryParseAdd("keep-alive");
                httpClient.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };
                httpClient.DefaultRequestHeaders.Pragma.TryParseAdd("no-cache");
                httpClient.DefaultRequestHeaders.Referrer = new Uri("https://br.financas.yahoo.com/quote/MGLU3.SA");

                int retries = 3;
                int delay = 1000;
                while (retries > 0)
                {
                    try
                    {
                        string content = await httpClient.GetStringAsync(url);
                        return content;
                    }
                    catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests && retries > 0)
                    {
                        await Task.Delay(delay);
                        delay *= 2;
                        retries--;
                    }
                }
                throw new Exception("Failed to download content after multiple retries.");
            }
        }
    }

    /// <summary>
    /// Processa o conteúdo CSV fornecido e converte-o em uma lista de objetos MagazineLuizaHistoryPrice.
    /// </summary>
    /// <param name="csvContent">O conteúdo CSV a ser processado.</param>
    /// <returns>Uma lista de objetos MagazineLuizaHistoryPrice obtidos a partir do conteúdo CSV.</returns>
    private static List<MagazineLuizaHistoryPrice> ProcessCsvData(string csvContent)
    {
        using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent)))
        using (var reader = new StreamReader(memoryStream, Encoding.ASCII))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<MagazineLuizaHistoryPrice>().ToList();
        }
    }

    /// <summary>
    /// Processa o HTML fornecido e converte-o em uma lista de objetos MagazineLuizaHistoryPrice.
    /// </summary>
    /// <param name="htmlContent">O conteúdo HTML contendo a tabela.</param>
    /// <returns>Uma lista de objetos MagazineLuizaHistoryPrice obtidos a partir do HTML.</returns>
    private static List<MagazineLuizaHistoryPrice> ProcessHtmlData(string htmlContent)
    {
        var document = new HtmlDocument();
        document.LoadHtml(htmlContent);
        var div = document.DocumentNode.SelectSingleNode("//div[@id='mrt-node-Col1-1-HistoricalDataTable' and @data-locator='subtree-root']");

        if (div == null)
        {
            throw new Exception("Div com id 'mrt-node-Col1-1-HistoricalDataTable' não encontrada no HTML.");
        }

        // Busca a tabela dentro dessa div
        var table = div.SelectSingleNode(".//table[@data-test='historical-prices']");

        if (table == null)
        {
            throw new Exception("Tabela de preços históricos não encontrada dentro da div especificada.");
        }

        var rows = table.SelectNodes(".//tr");

        var prices = new List<MagazineLuizaHistoryPrice>();

        foreach (var row in rows)
        {
            var cells = row.SelectNodes(".//td");

            if (cells != null && cells.Count == 7)
            {
                var date = DateTime.Parse(cells[0].InnerText.Trim(), new CultureInfo("pt-BR"));
                var open = decimal.Parse(cells[1].InnerText.Trim(), CultureInfo.InvariantCulture);
                var high = decimal.Parse(cells[2].InnerText.Trim(), CultureInfo.InvariantCulture);
                var low = decimal.Parse(cells[3].InnerText.Trim(), CultureInfo.InvariantCulture);
                var close = decimal.Parse(cells[4].InnerText.Trim(), CultureInfo.InvariantCulture);
                var adjClose = double.Parse(cells[5].InnerText.Trim(), CultureInfo.InvariantCulture);
                long.TryParse(cells[6].InnerText.Trim().Replace(".", string.Empty), CultureInfo.InvariantCulture, out var volume);

                prices.Add(new MagazineLuizaHistoryPrice(date, open, high, low, close, adjClose, volume));
            }
        }

        return prices;
    }

    /// <summary>
    /// Inicializa uma nova instância do repositório <see cref="HomeBrokerRepository"/>.
    /// </summary>
    public HomeBrokerRepository(IMagazineLuizaHistoryPriceFactory magazineLuizaHistoryPriceFactory)
    {
        _historyPriceFactory = magazineLuizaHistoryPriceFactory;
    }

    /// <summary>
    /// Obtém os dados históricos de preço do Magazine Luiza para o período especificado.
    /// </summary>
    /// <param name="period">O período para o qual os dados históricos são desejados.</param>
    /// <returns>Uma lista de objetos <see cref="MagazineLuizaHistoryPrice"/>.</returns>
    public async Task<List<MagazineLuizaHistoryPrice>> GetHistoryData(Period period)
    {
        var allPrices = new List<MagazineLuizaHistoryPrice>();
        DateTime currentStartDate = period.StartDate;
        DateTime currentEndDate = period.EndDate;

        while (currentStartDate < currentEndDate)
        {
            var urlLink = $"https://br.financas.yahoo.com/quote/MGLU3.SA/history/?period1={ToUnixTimestamp(currentStartDate)}&period2={ToUnixTimestamp(currentEndDate)}&interval=1d&events=history&includeAdjustedClose=true";
            string downloadContent = await DownloadContentAsync(urlLink);
            var processData = ProcessHtmlData(downloadContent);

            allPrices.AddRange(processData.Select(price =>
                _historyPriceFactory.GetHistoryPrice(price.Date, price.Open, price.High, price.Low, price.Close, price.AdjClose, price.Volume)));

            DateTime minDate = allPrices.Min(p => p.Date);
            DateTime maxDate = allPrices.Max(p => p.Date);

            if (minDate >= currentStartDate && processData.Count > 0)
                currentEndDate = minDate;
            else if (processData.Count < 50 || processData.Count == 0)
                break;
            else
                break;
        }

        return allPrices;
    }

}