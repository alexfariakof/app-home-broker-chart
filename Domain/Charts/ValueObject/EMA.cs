namespace Domain.Charts.ValueObject;
public record EMA
{
    public List<decimal> Values { get; set; } = new List<decimal>();
    private EMA() { }
    public EMA(List<decimal> historyPriceData, int periodDays)
    {

    }
}