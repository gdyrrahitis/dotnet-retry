namespace DotNetRetry.Unit.Tests.Rules.Configuration.OptionsTests
{
    using System;
    using DotNetRetry.Rules.Configuration;
    using Xunit;
    using static Xunit.Assert;

    public class Properties
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void ThrowsArgumentOutOfRangeExceptionForTriesBeingLessThanOne(int value)
        {
            // Arrange | Act
            var exception = Throws<ArgumentOutOfRangeException>(() => new Options { Attempts = value });

            // Assert
            Equal($"Argument value <{value}> is less than <1>.{Environment.NewLine}Parameter name: attempts",
                exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void ThrowsArgumentOutOfRangeExceptionForTriesBeingLessThanOneForConstructorWithTimespan(int value)
        {
            // Arrange | Act
            var exception = Throws<ArgumentOutOfRangeException>(() => new Options
            {
                Attempts = value,
                Time = TimeSpan.FromMilliseconds(1)
            });

            // Assert
            Equal($"Argument value <{value}> is less than <1>.{Environment.NewLine}Parameter name: attempts",
                exception.Message);
        }

        [Fact]
        public void ThrowsArgumentExceptionForTimespanBeingZero()
        {
            // Arrange | Act
            var result =
                Throws<ArgumentOutOfRangeException>(
                    () => new Options { Time = TimeSpan.Zero });

            // Assert
            Equal($"Argument value <{TimeSpan.Zero}> is less than or equal to <{TimeSpan.Zero}>.{Environment.NewLine}Parameter name: timeBetweenRetries",
                result.Message);
        }

        [Fact]
        public void ThrowsArgumentExceptionForTimespanBeingZeroForConstructorWithInt()
        {
            // Arrange | Act
            var result =
                Throws<ArgumentOutOfRangeException>(
                    () => new Options {Attempts = 10, Time = TimeSpan.Zero });

            // Assert
            Equal($"Argument value <{TimeSpan.Zero}> is less than or equal to <{TimeSpan.Zero}>.{Environment.NewLine}Parameter name: timeBetweenRetries",
                result.Message);
        }

        [Fact]
        public void Successfully()
        {
            // Arrange | Act
            var result = new Options
            {
                Attempts = 2,
                Time = TimeSpan.FromSeconds(10)
            };

            // Assert
            Equal(2, result.Attempts);
            Equal(10, result.Time.TotalSeconds);
        }
    }
}