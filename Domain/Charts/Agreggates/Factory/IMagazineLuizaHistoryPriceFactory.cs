using Domain.Charts.Agreggates;

namespace HomeBroker.Domain.Charts.Agreggates.Factory;

public interface IMagazineLuizaHistoryPriceFactory
{
    MagazineLuizaHistoryPrice GetHistoryPrice(DateTime date, decimal open, decimal high, decimal low, decimal close, double adjClose, long volume);
}
