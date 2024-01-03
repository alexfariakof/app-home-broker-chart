using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;

namespace Business.Interfaces;
public interface IHomeBrokerBusiness
{
    public List<MagazineLuizaHistoryPrice> GetHistoryData(Period period);
    public SMA GetSMA();    
    public EMA GetEMA(int periodDays);
}
