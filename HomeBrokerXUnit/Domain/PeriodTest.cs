using Domain.Charts.ValueObject;

namespace Domain;
public class PeriodTest
{
    [Fact]
    public void ImplicitConversion_FromTupleToPeriod_ShouldSucceed()
    {
        // Arrange
        var tuple = (new DateTime(2022, 1, 1), new DateTime(2022, 1, 5));

        // Act
        Period period = tuple;

        // Assert
        Assert.Equal(tuple.Item1, period.StartDate);
        Assert.Equal(tuple.Item2, period.EndDate);
    }

    [Fact]
    public void Days_Property_ShouldReturnCorrectValue()
    {
        // Arrange
        var fakePeriod = new Period(new DateTime(2022, 1, 1), new DateTime(2022, 1, 5));

        // Act
        int days = fakePeriod.Days;

        // Assert
        Assert.Equal(4, days);
    }
}
