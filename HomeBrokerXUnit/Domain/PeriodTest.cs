﻿using Domain.Charts.ValueObject;

namespace Domain;
public sealed class PeriodTest
{
    [Fact]
    public void Should_Implicit_Conversion_From_Tuple_To_Period_With_Succeess()
    {
        // Arrange
        var tuple = (new DateTime(2022, 1, 1), new DateTime(2022, 1, 10));

        // Act
        Period period = tuple;

        // Assert
        Assert.Equal(tuple.Item1, period.StartDate);
        Assert.Equal(tuple.Item2, period.EndDate);
    }

    [Fact]
    public void Should_Returns_Days_Property__Correctly()
    {
        // Arrange
        var fakePeriod = new Period(new DateTime(2022, 1, 1), new DateTime(2022, 1, 10));

        // Act
        int days = fakePeriod.Days;

        // Assert
        Assert.Equal(9, days);
    }

    [Fact]
    public void Should_Throws_ArgumentException_Validate_StartDate_EndDate_Order_StartDate_Greater_Than_EndDate()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Period(new DateTime(2022, 1, 10), new DateTime(2022, 1, 1)));
    }

    [Fact]
    public void Should_Throws_ArgumentException_Validate_DateRange_MinimumDate_Violation()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Period(new DateTime(2001, 1, 1), new DateTime(2001, 1, 10)));
    }

    [Fact]
    public void Should_Throws_ArgumentException_Validate_DateRange_Interval_Less_Than_FiveDays()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Period(new DateTime(2022, 1, 1), new DateTime(2022, 1, 4)));
    }

    
}
