using CsvHelper;
using Domain.Charts.Agreggates;
using Repository.Interfaces;
using System.Globalization;
using System.Text;

namespace Repository;
public class HomeBrokerRepository : IHomeBrokerRepository
{
    public HomeBrokerRepository()  { }

    public async Task<List<MagazineLuizaHistoryPrice>> GetHistoryData(DateTime startDate, DateTime endDate)
    {
        var downloadLink = $"https://query1.finance.yahoo.com/v7/finance/download/MGLU3.SA?period1={ToUnixTimestamp(startDate)}&period2={ToUnixTimestamp(endDate)}&interval=1d&filter=history&frequency=1d";
        string downloadContent = await DownloadContentAsync(downloadLink);                
        return ProcessCsvData(downloadContent);                

    }
    private static long ToUnixTimestamp(DateTime date)
    {
        return (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
    }
    private  static async Task<string> DownloadContentAsync(string url)
    {
        using (var httpClient = new HttpClient())
        {
            // Baixa o conteúdo da URL fornecida
            string content = await httpClient.GetStringAsync(url);
            return content;
        }
    }
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