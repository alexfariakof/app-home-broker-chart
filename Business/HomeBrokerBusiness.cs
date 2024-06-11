using Business.Interfaces;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using Repository.Interfaces;
using HomeBroker.Domain.Charts.Agreggates.Factory;
using HomeBroker.Business.Cache;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Business;
public class HomeBrokerBusiness : IHomeBrokerBusiness
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

    public async Task<List<MagazineLuizaHistoryPrice>> GetHistoryData(Period period)
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

    public async Task<Ema> GetEMA(int periodDays, Period period)
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
            var macd = new MACD(historyData.Select(price => price.Close).ToList());
            return macd;
        }
        catch
        {
            throw new ArgumentException("Erro ao gerar MACD.");
        }
    }

    public async Task<MemoryStream> GenerateExcelHistory(Period period)
    {
        var historyData = await GetHistoryData(period);

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Histórico");

            // Adiciona os cabeçalhos
            worksheet.Cells["A1"].Value = "Data";
            worksheet.Cells["B1"].Value = "Preço de Abertura";
            worksheet.Cells["C1"].Value = "Preço Mais Alto";
            worksheet.Cells["D1"].Value = "Preço Mais Baixo";
            worksheet.Cells["E1"].Value = "Preço de Fechamento";
            worksheet.Cells["F1"].Value = "Preço de Fechamento Ajustado";
            worksheet.Cells["G1"].Value = "Volume";

            // Formata os cabeçalhos
            using (var range = worksheet.Cells["A1:G1"])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // Adiciona os dados e formata as células
            for (int i = 0; i < historyData.Count; i++)
            {
                var item = historyData[i];
                worksheet.Cells[i + 2, 1].Value = item.Date;
                worksheet.Cells[i + 2, 1].Style.Numberformat.Format = "dd/MM/yyyy";
                worksheet.Cells[i + 2, 2].Value = item.Open;
                worksheet.Cells[i + 2, 2].Style.Numberformat.Format = "R$ #,######0.0000000";
                worksheet.Cells[i + 2, 3].Value = item.High;
                worksheet.Cells[i + 2, 3].Style.Numberformat.Format = "R$ #,######0.0000000";
                worksheet.Cells[i + 2, 4].Value = item.Low;
                worksheet.Cells[i + 2, 4].Style.Numberformat.Format = "R$ #,######0.0000000";
                worksheet.Cells[i + 2, 5].Value = item.Close;
                worksheet.Cells[i + 2, 5].Style.Numberformat.Format = "R$ #,######0.0000000";
                worksheet.Cells[i + 2, 6].Value = item.AdjClose;
                worksheet.Cells[i + 2, 6].Style.Numberformat.Format = "R$ #,######0.0000000";
                worksheet.Cells[i + 2, 7].Value = item.Volume;
                worksheet.Cells[i + 2, 7].Style.Numberformat.Format = "#,0";
            }

            // Ajusta a largura das colunas
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            return stream;
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