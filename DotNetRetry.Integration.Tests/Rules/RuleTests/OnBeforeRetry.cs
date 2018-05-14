namespace DotNetRetry.Integration.Tests.Rules.RuleTests
{
    using System;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Tests.Common;
    using Xunit;
    using static Xunit.Assert;

    public class OnBeforeRetry
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsSelf(Strategy input)
        {
            // Arrange 
            var rule = Rule.Setup(input).Config(options =>
            {
                options.Attempts = 1;
                options.Time = TimeSpan.FromMilliseconds(1);
            });

            // Act
            var result = rule.OnBeforeRetry((sender, args) => { });

            // Assert
            Same(rule, result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventNotShouldBeRaisedBeforeExecutionOfAttemptedMethodForNonReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { });

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventNotShouldBeRaisedBeforeExecutionOfAttemptedMethodForReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            var result = rule.Attempt(() => "Return value");

            // Assert
            False(dispatched, "Event should not be dispatched");
            Equal("Return value", result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedBeforeRetryForNonReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 2;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnBeforeRetry((sender, args) =>
                {
                    dispatched = true;
                });

            // Act
            Throws<AggregateException>(() => rule.Attempt(() => { throw new Exception("Custom exception"); }));

            // Assert
            Console.WriteLine($"This is the failed one ({input}) - OnBeforeRetry.EventShouldBeRaisedBeforeRetryForNonReturnableMethod");
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedBeforeRetryForReturnableMethod(Strategy input)
        {
            // Arrange
            var count = 0;
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 2;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            var result = rule.Attempt(() =>
            {
                if (count++ == 0)
                {
                    throw new Exception("Custom Exception");
                }

                return "Return value";
            });

            // Assert
            True(dispatched, "Event should be dispatched");
            Equal("Return value", result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForNonReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .Attempt(() => { });

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .Attempt(() => "Return value");

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}