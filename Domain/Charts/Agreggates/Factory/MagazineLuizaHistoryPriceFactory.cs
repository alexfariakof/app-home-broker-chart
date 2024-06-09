using HomeBroker.Domain.Charts.Agreggates.Factory;

namespace Domain.Charts.Agreggates.Factory;

public sealed class MagazineLuizaHistoryPriceFactory: IMagazineLuizaHistoryPriceFactory
{
    private readonly Dictionary<(decimal, decimal, decimal, decimal, double), MagazineLuizaHistoryPrice> _cache = new();

    public MagazineLuizaHistoryPrice GetHistoryPrice(DateTime date, decimal open, decimal high, decimal low, decimal close, double adjClose, long volume)
    {
        var key = (open, high, low, close, adjClose);

        if (!_cache.TryGetValue(key, out var historyPrice))
        {
            historyPrice = new MagazineLuizaHistoryPrice(date, open, high, low, close, adjClose, volume);
            _cache[key] = historyPrice;
        }

        return historyPrice;
    }
}