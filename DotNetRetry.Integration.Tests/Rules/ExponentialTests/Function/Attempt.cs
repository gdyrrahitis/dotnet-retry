namespace DotNetRetry.Integration.Tests.Rules.ExponentialTests.Function
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
        [InlineData(0, 3, 100, "Success")]
        [InlineData(1, 3, 100, "Success")]
        [InlineData(2, 3, 100, "Success")]
        public void IsSuccessOnVariousRetryingScenarios(int whenSuccessful, int totalAttempts,
            int milliseconds, string returnValue)
        {
            // Arrange
            var attempt = 0;
            var rule = _options.Config(new Options(totalAttempts, TimeSpan.FromMilliseconds(milliseconds)));
            var exponential = new Exponential(rule);
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
            var result = exponential.Attempt(function);

            // Assert
            Equal(whenSuccessful, attempt);
            Equal(returnValue, result);
        }

        [Fact]
        public void FailureAfterAllTriesReturnsAggregateExceptionWithAllTheExceptionsOccurred()
        {
            // Arrange
            var tries = 0;
            var rule = _options.Config(new Options(3, TimeSpan.FromMilliseconds(1)));
            var exponential = new Exponential(rule);

            Func<string> function = () =>
            {
                tries++;
                throw new Exception("Unhandled exception");
            };

            // Act
            var exception = Throws<AggregateException>(() => exponential.Attempt(function));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
        }
    }
}