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
        var result = business.GetSMA();

        // Assert     
        Assert.NotNull(result);
        Assert.IsType<SMA>(result);
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
        var result = business.GetEMA(10);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<EMA>(result);
        Assert.True(fakeHistoryData.Count > result.Values.Count);
    }
}