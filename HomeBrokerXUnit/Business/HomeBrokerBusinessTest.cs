using Domain.Charts.ValueObject;
using HomeBrokerXUnit.__mock__;
using Repository.Interfaces;
using Moq;
using Domain.Charts.Agreggates.Factory;
using Repository;
using OfficeOpenXml;
using Domain.Charts.Agreggates;
using HomeBroker.Business.Cache;

namespace Business;
public sealed class HomeBrokerBusinessTest
{
    [Fact]
    public async Task Should_Returns_HistoryData_GetHistoryData()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(10);

        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Returns(Task.FromResult(fakeHistoryData));
        var business = new HomeBrokerBusiness(mockFactory, mockRepository.Object);

        // Act
        var result = await business.GetHistoryData(fakePeriod);

        // Assert
        Assert.NotNull(result);
        var count = result.Count();
        Assert.Equivalent(fakeHistoryData, result, true);
    }

    [Fact]
    public async Task Should_Returns_SameData_EveryTime_GetHistoryData()
    {
        // Arrange
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var mockRepository = new HomeBrokerRepository(mockFactory);        
        var endDate = new DateTime(2024, 06, 09);
        var fakePeriod = new Period(endDate.AddYears(-1), endDate);
        var business = new HomeBrokerBusiness(mockFactory, mockRepository);

        // Act
        var result = await business.GetHistoryData(fakePeriod);
        var repoResult = await  mockRepository.GetHistoryData(fakePeriod);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(repoResult);
        Assert.Equal(249, result.Count());
        Assert.Equal(249, repoResult.Count());
        Assert.Equivalent(result, repoResult, true);
    }

    [Fact]
    public async Task Should_Returns_SMA_GetSMA()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(10);

        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Returns(Task.FromResult(fakeHistoryData));
        var business = new HomeBrokerBusiness(mockFactory, mockRepository.Object);

        // Act
        var result = await business.GetSMA(fakePeriod);

        // Assert     
        Assert.NotNull(result);
        Assert.IsType<Sma>(result);
        Assert.Equal(result.Values.Count, fakeHistoryData.Count);
    }

    [Fact]
    public async Task Should_Returns_EMA_GetEMA()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(20);

        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Returns(Task.FromResult(fakeHistoryData));

        var business = new HomeBrokerBusiness(mockFactory, mockRepository.Object);

        // Act
        var result = await business.GetEMA(10, fakePeriod);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Ema>(result);
        Assert.True(fakeHistoryData.Count > result.Values.Count);
    }

    [Fact]
    public async Task Should_Returns_MACD_GetMACD()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(34);

        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Returns(Task.FromResult(fakeHistoryData));

        var business = new HomeBrokerBusiness(mockFactory, mockRepository.Object);

        // Act
        var result =await business.GetMACD(fakePeriod);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<MACD>(result);
        Assert.NotEmpty(result.MACDLine);
        Assert.NotEmpty(result.Signal);
        Assert.NotEmpty(result.Histogram);
    }

    [Fact]
    public void Should_Throw_Exception_GetSMA()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var business = new HomeBrokerBusiness(mockFactory, mockRepository.Object);
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Throws(new Exception ("GetSMA_WithException_ShouldThrowException"));

        // Act & Assert
        Assert.Throws<AggregateException>(() => business.GetSMA(fakePeriod).Result);
    }

    [Fact]
    public void Should_Throw_Exception_GetEMA()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var business = new HomeBrokerBusiness(mockFactory, mockRepository.Object);
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Throws(new Exception ("GetEMA_WithException_ShouldThrowException"));

        // Act & Assert
        Assert.Throws<AggregateException >(() => business.GetEMA(10, fakePeriod).Result);
    }

    [Fact]
    public void Should_Throw_Exception_GetMACD()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var business = new HomeBrokerBusiness(mockFactory, mockRepository.Object);
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Throws(new Exception ("GetMACD_WithException_ShouldThrowException"));

        // Act & Assert
        Assert.Throws<AggregateException>(() => business.GetMACD(fakePeriod).Result);
    }

    [Fact]
    public async Task Should_Generate_Excel_History()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(10);

        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Returns(Task.FromResult(fakeHistoryData));
        var business = new HomeBrokerBusiness(mockFactory, mockRepository.Object);

        // Act
        var result = await business.GenerateExcelHistory(fakePeriod);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<MemoryStream>(result);

        result.Position = 0;
        using (var package = new ExcelPackage(result))
        {
            var worksheet = package.Workbook.Worksheets["Histórico"];
            Assert.NotNull(worksheet);

            // Verifica cabeçalhos
            Assert.Equal("Data", worksheet.Cells["A1"].Value);
            Assert.Equal("Preço de Abertura", worksheet.Cells["B1"].Value);
            Assert.Equal("Preço Mais Alto", worksheet.Cells["C1"].Value);
            Assert.Equal("Preço Mais Baixo", worksheet.Cells["D1"].Value);
            Assert.Equal("Preço de Fechamento", worksheet.Cells["E1"].Value);
            Assert.Equal("Preço de Fechamento Ajustado", worksheet.Cells["F1"].Value);
            Assert.Equal("Volume", worksheet.Cells["G1"].Value);

            // Verifica dados
            for (int i = 0; i < fakeHistoryData.Count; i++)
            {
                var item = fakeHistoryData[i];
                Assert.Equal(item.Date.ToShortDateString(), worksheet.Cells[i + 2, 1].GetValue<DateTime>().ToShortDateString());
                Assert.Equal(item.Open, worksheet.Cells[i + 2, 2].GetValue<decimal>());
                Assert.Equal(item.High, worksheet.Cells[i + 2, 3].GetValue<decimal>());
                Assert.Equal(item.Low, worksheet.Cells[i + 2, 4].GetValue<decimal>());
                Assert.Equal(item.Close, worksheet.Cells[i + 2, 5].GetValue<decimal>());
                Assert.Equal(item.AdjClose, worksheet.Cells[i + 2, 6].GetValue<double>());
                Assert.Equal(item.Volume, worksheet.Cells[i + 2, 7].GetValue<long>());
            }
        }
    }

    [Fact]
    public async Task Should_Returns_HistoryData_FromCache()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(10);
        var cacheEntry = new CacheEntry<List<MagazineLuizaHistoryPrice>>(fakeHistoryData, DateTime.UtcNow, TimeSpan.FromMinutes(20));

        var business = new HomeBrokerBusiness(mockFactory, mockRepository.Object);
        business.GetType().GetField("_historyCache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(business, new Dictionary<Period, CacheEntry<List<MagazineLuizaHistoryPrice>>> { { fakePeriod, cacheEntry } });

        // Act
        var result = await business.GetHistoryData(fakePeriod);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(fakeHistoryData.Count, result.Count);
        Assert.Equivalent(fakeHistoryData, result);
    }

    [Fact]
    public async Task Should_Returns_HistoryData_FromRepository_And_UpdateCache()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(10);

        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Returns(Task.FromResult(fakeHistoryData));
        var business = new HomeBrokerBusiness(mockFactory, mockRepository.Object);

        // Act
        var result = await business.GetHistoryData(fakePeriod);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(fakeHistoryData.Count, result.Count);
        Assert.Equivalent(fakeHistoryData, result);

        // Verify cache update
        var cache = business?.GetType()?.GetField("_historyCache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(business) as Dictionary<Period, CacheEntry<List<MagazineLuizaHistoryPrice>>>;
        Assert.True(cache?.ContainsKey(fakePeriod));
        Assert.Equivalent(fakeHistoryData, cache[fakePeriod].Data);
    }

    [Fact]
    public async Task Should_Returns_HistoryData_FromRepository_After_CacheExpiry()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var mockFactory = new MagazineLuizaHistoryPriceFactory();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(10);
        var expiredCacheEntry = new CacheEntry<List<MagazineLuizaHistoryPrice>>(fakeHistoryData, DateTime.UtcNow.AddMinutes(-30), TimeSpan.FromMinutes(20));

        var business = new HomeBrokerBusiness(mockFactory, mockRepository.Object);
        business?.GetType()?.GetField("_historyCache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(business, new Dictionary<Period, CacheEntry<List<MagazineLuizaHistoryPrice>>> { { fakePeriod, expiredCacheEntry } });
        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Returns(Task.FromResult(fakeHistoryData));

        // Act
        var result = await business.GetHistoryData(fakePeriod);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(fakeHistoryData.Count, result.Count);
        Assert.Equivalent(fakeHistoryData, result);

        // Verify cache update
        var cache = business?.GetType()?.GetField("_historyCache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(business) as Dictionary<Period, CacheEntry<List<MagazineLuizaHistoryPrice>>>;
        Assert.True(cache?.ContainsKey(fakePeriod));
        Assert.Equivalent(fakeHistoryData, cache[fakePeriod].Data);
    }

}