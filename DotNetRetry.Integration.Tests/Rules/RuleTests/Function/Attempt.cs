namespace DotNetRetry.Integration.Tests.Rules.RuleTests.Function
{
    using System;
    using System.Diagnostics;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Tests.Common;
    using Xunit;
    using static Xunit.Assert;

    public class Attempt
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsListOfExceptionsAfterFailedInAllTries(Strategy input)
        {
            // Arrange
            var tries = 0;
            var rules = Rule.Setup(input)
                .Config(new Options(3, TimeSpan.FromMilliseconds(100)));
            Func<string> function = () =>
            {
                tries++;
                throw new Exception("Unhandled exception");
            };

            // Act
            var exception = Throws<AggregateException>(() => rules.Attempt(function));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void TakesTwoHundredMillisecondsToCompleteAfterThreeRetriesOneSecondEach(Strategy input)
        {
            // Arrange
            var stopwatch = Stopwatch.StartNew();
            var rules = Rule.Setup(input)
                .Config(new Options(3, TimeSpan.FromMilliseconds(100)));
            Func<string> function = () =>
            {
                throw new Exception("Unhandled exception");
            };

            // Act
            stopwatch.Start();
            Throws<AggregateException>(() => rules.Attempt(function));
            stopwatch.Stop();

            // Assert
            var elapsed = stopwatch.Elapsed;
            True(elapsed.TotalMilliseconds - 200 < 50);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void FailsTheFirstTimeButSucceedsOnSecondTryReturningStringValue(Strategy input)
        {
            // Arrange
            var tries = 0;
            var rules = Rule.Setup(input)
                .Config(new Options(3, TimeSpan.FromMilliseconds(100)));
            Func<string> function = () =>
            {
                if (tries++ < 1)
                {
                    throw new Exception("Unhandled exception");
                }

                return "abc";
            };

            // Act
            var result = rules.Attempt(function);

            // Assert
            Equal(2, tries);
            Equal("abc", result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void FailsTheSecondTimeButSucceedsOnThirdTryReturningStringValue(Strategy input)
        {
            // Arrange
            var tries = 0;
            var rules = Rule.Setup(input)
                .Config(new Options(3, TimeSpan.FromMilliseconds(100)));
            Func<string> function = () =>
            {
                if (tries++ < 2)
                {
                    throw new Exception("Unhandled exception");
                }

                return "abc";
            };

            // Act
            var result = rules.Attempt(function);

            // Assert
            Equal(3, tries);
            Equal("abc", result);
        }
    }
}