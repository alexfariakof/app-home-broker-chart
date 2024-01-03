using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;

namespace Repository.Interfaces
{
    public interface IHomeBrokerRepository
    {
        Task<List<MagazineLuizaHistoryPrice>> GetHistoryData(Period period);
    }
}
