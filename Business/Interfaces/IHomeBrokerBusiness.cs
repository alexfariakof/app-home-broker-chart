using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;

namespace Business.Interfaces;
public interface IHomeBrokerBusiness
{
    public List<MagazineLuizaHistoryPrice> GetHistoryData(Period period);
    public Sma GetSMA();    
    public Ema GetEMA(int periodDays);
}
