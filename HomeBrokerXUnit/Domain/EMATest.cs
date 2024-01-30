using Domain.Charts.ValueObject;
namespace Domain;
public class EMATest
{
    [Fact]
    public void Should_Calculates_EMA_Correctly()
    {
        // Arrange
        List<decimal> historyPriceData = new List<decimal> {
            22.27m, 22.19m, 22.08m, 22.17m, 22.18m, 22.13m, 22.23m, 22.43m, 22.24m, 22.29m, 22.15m, 22.39m, 22.38m, 22.61m,23.36m, 
            24.05m, 23.75m, 23.83m, 23.95m, 23.63m, 23.82m, 23.87m, 23.65m, 23.19m, 23.10m, 23.33m, 22.68m, 23.10m, 22.40m, 22.17m 
        };

        List<decimal> expectedEMA = new List<decimal> {
            22.22m, 22.21m, 22.24m, 22.27m, 22.33m, 22.52m, 22.80m, 22.97m, 23.13m, 23.28m, 23.34m, 23.43m, 23.51m, 23.53m, 23.47m, 
            23.40m, 23.39m, 23.26m, 23.23m, 23.08m, 22.92m
        };

        // Act
        var ema = new Ema(historyPriceData, 10);

        // Assert
        for(int i=0;i < ema.Values.Count;i++)
        {
            Assert.Equal(expectedEMA[i], Math.Round(ema.Values[i], 2));
        }        
    }

    [Fact]
    public void Should_Throws_Exception_When_Calculates_EMA()
    {
        // Arrange
        List<decimal> historyPriceData = new List<decimal> { 22.22m, 22.21m, 22.24m, 22.27m, };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Ema(historyPriceData, 10));
        Assert.Equal("Não há dados suficientes para gerar uma EMA.", exception.Message);
    }
}
