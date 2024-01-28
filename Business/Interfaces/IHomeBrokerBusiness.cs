using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;

namespace Business.Interfaces;
public interface IHomeBrokerBusiness
{
    public List<MagazineLuizaHistoryPrice> GetHistoryData(Period period);
    public Sma GetSMA(Period period);    
    public Ema GetEMA(int periodDays, Period period);
    public MACD GetMACD(Period period);
}
