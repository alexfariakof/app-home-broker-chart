using Domain.Charts.Agreggates;

namespace Domain.Charts.ValueObject;
public record MACD
{
    public List<decimal> MACDLine { get; set; } = new List<decimal>();
    public List<decimal> Signal{ get; set; } = new List<decimal>();    
    public List<decimal> Histogram { get; set; } = new List<decimal>();

    public MACD(List<MagazineLuizaHistoryPrice> historyData)
    {
        var ema12 = new Ema(historyData.Select(price => price.Close).ToList(), 12);
        var ema26 = new Ema(historyData.Select(price => price.Close).ToList(), 26);

        //MACD Line: (12-day EMA - 26-day EMA)
        for (var i = 0; i < ema12.Values.Count; i++)
        {
            if (i < ema26.Values.Count)
                MACDLine.Add(ema12.Values[i] - ema26.Values[i]);
        }

        //Signal Line: 9-day EMA of MACD Line
        for (var i = 0; i < MACDLine.Count; i++)
            Signal= new Ema(MACDLine, 9).Values;

        // MACD Histogram: MACD Line -Signal Line
        for (var i = 0; i < historyData.Count; i++)
        {
            if (i < Signal.Count)
                Histogram.Add(MACDLine[i] - Signal[i]);
        }
    }
}