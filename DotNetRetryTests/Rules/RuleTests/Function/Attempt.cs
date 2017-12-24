namespace DotNetRetry.Unit.Tests.Rules.RuleTests.Function
{
    using System;
    using System.Diagnostics;
    using DotNetRetry.Rules;
    using Xunit;

    public class Attempt
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsListOfExceptionsAfterFailedInAllTries(Strategies input)
        {
            // Arrange
            var tries = 0;
            var rules = Rule.SetupRules(input);
            Func<string> function = () =>
            {
                tries++;
                throw new Exception("Unhandled exception");
            };

            // Act
            var exception = Assert.Throws<AggregateException>(() => rules.Attempt(function, 3, TimeSpan.FromSeconds(1)));

            // Assert
            Assert.Equal(3, exception.InnerExceptions.Count);
            Assert.Equal(3, tries);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void TakesTwoSecondsToCompleteAfterThreeRetriesOneSecondEach(Strategies input)
        {
            // Arrange
            var stopwatch = Stopwatch.StartNew();
            var rules = Rule.SetupRules(input);
            Func<string> function = () =>
            {
                throw new Exception("Unhandled exception");
            };

            // Act
            stopwatch.Start();
            Assert.Throws<AggregateException>(() => rules.Attempt(function, 3, TimeSpan.FromSeconds(1)));
            stopwatch.Stop();

            // Assert
            var elapsed = stopwatch.Elapsed;
            Assert.Equal(2, elapsed.Seconds);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void FailsTheFirstTimeButSucceedsOnSecondTryReturningStringValue(Strategies input)
        {
            // Arrange
            var tries = 0;
            var rules = Rule.SetupRules(input);
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
            Assert.Equal(2, tries);
            Assert.Equal("abc", result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void FailsTheSecondTimeButSucceedsOnThirdTryReturningStringValue(Strategies input)
        {
            // Arrange
            var tries = 0;
            var rules = Rule.SetupRules(input);
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
            Assert.Equal(3, tries);
            Assert.Equal("abc", result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ThrowsArgumentOutOfRangeExceptionForTriesBeingLessThanOne(Strategies input)
        {
            // Arrange
            var rules = Rule.SetupRules(input);

            // Act
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => rules.Attempt(() => "abc", 0, TimeSpan.FromSeconds(1)));

            // Assert
            Assert.Equal("Argument value <0> is less than <1>.\r\nParameter name: attempts", exception.Message);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ThrowsArgumentExceptionForTimespanBeingZero(Strategies input)
        {
            // Arrange
            var rules = Rule.SetupRules(input);

            // Act
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => rules.Attempt(() => "abc", 3, TimeSpan.Zero));

            // Assert
            Assert.Equal($"Argument value <{TimeSpan.Zero}> is less than or equal to <{TimeSpan.Zero}>.\r\nParameter name: timeBetweenRetries", exception.Message);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ThrowsArgumentExceptionForTimespanBeingMinValue(Strategies input)
        {
            // Arrange
            var rules = Rule.SetupRules(input);

            // Act
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => rules.Attempt(() => "abc", 3, TimeSpan.MinValue));

            // Assert
            Assert.Equal($"Argument value <{TimeSpan.MinValue}> is less than or equal to <{TimeSpan.Zero}>.\r\nParameter name: timeBetweenRetries", exception.Message);
        }
    }
}