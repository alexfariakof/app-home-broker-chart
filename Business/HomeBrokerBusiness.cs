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
        try
        {
            var sma = new Sma(this.GetHistoryData(period).Select(price => price.Close).ToList());
            return sma;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public Ema GetEMA(int periodDays, Period period)
    {
        try
        {
            var ema = new Ema(this.GetHistoryData(period).Select(price => price.Close).ToList(), periodDays);
            return ema;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public MACD GetMACD(Period period)
    {
        try
        {
            var macd = new MACD(this.GetHistoryData(period).Select(price => price.Close).ToList());
            return macd;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}