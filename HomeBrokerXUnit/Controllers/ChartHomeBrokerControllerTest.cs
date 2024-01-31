using Business.Interfaces;
using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;
using HomeBrokerSPA.Controllers;
using HomeBrokerXUnit.Faker;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Controllers;
public class ChartHomeBrokerControllerTest
{
    [Fact]
    public void Should_Returns_List_MagazineLuizaHistoryPrice_Get()
    {
        // Arrange
        var businessMock = new Mock<IHomeBrokerBusiness>();
        var expectedData = MagazineLuizaHistoryPriceFaker.GetListFaker(150);
        businessMock.Setup(business => business.GetHistoryData(It.IsAny<Period>())).Returns(expectedData);
        var controller = new ChartHomeBrokerController(businessMock.Object);

        // Act
        var result = controller.Get(DateTime.Now.AddYears(-1), DateTime.Now) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var actionResult = Assert.IsType<List<MagazineLuizaHistoryPrice>>(result.Value);
        Assert.Equal(150, actionResult.Count);
        Assert.Equal(expectedData, actionResult);
    }

    [Fact]
    public void Should_Returns_SMA_GetSMA()
    {
        // Arrange
        var businessMock = new Mock<IHomeBrokerBusiness>();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var expectedSMA = new Sma(MagazineLuizaHistoryPriceFaker.GetListFaker(100).Select(price => price.Close).ToList(), 10);
        businessMock.Setup(business => business.GetSMA(fakePeriod)).Returns(expectedSMA);
        var controller = new ChartHomeBrokerController(businessMock.Object);

        // Act
        var result = controller.GetSMA(fakePeriod.StartDate, fakePeriod.EndDate) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);        
        var actionResult = Assert.IsType<Sma>(result.Value);
        Assert.Equal(expectedSMA.Values, actionResult.Values);
    }

    [Fact]
    public void Should_Returns_EMA_GetEMA()
    {
        // Arrange
        var businessMock = new Mock<IHomeBrokerBusiness>();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var expectedEMA = new Ema(MagazineLuizaHistoryPriceFaker.GetListFaker(200).Select(price => price.Close).ToList(), 10);
        businessMock.Setup(business => business.GetEMA(It.IsAny<int>(), It.IsAny<Period>())).Returns(expectedEMA);
        var controller = new ChartHomeBrokerController(businessMock.Object);

        // Act
        var result = controller.GetEMA(10, fakePeriod.StartDate, fakePeriod.EndDate) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var actionResult = Assert.IsType<Ema>(result.Value);
        Assert.Equal(expectedEMA.Values, actionResult.Values);
    }

    [Fact]
    public void Should_Returns_MACD_GETMACD()
    {
        // Arrange
        var businessMock = new Mock<IHomeBrokerBusiness>();
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var expectedMACD = new MACD(MagazineLuizaHistoryPriceFaker.GetListFaker(200).Select(price => price.Close).ToList());
        businessMock.Setup(business => business.GetMACD(It.IsAny<Period>())).Returns(expectedMACD);
        var controller = new ChartHomeBrokerController(businessMock.Object);

        // Act
        var result = controller.GetMACD(fakePeriod.StartDate, fakePeriod.EndDate) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        var actionResult = Assert.IsType<MACD>(result.Value);
        Assert.Equal(expectedMACD.MACDLine, actionResult.MACDLine);
        Assert.Equal(expectedMACD.Signal, actionResult.Signal);
        Assert.Equal(expectedMACD.Histogram, actionResult.Histogram);
    }

    [Fact]
    public void Should_Return_NoContentResult_Get()
    {
        // Arrange
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var businessMock = new Mock<IHomeBrokerBusiness>();
        businessMock.Setup(business => business.GetHistoryData(It.IsAny<Period>())).Throws(new Exception("Erro desconhecido Nenhum resultado é retornado."));
        var controller = new ChartHomeBrokerController(businessMock.Object);

        // Act
        var result = controller.Get(fakePeriod.StartDate, fakePeriod.EndDate);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Should_Return_BadRequest_GetSMA()
    {
        // Arrange
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var businessMock = new Mock<IHomeBrokerBusiness>();        
        businessMock.Setup(business => business.GetSMA(It.IsAny<Period>())).Throws(new Exception("Get SMA Exeption"));
        var controller = new ChartHomeBrokerController(businessMock.Object);

        // Act
        var result = controller.GetSMA(fakePeriod.StartDate, fakePeriod.EndDate);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = Assert.IsType<BadRequestObjectResult>(result).Value;
        var message = value.GetType().GetProperty("message").GetValue(value, null) as string;
        Assert.Equal("Get SMA Exeption", message);
    }

    [Fact]
    public void Should_Return_BadRequest_GetEMA()
    {
        // Arrange
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var businessMock = new Mock<IHomeBrokerBusiness>();
        businessMock.Setup(business => business.GetEMA(It.IsAny<int>(), It.IsAny<Period>())).Throws(new Exception("Get EMA Exeption"));
        var controller = new ChartHomeBrokerController(businessMock.Object);

        // Act
        var result = controller.GetEMA(default, fakePeriod.StartDate, fakePeriod.EndDate);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = Assert.IsType<BadRequestObjectResult>(result).Value;
        var message = value.GetType().GetProperty("message").GetValue(value, null) as string;
        Assert.Equal("Get EMA Exeption", message);
    }

    [Fact]
    public void Should_Return_BadRequest_GetMACD()
    {
        // Arrange
        var fakePeriod = new Period(DateTime.Now.AddYears(-1), DateTime.Now);
        var businessMock = new Mock<IHomeBrokerBusiness>();
        businessMock.Setup(business => business.GetMACD(It.IsAny<Period>())).Throws(new Exception("Get MACD Exeption"));
        var controller = new ChartHomeBrokerController(businessMock.Object);

        // Act
        var result = controller.GetMACD(fakePeriod.StartDate, fakePeriod.EndDate);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BadRequestObjectResult>(result);
        var value = Assert.IsType<BadRequestObjectResult>(result).Value;
        var message = value.GetType().GetProperty("message").GetValue(value, null) as string;
        Assert.Equal("Get MACD Exeption", message);
    }
}
