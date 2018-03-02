namespace DotNetRetry.Unit.Tests.Rules.Configuration.OptionsTests
{
    using System;
    using DotNetRetry.Rules.Configuration;
    using Xunit;
    using static Xunit.Assert;

    public class Constructor
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void ThrowsArgumentOutOfRangeExceptionForTriesBeingLessThanOne(int value)
        {
            // Arrange | Act
            var exception = Throws<ArgumentOutOfRangeException>(() => new Options(value));

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
            var exception = Throws<ArgumentOutOfRangeException>(() => new Options(value, 
                TimeSpan.FromMilliseconds(1)));

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
                    () => new Options(TimeSpan.Zero));

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
                    () => new Options(10, TimeSpan.Zero));

            // Assert
            Equal($"Argument value <{TimeSpan.Zero}> is less than or equal to <{TimeSpan.Zero}>.{Environment.NewLine}Parameter name: timeBetweenRetries",
                result.Message);
        }

        [Fact]
        public void SuccessfullyForConstructorWithTwoArguments()
        {
            // Arrange | Act
            var result = new Options(1, TimeSpan.FromSeconds(1));

            // Assert
            Equal(1, result.Attempts);
            Equal(1, result.Time.TotalSeconds);
        }

        [Fact]
        public void SuccessfullyForConstructorWithIntArgument()
        {
            // Arrange | Act
            var result = new Options(1);

            // Assert
            Equal(1, result.Attempts);
            Equal(TimeSpan.Zero, result.Time);
        }

        [Fact]
        public void SuccessfullyForConstructorWithTimespanArgument()
        {
            // Arrange | Act
            var result = new Options(TimeSpan.FromSeconds(1));

            // Assert
            Equal(1, result.Time.TotalSeconds);
            Equal(0, result.Attempts);
        }
    }
}