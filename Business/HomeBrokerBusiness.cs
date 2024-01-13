using Business.Interfaces;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using Repository.Interfaces;

namespace Business;
public class HomeBrokerBusiness : IHomeBrokerBusiness
{ 
    private readonly IHomeBrokerRepository homeBrokerRepository;
    public List<MagazineLuizaHistoryPrice> homeBrokerHistory;
    public HomeBrokerBusiness(IHomeBrokerRepository _repo)
    {
        homeBrokerRepository = _repo;
        this.homeBrokerHistory = new List<MagazineLuizaHistoryPrice>();
    }

    public List<MagazineLuizaHistoryPrice> GetHistoryData(Period period)
    {
        return homeBrokerRepository.GetHistoryData(period).Result;
    }
    public Sma GetSMA(Period period)
    {
        List<decimal> closeValues = this.GetHistoryData(period).Select(price => price.Close).ToList();
        var sma = new Sma(closeValues);
        return sma;
    }
    public Ema GetEMA(int periodDays, Period period)
    {
        List<decimal> closeValues = this.GetHistoryData(period).Select(price => price.Close).ToList();
        var ema = new Ema(closeValues, periodDays);
        return ema;
    }
    public MACD GetMACD(Period period)
    {
        var macd = new MACD(this.GetHistoryData(period));
        return macd;
    }
}