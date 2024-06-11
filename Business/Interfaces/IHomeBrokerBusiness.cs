using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;

namespace Business.Interfaces;
public interface IHomeBrokerBusiness
{
    public Task<List<MagazineLuizaHistoryPrice>> GetHistoryData(Period period);
    public Task<Sma> GetSMA(Period period);    
    public Task<Ema> GetEMA(int periodDays, Period period);
    public Task<MACD> GetMACD(Period period);
    public Task<MemoryStream> GenerateExcelHistory(Period period);

}
