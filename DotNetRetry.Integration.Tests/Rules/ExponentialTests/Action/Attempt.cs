namespace DotNetRetry.Integration.Tests.Rules.ExponentialTests.Action
{
    using System;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using Xunit;
    using static Xunit.Assert;

    public class Attempt
    {
        private readonly RuleOptions _options;

        public Attempt()
        {
            _options = Rule.Setup(Strategy.Exponential);
        }

        [Theory]
        [InlineData(0, 10, 10, "Success")]
        [InlineData(1, 10, 10, "Success")]
        [InlineData(2, 10, 10, "Success")]
        [InlineData(3, 10, 10, "Success")]
        public void IsSuccessOnVariousRetryingScenarios(int whenSuccessful, int totalAttempts,
        int seconds, string returnValue)
        {
            // Arrange
            var attempt = 0;
            var actual = "Not Implemented";
            var rule = _options.Config(options =>
            {
                options.Attempts = totalAttempts;
                options.Time = TimeSpan.FromSeconds(seconds);
            });
            var exponential = new Exponential(rule);
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
            exponential.Attempt(successFullAction);

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
            var rule = _options.Config(options =>
            {
                options.Attempts = 3;
                options.Time = TimeSpan.FromMilliseconds(100);
            });
            var exponential = new Exponential(rule);
            Action failureAction = () =>
            {
                tries++;
                const string invalidNumber = "123abc";
                actual = int.Parse(invalidNumber);
            };

            // Act
            var exception = Throws<AggregateException>(() => exponential.Attempt(failureAction));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
            Equal(0, actual);
        }
    }
}