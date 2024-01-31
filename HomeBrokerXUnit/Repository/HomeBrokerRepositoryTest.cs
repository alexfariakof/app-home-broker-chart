using Domain.Charts.Agreggates;
using Domain.Charts.ValueObject;

namespace Repository;
public class HomeBrokerRepositoryTest
{
    [Fact]
    public void Should_Returns_HistoryData_GetHistoryData()
    {
        // Arrange
        var mockHomeBrokerRepository = new HomeBrokerRepository();
        var period = new Period(new DateTime(2023, 01,01), new DateTime(2024, 01, 01));

        // Act
        var result = mockHomeBrokerRepository.GetHistoryData(period).Result;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<MagazineLuizaHistoryPrice>>(result);
        Assert.False(result.Count == 365);
        Assert.Equal(248, result.Count);        
    }
}
