namespace Domain.Charts.ValueObject;
public record SMA
{
    public List<decimal> Values { get; set; } = new List<decimal>();
    private SMA() { }
    public SMA(List<decimal> historyPriceData, int periodDays = 5)
    {
        if (historyPriceData == null || historyPriceData.Count == 0 || historyPriceData.Count < periodDays)
            throw new AggregateException("Não há dados suficiente para gerar uma SMA");

        var count = historyPriceData.Count;

        for (int i = 0; i < count; i++)
        {
            decimal sum = 0;
            for (int j = 0; j < 4; j++)
            {
                if (i + j >= count) break;
                sum += historyPriceData[i +j];
            }
            decimal average = sum / periodDays;
            Values.Add(average);          
        }
    }
}