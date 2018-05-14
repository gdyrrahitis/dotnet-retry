namespace DotNetRetry.Integration.Tests.Rules.SequentialTests.Function
{
    using System;
    using System.Diagnostics;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using Xunit;
    using static Xunit.Assert;

    public class Attempt
    {
        private readonly RuleOptions _options;

        public Attempt()
        {
            _options = Rule.Setup(Strategy.Sequential);
        }

        [Theory]
        [InlineData(0, 3, 100, "Success")]
        [InlineData(1, 3, 100, "Success")]
        [InlineData(2, 3, 100, "Success")]
        public void IsSuccessOnVariousRetryingScenarios(int whenSuccessful, int totalAttempts, 
            int milliseconds, string returnValue)
        {
            // Arrange
            var attempt = 0;
            var rule = _options.Config(options =>
            {
                options.Attempts = totalAttempts;
                options.Time = TimeSpan.FromMilliseconds(milliseconds);
            });
            var sequential = new Sequential(rule);
            Func<string> function = () =>
            {
                if (whenSuccessful == attempt)
                {
                    return returnValue;
                }

                attempt += 1;
                throw new Exception("Retry");
            };

            // Act
            var result = sequential.Attempt(function);

            // Assert
            Equal(whenSuccessful, attempt);
            Equal(returnValue, result);
        }

        [Fact]
        public void FailureAfterAllTriesReturnsAggregateExceptionWithAllTheExceptionsOccurred()
        {
            // Arrange
            var tries = 0;
            var rule = _options.Config(options =>
            {
                options.Attempts = 3;
                options.Time = TimeSpan.FromMilliseconds(100);
            });
            var sequential = new Sequential(rule);

            Func<string> function = () =>
            {
                tries++;
                throw new Exception("Unhandled exception");
            };

            // Act
            var exception = Throws<AggregateException>(() => sequential.Attempt(function));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
        }

        [Fact]
        public void TakesTwoHundredMillisecondsToCompleteAfterThreeRetriesOneSecondEach()
        {
            // Arrange
            var rule = _options.Config(options =>
            {
                options.Attempts = 3;
                options.Time = TimeSpan.FromMilliseconds(100);
            });
            var sequential = new Sequential(rule);
            var stopwatch = Stopwatch.StartNew();
            Func<string> function = () =>
            {
                throw new Exception("Unhandled exception");
            };

            // Act
            stopwatch.Start();
            Throws<AggregateException>(() => sequential.Attempt(function));
            stopwatch.Stop();

            // Assert
            var elapsed = stopwatch.Elapsed;
            True(elapsed.TotalMilliseconds - 200 < 50, $"Failed as it took {elapsed.TotalMilliseconds - 200}");
        }
    }
}