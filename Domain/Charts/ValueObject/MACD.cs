using Domain.Charts.Agreggates;

namespace Domain.Charts.ValueObject;
public record MACD
{
    const int MIN_AMOUNT_DATA = 34;
    const int MACD_LINE_VALUE = 9;
    const int EMA12_VALUE = 12;
    const int EMA26_VALUE = 26;
    public List<decimal> MACDLine { get; set; } = new List<decimal>();
    public List<decimal> Signal{ get; set; } = new List<decimal>();    
    public List<decimal> Histogram { get; set; } = new List<decimal>();

    public MACD(List<MagazineLuizaHistoryPrice> historyData)           
    {
        if (historyData == null || historyData.Count == 0 || historyData.Count < MIN_AMOUNT_DATA)
            throw new ArgumentException("Não há dados suficientes para gerar um MACD.");

        var ema12 = new Ema(historyData, EMA12_VALUE);
        var ema26 = new Ema(historyData, EMA26_VALUE);

        //MACD Line: (12-day EMA - 26-day EMA)
        for (var i = 0; i < ema12.Values.Count; i++)
        {
            if (i < ema26.Values.Count)
                MACDLine.Add(ema12.Values[i] - ema26.Values[i]);
        }

        //Signal Line: 9-day EMA of MACD Line
        for (var i = 0; i < MACDLine.Count; i++)
            Signal= new Ema(MACDLine, MACD_LINE_VALUE).Values;

        // MACD Histogram: MACD Line -Signal Line
        for (var i = 0; i < historyData.Count; i++)
        {
            if (i < Signal.Count)
                Histogram.Add(MACDLine[i] - Signal[i]);
        }
    }
}