using Domain.Charts.Agreggates;
using HomeBrokerXUnit.__mock__;

namespace Domain;

public class MagazineLuizaHistoryPriceTest
{
    [Fact]
    public void Should_Create_Instance_MagazineLuizaHistoryPrice()
    {
        // Arrange
        var faker = MagazineLuizaHistoryPriceFaker.GetNewFaker();
        var date = faker.Date;
        var open = faker.Open;
        var high = faker.High;
        var low = faker.Low;
        var close = faker.Close;
        var adjClose = faker.AdjClose;
        var volume = faker.Volume;

        // Act
        var historyPrice = new MagazineLuizaHistoryPrice
        {
            Date = date,
            Open = open,
            High = high,
            Low = low,
            Close = close,
            AdjClose = adjClose,
            Volume = volume
        };

        // Assert
        Assert.NotNull(historyPrice);
        Assert.Equal(date, historyPrice.Date);
        Assert.Equal(open, historyPrice.Open);
        Assert.Equal(high, historyPrice.High);
        Assert.Equal(low, historyPrice.Low);
        Assert.Equal(close, historyPrice.Close);
        Assert.Equal(adjClose, historyPrice.AdjClose);
        Assert.Equal(volume, historyPrice.Volume);
    }
}
