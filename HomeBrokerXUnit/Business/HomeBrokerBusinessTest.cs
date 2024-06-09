using Domain.Charts.ValueObject;
using HomeBrokerXUnit.__mock__;
using Repository.Interfaces;
using Moq;
using Domain.Charts.Agreggates.Factory;
using Repository;

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
}