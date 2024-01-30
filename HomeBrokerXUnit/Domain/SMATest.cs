using Domain.Charts.ValueObject;
namespace Domain;
public class SMATest
{
    [Fact]
    public void Should_Calculates_SMA_Correctly()
    {
        // Arrange
        List<decimal> historyPriceData = new List<decimal> { 11, 12, 13, 14, 15, 16, 17 };

        // Act
        var sma = new Sma(historyPriceData, 5);

        // Assert
        Assert.Equal(13, sma.Values[0]);
        Assert.Equal(14, sma.Values[1]);
        Assert.Equal(15, sma.Values[2]);
        Assert.Equal(12.4m, sma.Values[3]);
        Assert.Equal(9.6m, sma.Values[4]);
        Assert.Equal(6.6m, sma.Values[5]);
        Assert.Equal(3.4m, sma.Values[6]);
    }

    [Fact]
    public void Should_Throws_ArgumentException_When_Calculates_SMA()
    {
        // Arrange
        List<decimal> historyPriceData = new List<decimal> { 11, 12, 13, 14 };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Sma(historyPriceData));
        Assert.Equal("Não há dados suficiente para gerar uma SMA.", exception.Message);
    }
}