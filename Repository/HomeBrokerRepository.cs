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
        string downloadContent = await DownloadContentAsync(downloadLink);
        return ProcessCsvData(downloadContent);
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
    /// Baixa o conteúdo de uma URL assíncronamente usando o cliente HttpClient.
    /// </summary>
    /// <param name="url">A URL do qual o conteúdo será baixado.</param>
    /// <returns>Uma tarefa representando a operação assíncrona, contendo o conteúdo baixado da URL.</returns>    
    private static async Task<string> DownloadContentAsync(string url)
    {
        using (var httpClient = new HttpClient())
        {
            // Baixa o conteúdo da URL fornecida
            string content = await httpClient.GetStringAsync(url);
            return content;
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