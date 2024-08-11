using FluentAssertions;
using LegacyApp.Utils;
using Xunit;

namespace LegacyApp.Tests.Unit.Utils;

public class DateUtilsTests
{
    [Xunit.Theory]
    [InlineData("1992-01-01", 32)]
    [InlineData("2014-01-01", 10)]
    public void CalculateAge_WhenInformedDate_ShouldReturnExpectedAge(string birthDateString, int expectedAge)
    {
        // Arrange
        var birthDate = DateTime.Parse(birthDateString);
        
        // Act
        var result = DateUtils.CalculateAge(birthDate);
        
        // Arrange
        result.Should().Be(expectedAge);
    }
}