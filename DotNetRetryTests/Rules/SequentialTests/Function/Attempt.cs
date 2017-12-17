namespace DotNetRetry.Tests.Rules.SequentialTests.Function
{
    using System;
    using System.Diagnostics;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class Attempt
    {
        private readonly Sequential _rule;

        public Attempt()
        {
            var rule = RetryRule.SetupRules(Rule.Sequential);
            _rule = new Sequential(rule);
        }

        [Fact]
        public void ReturnsListOfExceptionsAfterFailedInAllTries()
        {
            // Arrange
            var tries = 0;
            Func<string> function = () =>
            {
                tries++;
                throw new Exception("Unhandled exception");
            };

            // Act
            var exception = Throws<AggregateException>(() => _rule.Attempt(function, 3, TimeSpan.FromSeconds(1)));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
        }

        [Fact]
        public void TakesTwoSecondsToCompleteAfterThreeRetriesOneSecondEach()
        {
            // Arrange
            var stopwatch = Stopwatch.StartNew();
            Func<string> function = () =>
            {
                throw new Exception("Unhandled exception");
            };

            // Act
            stopwatch.Start();
            Throws<AggregateException>(() => _rule.Attempt(function, 3, TimeSpan.FromSeconds(1)));
            stopwatch.Stop();
            var elapsed = stopwatch.Elapsed;

            // Assert
            Equal(2, elapsed.Seconds);
        }

        [Fact]
        public void FailsTheFirstTimeButSucceedsOnSecondTryReturningStringValue()
        {
            // Arrange
            var tries = 0;
            Func<string> function = () =>
            {
                if (tries++ < 1)
                {
                    throw new Exception("Unhandled exception");
                }

                return "abc";
            };

            // Act
            var result = _rule.Attempt(function, 3, TimeSpan.FromSeconds(1));

            // Assert
            Equal(2, tries);
            Equal("abc", result);
        }

        [Fact]
        public void FailsTheSecondTimeButSucceedsOnThirdTryReturningStringValue()
        {
            // Arrange
            var tries = 0;
            Func<string> function = () =>
            {
                if (tries++ < 2)
                {
                    throw new Exception("Unhandled exception");
                }

                return "abc";
            };

            // Act
            var result = _rule.Attempt(function, 3, TimeSpan.FromSeconds(1));

            // Assert
            Equal(3, tries);
            Equal("abc", result);
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionForTriesBeingLessThanOne()
        {
            // Arrange | Act
            var exception = Throws<ArgumentOutOfRangeException>(() => _rule.Attempt(() => "abc", 0, TimeSpan.FromSeconds(1)));

            // Assert
            Equal("Argument value <0> is less than <1>.\r\nParameter name: attempts", exception.Message);
        }

        [Fact]
        public void ThrowsArgumentExceptionForTimespanBeingZero()
        {
            // Arrange | Act
            var exception = Throws<ArgumentOutOfRangeException>(() => _rule.Attempt(() => "abc", 3, TimeSpan.Zero));

            // Assert
            Equal($"Argument value <{TimeSpan.Zero}> is less than or equal to <{TimeSpan.Zero}>.\r\nParameter name: timeBetweenRetries", exception.Message);
        }

        [Fact]
        public void ThrowsArgumentExceptionForTimespanBeingMinValue()
        {
            // Arrange | Act
            var exception = Throws<ArgumentOutOfRangeException>(() => _rule.Attempt(() => "abc", 3, TimeSpan.MinValue));

            // Assert
            Equal($"Argument value <{TimeSpan.MinValue}> is less than or equal to <{TimeSpan.Zero}>.\r\nParameter name: timeBetweenRetries", exception.Message);
        }
    }
}