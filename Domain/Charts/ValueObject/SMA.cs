namespace Domain.Charts.ValueObject;
public record Sma
{
    public List<decimal> Values { get; set; } = new List<decimal>();
    public Sma(List<decimal> historyPriceData, int periodDays = 5)
    {
        if (historyPriceData == null || historyPriceData.Count == 0 || historyPriceData.Count < periodDays)
            throw new ArgumentException("Não há dados suficiente para gerar uma SMA.");

        var count = historyPriceData.Count;

        for (int i = 0; i < count; i++)
        {
            decimal sum = 0;
            for (int j = 0; j < periodDays; j++)
            {
                if (i + j >= count) break;
                sum += historyPriceData[i +j];
            }
            decimal average = sum / periodDays;
            Values.Add(average);          
        }
    }
}