using Domain.Charts.Agreggates;

namespace Business.Interfaces;
public interface IHomeBrokerBusiness
{
    public List<MagazineLuizaHistoryPrice> GetHistoryData(DateTime startDate, DateTime endDate);
}
