namespace DotNetRetry.Tests.Rules.SequentialTests.Action
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

        [Theory]
        [InlineData(0, 3, 100, "Success")]
        [InlineData(1, 3, 100, "Success")]
        [InlineData(2, 3, 100, "Success")]
        public void IsSuccessOnVariousRetryingScenarios(int whenSuccessful, int totalAttempts, 
            int milliseconds, string returnValue)
        {
            // Arrange
            var actual = "Not Implemented";
            var attempt = 0;
            Action successFullAction = () =>
            {
                if (whenSuccessful == attempt)
                {
                    actual = returnValue;
                    return;
                }

                attempt += 1;
                throw new Exception("Retry");
            };

            // Act
            _rule.Attempt(successFullAction, totalAttempts, TimeSpan.FromMilliseconds(milliseconds));

            // Assert
            Equal(whenSuccessful, attempt);
            Equal(actual, returnValue);
        }

        [Fact]
        public void FailureAfterAllTriesReturnsAggregateExceptionWithAllTheExceptionsOccurred()
        {
            // Arrange
            var actual = 0;
            var tries = 0;
            Action failureAction = () =>
            {
                tries++;
                const string invalidNumber = "123abc";
                actual = int.Parse(invalidNumber);
            };

            // Act
            var exception = Throws<AggregateException>(() => _rule.Attempt(failureAction, 3, TimeSpan.FromSeconds(1)));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
            Equal(0, actual);
        }

        [Fact]
        public void TakesTwoSecondsToCompleteAfterThreeRetriesOneSecondEach()
        {
            // Arrange
            var stopwatch = Stopwatch.StartNew();
            Action action = () =>
            {
                throw new Exception("Unhandled exception");
            };

            // Act
            stopwatch.Start();
            Throws<AggregateException>(() => _rule.Attempt(action, 3, TimeSpan.FromSeconds(1)));
            stopwatch.Stop();
            var elapsed = stopwatch.Elapsed;

            // Assert
            Equal(2, elapsed.Seconds);
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionForTriesBeingLessThanOne()
        {
            // Arrange | Act
            var exception = Throws<ArgumentOutOfRangeException>(() => _rule.Attempt(() => { }, 0, TimeSpan.FromSeconds(1)));

            // Assert
            Equal("Argument value <0> is less than <1>.\r\nParameter name: attempts", exception.Message);
        }

        [Fact]
        public void ThrowsArgumentExceptionForTimespanBeingZero()
        {
            // Arrange | Act
            var exception = Throws<ArgumentOutOfRangeException>(() => _rule.Attempt(() => { }, 3, TimeSpan.Zero));

            // Assert
            Equal($"Argument value <{TimeSpan.Zero}> is less than or equal to <{TimeSpan.Zero}>.\r\nParameter name: timeBetweenRetries", exception.Message);
        }

        [Fact]
        public void ThrowsArgumentExceptionForTimespanBeingMinValue()
        {
            // Arrange | Act
            var exception = Throws<ArgumentOutOfRangeException>(() => _rule.Attempt(() => { }, 3, TimeSpan.MinValue));

            // Assert
            Equal($"Argument value <{TimeSpan.MinValue}> is less than or equal to <{TimeSpan.Zero}>.\r\nParameter name: timeBetweenRetries", exception.Message);
        }
    }
}
