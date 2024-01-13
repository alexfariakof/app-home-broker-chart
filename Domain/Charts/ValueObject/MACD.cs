using Domain.Charts.Agreggates;

namespace Domain.Charts.ValueObject;
public record MACD
{
    public List<decimal> MACDLine { get; set; } = new List<decimal>();
    public List<decimal> Signal{ get; set; } = new List<decimal>();    
    public List<decimal> Histogram { get; set; } = new List<decimal>();

    public MACD(List<MagazineLuizaHistoryPrice> historyData)
    {
        MACDLine = historyData.Select(price => price.Close).ToList();
        Signal = historyData.Select(price => price.Close).ToList();
        Histogram = historyData.Select(price => price.Close).ToList();
    }
}