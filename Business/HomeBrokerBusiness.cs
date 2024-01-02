using Business.Interfaces;
using Domain.Charts.Agreggates;
using Repository.Interfaces;

namespace Business;
public class HomeBrokerBusiness : IHomeBrokerBusiness
{ 
    private readonly IHomeBrokerRepository homeBrokerRepository;
    public HomeBrokerBusiness(IHomeBrokerRepository _repo)
    {
        homeBrokerRepository = _repo;
    }

    public List<MagazineLuizaHistoryPrice> GetHistoryData(DateTime startDate, DateTime endDate)
    {
        return homeBrokerRepository.GetHistoryData(startDate, endDate).Result;
    }
}