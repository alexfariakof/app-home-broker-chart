using Business.Interfaces;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using HomeBrokerXUnit.Faker;
using Moq;
using webapi.Controllers;

namespace Controllers;
public class ChartHomeBrokerControllerTest
{
    [Fact]
    public void Get_Should_Returns_Correct_Results()
    {
        // Arrange
        var businessMock = new Mock<IHomeBrokerBusiness>();

        var expectedData = MagazineLuizaHistoryPriceFaker.GetListFaker(150);
        businessMock.Setup(business => business.GetHistoryData(It.IsAny<Period>())).Returns(expectedData);
        var controller = new ChartHomeBrokerController(businessMock.Object);

        // Act
        var result = controller.Get(DateTime.Now.AddYears(-1), DateTime.Now);

        // Assert
        var actionResult = Assert.IsType<List<MagazineLuizaHistoryPrice>>(result);
        Assert.Equal(150, actionResult.Count);
        Assert.Equal(expectedData, actionResult);
    }

    [Fact]
    public void GetSMA_Should_Returns_SMA()
    {
        // Arrange
        var businessMock = new Mock<IHomeBrokerBusiness>();

        var expectedSMA = new SMA(MagazineLuizaHistoryPriceFaker.GetListFaker(100).Select(price => price.Close).ToList(), 10);
        businessMock.Setup(business => business.GetSMA()).Returns(expectedSMA);
        var controller = new ChartHomeBrokerController(businessMock.Object);

        // Act
        var result = controller.GetSMA();

        // Assert
        var actionResult = Assert.IsType<SMA>(result);
        Assert.Equal(expectedSMA.Values, actionResult.Values);
    }

    [Fact]
    public void GetEMA_Should_Returns_EMA()
    {
        // Arrange
        var businessMock = new Mock<IHomeBrokerBusiness>();

        var expectedEMA = new EMA(MagazineLuizaHistoryPriceFaker.GetListFaker(200).Select(price => price.Close).ToList(), 10);
        businessMock.Setup(business => business.GetEMA(It.IsAny<int>())).Returns(expectedEMA);
        var controller = new ChartHomeBrokerController(businessMock.Object);

        // Act
        var result = controller.GetEMA(10);

        // Assert
        var actionResult = Assert.IsType<EMA>(result);
        Assert.Equal(expectedEMA.Values, actionResult.Values);
    }
}
