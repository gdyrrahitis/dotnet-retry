namespace DotNetRetry.Integration.Tests.Rules.RuleTests
{
    using System;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Tests.Common;
    using Xunit;
    using static Xunit.Assert;

    public class OnAfterRetry
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsSelf(Strategy input)
        {
            // Arrange 
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 3;
                    options.Time = TimeSpan.FromMilliseconds(1);
                });

            // Act
            var result = rule.OnAfterRetry((sender, args) => { });

            // Assert
            Same(rule, result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeRaisedBecauseThereWasNoRetryForNonReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 3;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnAfterRetry((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { });

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeRaisedBecauseThereWasNoRetryForReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 3;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnAfterRetry((sender, args) => dispatched = true);

            // Act
            var result = rule.Attempt(() => "Return value");

            // Assert
            False(dispatched, "Event should be dispatched");
            Equal("Return value", result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedAsRetriesWherePerformedForNonReturnableMethod(Strategy input)
        {
            // Arrange
            var count = 0;
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 3;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnAfterRetry((sender, args) =>
                {
                    count++;
                    dispatched = true;
                });

            // Act
            Throws<AggregateException>(() => rule.Attempt(() => { throw new Exception("Custom exception"); }));

            // Assert
            True(dispatched, "Event should be dispatched");
            Equal(3, count);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedAsRetriesWherePerformedForReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 3;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnAfterRetry((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() =>
            {
                throw new Exception("Custom exception");
#pragma warning disable 162
                return "Return value";
#pragma warning restore 162
            }));

            // Assert
            True(dispatched, "Event should be dispatched");
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
                    options.Attempts = 3;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnAfterRetry((sender, args) => dispatched = true);

            // Act
            Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 3;
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
                    options.Attempts = 3;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnAfterRetry((sender, args) => dispatched = true);

            // Act
            Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 3;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .Attempt(() => "Return value");

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}