namespace DotNetRetry.Tests.Rules.RetryRuleTests.Function
{
    using System;
    using System.Diagnostics;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class Attempt
    {
        [Theory]
        [InlineData(Rule.Sequential)]
        [InlineData(Rule.Exponential, Skip = "Not implemented")]
        public void ReturnsListOfExceptionsAfterFailedInAllTries(Rule input)
        {
            // Arrange
            var tries = 0;
            var rules = RetryRule.SetupRules(input);
            Func<string> function = () =>
            {
                tries++;
                throw new Exception("Unhandled exception");
            };

            // Act
            var exception = Throws<AggregateException>(() => rules.Attempt(function, 3, TimeSpan.FromSeconds(1)));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
        }

        [Theory]
        [InlineData(Rule.Sequential)]
        [InlineData(Rule.Exponential, Skip = "Not implemented")]
        public void TakesTwoSecondsToCompleteAfterThreeRetriesOneSecondEach(Rule input)
        {
            // Arrange
            var stopwatch = Stopwatch.StartNew();
            var rules = RetryRule.SetupRules(input);
            Func<string> function = () =>
            {
                throw new Exception("Unhandled exception");
            };

            // Act
            stopwatch.Start();
            Throws<AggregateException>(() => rules.Attempt(function, 3, TimeSpan.FromSeconds(1)));
            stopwatch.Stop();

            // Assert
            var elapsed = stopwatch.Elapsed;
            Equal(2, elapsed.Seconds);
        }

        [Theory]
        [InlineData(Rule.Sequential)]
        [InlineData(Rule.Exponential, Skip = "Not implemented")]
        public void FailsTheFirstTimeButSucceedsOnSecondTryReturningStringValue(Rule input)
        {
            // Arrange
            var tries = 0;
            var rules = RetryRule.SetupRules(input);
            Func<string> function = () =>
            {
                if (tries++ < 1)
                {
                    throw new Exception("Unhandled exception");
                }

                return "abc";
            };

            // Act
            var result = rules.Attempt(function, 3, TimeSpan.FromSeconds(1));

            // Assert
            Equal(2, tries);
            Equal("abc", result);
        }

        [Theory]
        [InlineData(Rule.Sequential)]
        [InlineData(Rule.Exponential, Skip = "Not implemented")]
        public void FailsTheSecondTimeButSucceedsOnThirdTryReturningStringValue(Rule input)
        {
            // Arrange
            var tries = 0;
            var rules = RetryRule.SetupRules(input);
            Func<string> function = () =>
            {
                if (tries++ < 2)
                {
                    throw new Exception("Unhandled exception");
                }

                return "abc";
            };

            // Act
            var result = rules.Attempt(function, 3, TimeSpan.FromSeconds(1));

            // Assert
            Equal(3, tries);
            Equal("abc", result);
        }

        [Theory]
        [InlineData(Rule.Sequential)]
        [InlineData(Rule.Exponential, Skip = "Not implemented")]
        public void ThrowsArgumentOutOfRangeExceptionForTriesBeingLessThanOne(Rule input)
        {
            // Arrange
            var rules = RetryRule.SetupRules(input);

            // Act
            var exception = Throws<ArgumentOutOfRangeException>(() => rules.Attempt(() => "abc", 0, TimeSpan.FromSeconds(1)));

            // Assert
            Equal("", exception.Message);
        }

        [Theory]
        [InlineData(Rule.Sequential)]
        [InlineData(Rule.Exponential, Skip = "Not implemented")]
        public void ThrowsArgumentExceptionForTimespanBeingZero(Rule input)
        {
            // Arrange
            var rules = RetryRule.SetupRules(input);

            // Act
            var exception = Throws<ArgumentException>(() => rules.Attempt(() => "abc", 3, TimeSpan.Zero));

            // Assert
            Equal("", exception.Message);
        }

        [Theory]
        [InlineData(Rule.Sequential)]
        [InlineData(Rule.Exponential, Skip = "Not implemented")]
        public void ThrowsArgumentExceptionForTimespanBeingMinValue(Rule input)
        {
            // Arrange
            var rules = RetryRule.SetupRules(input);

            // Act
            var exception = Throws<ArgumentException>(() => rules.Attempt(() => "abc", 3, TimeSpan.MinValue));

            // Assert
            Equal("", exception.Message);
        }
    }
}