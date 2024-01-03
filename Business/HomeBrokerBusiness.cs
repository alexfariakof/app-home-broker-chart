using Business.Interfaces;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using Repository.Interfaces;

namespace Business;
public class HomeBrokerBusiness : IHomeBrokerBusiness
{ 
    private readonly IHomeBrokerRepository homeBrokerRepository;
    private Period _period;
    public List<MagazineLuizaHistoryPrice> homeBrokerHistory;

    public HomeBrokerBusiness(IHomeBrokerRepository _repo)
    {
        homeBrokerRepository = _repo;
        this._period = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        this.homeBrokerHistory = homeBrokerRepository.GetHistoryData(_period).Result;
    }

    public List<MagazineLuizaHistoryPrice> GetHistoryData(Period period)
    {
        this._period = period;
        return homeBrokerRepository.GetHistoryData(period).Result;
    }
    public SMA GetSMA()
    {
        List<decimal> closeValues = homeBrokerHistory.Select(price => price.Close).ToList();
        var sma = new SMA(closeValues);
        return sma;
    }
}