using Domain.Charts.ValueObject;
using HomeBrokerXUnit.Faker;

namespace Domain;
public class MACDTest
{
    [Fact]
    public void Should_Create_MACD_Correctly()
    {
        // Arrange
        var faker = MagazineLuizaHistoryPriceFaker.GetListFaker(10);

        // Act
        var macd = new MACD(faker);

        // Assert
        Assert.NotNull(macd);
        Assert.NotNull(macd.MACDLine);
        Assert.NotNull(macd.Signal);
        Assert.NotNull(macd.Histogram);
    }
}