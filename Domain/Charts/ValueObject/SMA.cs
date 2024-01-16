using Domain.Charts.Agreggates;

namespace Domain.Charts.ValueObject;
public record Sma
{
    public List<DateTime> Dates { get; set; } = new List<DateTime>();
    public List<decimal> Values { get; set; } = new List<decimal>();
    public Sma(List<MagazineLuizaHistoryPrice> historyList, int periodDays = 5)
    {
        if (historyList == null || historyList.Count == 0 || historyList.Count < periodDays)
            throw new ArgumentException("Não há dados suficiente para gerar uma SMA.");

        var historyPrice = historyList.Select(price => price.Close).ToList();
        var historyData = historyList.Select(h => h.Date).ToList();

        var count = historyList.Count;
        for (int i = 0; i < count; i++)
        {
            decimal sum = 0;
            for (int j = 0; j < periodDays; j++)
            {
                if (i + j >= count - periodDays)
                {
                    sum = Values.Last()*periodDays;
                    break;
                }
                sum += historyPrice[i +j];
            }
            decimal average = sum/periodDays;
            Dates.Add(historyData[i]);
            Values.Add(average);                  
        }
    }
}