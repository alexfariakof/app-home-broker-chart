namespace Domain.Charts.ValueObject;
public record MACD
{
    public List<decimal> MACDLine { get; set; } = new List<decimal>();
    public List<decimal> Signal{ get; set; } = new List<decimal>();    
    public List<decimal> Histogram { get; set; } = new List<decimal>();

    public MACD(List<decimal> historyPriceData)
    {

    }
}