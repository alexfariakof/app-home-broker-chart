using Business.Interfaces;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using Repository.Interfaces;
using HomeBroker.Domain.Charts.Agreggates.Factory;
using HomeBroker.Business.Cache;

namespace Business;
public class HomeBrokerBusiness: IHomeBrokerBusiness
{ 
    private readonly IHomeBrokerRepository _homeBrokerRepository;
    private readonly IMagazineLuizaHistoryPriceFactory _historyPriceFactory;    
    private readonly TimeSpan CACHE_EXPIRATION_TIME = TimeSpan.FromMinutes(20);
    private readonly TimeSpan CACHE_DELAY = TimeSpan.FromMinutes(30);
    private Dictionary<Period, CacheEntry<List<MagazineLuizaHistoryPrice>>> _historyCache;
    private readonly Timer _cacheCleanupTimer;
    private readonly object _lock = new object();
    public List<MagazineLuizaHistoryPrice> HomeBrokerHistory { get; private set; }

    public HomeBrokerBusiness(IMagazineLuizaHistoryPriceFactory magazineLuizaHistoryPriceFactory, IHomeBrokerRepository homeBrokerRepository)
    {
        _historyPriceFactory = magazineLuizaHistoryPriceFactory;
        _homeBrokerRepository = homeBrokerRepository;
        _historyCache = new Dictionary<Period, CacheEntry<List<MagazineLuizaHistoryPrice>>>();
        this.HomeBrokerHistory = new List<MagazineLuizaHistoryPrice>();
        _cacheCleanupTimer = new Timer(DisposeCache, null, TimeSpan.Zero, CACHE_DELAY);
    }

    public async  Task<List<MagazineLuizaHistoryPrice>> GetHistoryData(Period period)
    {
        if (_historyCache.TryGetValue(period, out var cacheEntry) && !cacheEntry.IsExpired())
        {
            cacheEntry.ExpirationTime = DateTime.UtcNow.Add(CACHE_EXPIRATION_TIME);
            return cacheEntry.Data.
                Select(price => _historyPriceFactory.GetHistoryPrice(
                    price.Date, price.Open, price.High, price.Low, price.Close, price.AdjClose, price.Volume
                    )).ToList();
        }

        var historyData = (await _homeBrokerRepository.GetHistoryData(period))
               .Select(price => _historyPriceFactory.GetHistoryPrice(
                   price.Date, price.Open, price.High, price.Low, price.Close, price.AdjClose, price.Volume
               )).ToList();

        _historyCache[period] = new CacheEntry<List<MagazineLuizaHistoryPrice>>(historyData, DateTime.UtcNow, CACHE_EXPIRATION_TIME);
        return historyData;
    }

    public async Task<Sma> GetSMA(Period period)
    {
        try
        {
            var historyData = await GetHistoryData(period);
            var sma = new Sma(historyData.Select(price => price.Close).ToList());
            return sma;
        }
        catch 
        {
            throw new ArgumentException("Erro ao gerar SMA.");
        }
    }

    public async  Task<Ema> GetEMA(int periodDays, Period period)
    {
        try
        {
            var historyData = await GetHistoryData(period);
            var ema = new Ema(historyData.Select(price => price.Close).ToList(), periodDays);
            return ema;
        }
        catch
        {
            throw new ArgumentException("Erro ao gerar EMA.");
        }
    }

    public async Task<MACD> GetMACD(Period period)
    {
        try
        {
            var historyData = await GetHistoryData(period);
            var macd =  new MACD(historyData.Select(price => price.Close).ToList());
            return macd;
        }
        catch 
        {
            throw new ArgumentException("Erro ao gerar MACD.");
        }
    }

    private void DisposeCache(object state)
    {
        lock (_lock)
        {
            var expiredEntries = _historyCache.Where(e => e.Value.IsExpired()).ToList();
            foreach (var entry in expiredEntries)
            {
                _historyCache.Remove(entry.Key);
            }
        }
    }
}