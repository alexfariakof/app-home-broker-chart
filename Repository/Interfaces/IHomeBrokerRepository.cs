using Domain.Charts.Agreggates;

namespace Repository.Interfaces
{
    public interface IHomeBrokerRepository
    {
        Task<List<MagazineLuizaHistoryPrice>> GetHistoryData(DateTime startDate, DateTime endDate);
    }
}
