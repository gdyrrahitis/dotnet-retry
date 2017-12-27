namespace DotNetRetry.Unit.Tests.Core.Auxiliery.GuardsTests
{
    using System;
    using DotNetRetry.Core.Auxiliery;
    using Xunit;
    using static Xunit.Assert;

    public class ValidateArguments
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void ThrowsArgumentOutOfRangeExceptionForAttemptValuesLessThanOne(int value)
        {
            // Arrange | Act
            var result =
                Throws<ArgumentOutOfRangeException>(
                    () => Guards.ValidateArguments(value, TimeSpan.FromSeconds(1)));

            // Assert
            Equal($"Argument value <{value}> is less than <1>.\r\nParameter name: attempts", result.Message);
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionForTimespanValuesLessOrEqualToZero()
        {
            // Arrange | Act
            var result =
                Throws<ArgumentOutOfRangeException>(
                    () => Guards.ValidateArguments(10, TimeSpan.Zero));

            // Assert
            Equal($"Argument value <{TimeSpan.Zero}> is less than or equal to <{TimeSpan.Zero}>.\r\nParameter name: timeBetweenRetries", 
                result.Message);
        }
    }
}