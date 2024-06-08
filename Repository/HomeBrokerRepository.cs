using CsvHelper;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using Repository.Interfaces;
using System.Globalization;
using System.Text;

namespace Repository;

/// <summary>
/// Repositório responsável por obter dados históricos do Magazine Luiza.
/// </summary>
public class HomeBrokerRepository : IHomeBrokerRepository
{
    /// <summary>
    /// Inicializa uma nova instância do repositório <see cref="HomeBrokerRepository"/>.
    /// </summary>
    public HomeBrokerRepository() { }

    /// <summary>
    /// Obtém os dados históricos de preço do Magazine Luiza para o período especificado.
    /// </summary>
    /// <param name="period">O período para o qual os dados históricos são desejados.</param>
    /// <returns>Uma lista de objetos <see cref="MagazineLuizaHistoryPrice"/>.</returns>
    public async Task<List<MagazineLuizaHistoryPrice>> GetHistoryData(Period period)
    {
        var downloadLink = $"https://query1.finance.yahoo.com/v7/finance/download/MGLU3.SA?period1={ToUnixTimestamp(period.StartDate)}&period2={ToUnixTimestamp(period.EndDate)}&interval=1d&filter=history&frequency=1d";
        try
        {
            string downloadContent = await DownloadContentAsync(downloadLink);
            return ProcessCsvData(downloadContent);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

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
                httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
                httpClient.DefaultRequestHeaders.Connection.TryParseAdd("keep-alive");
                httpClient.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue() { NoCache = true };
                httpClient.DefaultRequestHeaders.Pragma.TryParseAdd("no-cache");
                httpClient.DefaultRequestHeaders.Referrer = new Uri("https://query1.finance.yahoo.com/v7/finance/download/MGLU3.SA");

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
}