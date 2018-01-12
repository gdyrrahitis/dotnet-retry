namespace DotNetRetry.Integration.Tests.Rules.SequentialTests.Action
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
            var actual = "Not Implemented";
            var attempt = 0;
            var rule = _options.Config(new Options(totalAttempts, TimeSpan.FromMilliseconds(milliseconds)));
            var sequential = new Sequential(rule);
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
            sequential.Attempt(successFullAction);

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
            var rule = _options.Config(new Options(3, TimeSpan.FromMilliseconds(1)));
            var sequential = new Sequential(rule);
            Action failureAction = () =>
            {
                tries++;
                const string invalidNumber = "123abc";
                actual = int.Parse(invalidNumber);
            };

            // Act
            var exception = Throws<AggregateException>(() => sequential.Attempt(failureAction));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
            Equal(0, actual);
        }

        [Fact]
        public void TakesTwoHundredMillisecondsToCompleteAfterThreeRetriesOneHundredMillisecondsEach()
        {
            // Arrange
            var rule = _options.Config(new Options(3, TimeSpan.FromMilliseconds(100)));
            var sequential = new Sequential(rule);
            var stopwatch = Stopwatch.StartNew();
            Action action = () =>
            {
                throw new Exception("Unhandled exception");
            };

            // Act
            stopwatch.Start();
            Throws<AggregateException>(() => sequential.Attempt(action));
            stopwatch.Stop();
            var elapsed = stopwatch.Elapsed;

            // Assert
            True(elapsed.TotalMilliseconds - 200 < 50);
        }
    }
}
