using Domain.Charts.Agreggates;
using Domain.Charts.Agreggates.Factory;
using Domain.Charts.ValueObject;
using HomeBroker.Domain.Charts.Agreggates.Factory;
using Moq;

namespace Repository;
public sealed class HomeBrokerRepositoryTest
{
    [Fact]
    public async Task Should_Returns_HistoryData_GetHistoryData()
    {
        // Arrange
        var mock = new MagazineLuizaHistoryPriceFactory();
        var mockHomeBrokerRepository = new HomeBrokerRepository(mock);
        var period = new Period(new DateTime(2023, 01, 01), new DateTime(2024, 01, 01));

        // Act
        var result = await mockHomeBrokerRepository.GetHistoryData(period);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<MagazineLuizaHistoryPrice>>(result);
        Assert.False(result.Count == 365);
        Assert.Equal(248, result.Count);        
    }
}