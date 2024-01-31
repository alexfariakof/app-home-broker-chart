using Domain.Charts.ValueObject;
using HomeBrokerXUnit.Faker;
using Repository.Interfaces;
using Moq;

namespace Business;
public class HomeBrokerBusinessTest
{

    [Fact]
    public void Should_Returns_HistoryData_GetHistoryData()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(10);

        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Returns(Task.FromResult(fakeHistoryData));
        var business = new HomeBrokerBusiness(mockRepository.Object);

        // Act
        var result = business.GetHistoryData(fakePeriod);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(fakeHistoryData, result);
    }

    [Fact]
    public void Should_Returns_SMA_GetSMA()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(10);

        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Returns(Task.FromResult(fakeHistoryData));
        var business = new HomeBrokerBusiness(mockRepository.Object);

        // Act
        var result = business.GetSMA(fakePeriod);

        // Assert     
        Assert.NotNull(result);
        Assert.IsType<Sma>(result);
        Assert.Equal(result.Values.Count, fakeHistoryData.Count);
    }

    [Fact]
    public void Should_Returns_EMA_GetEMA()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(20);

        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Returns(Task.FromResult(fakeHistoryData));

        var business = new HomeBrokerBusiness(mockRepository.Object);

        // Act
        var result = business.GetEMA(10, fakePeriod);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Ema>(result);
        Assert.True(fakeHistoryData.Count > result.Values.Count);
    }

    [Fact]
    public void Should_Returns_MACD_GetMACD()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var fakeHistoryData = MagazineLuizaHistoryPriceFaker.GetListFaker(34);

        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Returns(Task.FromResult(fakeHistoryData));

        var business = new HomeBrokerBusiness(mockRepository.Object);

        // Act
        var result = business.GetMACD(fakePeriod);

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
        var business = new HomeBrokerBusiness(mockRepository.Object);
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Throws(new Exception ("GetSMA_WithException_ShouldThrowException"));

        // Act & Assert
        Assert.Throws<Exception>(() => business.GetSMA(fakePeriod));
    }

    [Fact]
    public void Should_Throw_Exception_GetEMA()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var business = new HomeBrokerBusiness(mockRepository.Object);
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Throws(new Exception ("GetEMA_WithException_ShouldThrowException"));

        // Act & Assert
        Assert.Throws<Exception >(() => business.GetEMA(10, fakePeriod));
    }

    [Fact]
    public void Should_Throw_Exception_GetMACD()
    {
        // Arrange
        var mockRepository = new Mock<IHomeBrokerRepository>();
        var business = new HomeBrokerBusiness(mockRepository.Object);
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        mockRepository.Setup(repo => repo.GetHistoryData(It.IsAny<Period>())).Throws(new Exception ("GetMACD_WithException_ShouldThrowException"));

        // Act & Assert
        Assert.Throws<Exception >(() => business.GetMACD(fakePeriod));
    }
}