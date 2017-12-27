namespace DotNetRetry.Unit.Tests.Rules.RuleTests
{
    using System;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class OnAfterRetry
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsSelf(Strategies input)
        {
            // Arrange 
            var rule = Rule.SetupRules(input);

            // Act
            var result = rule.OnAfterRetry((sender, args) => { });

            // Assert
            Same(rule, result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedAfterTheRetryForNonReturnableMethod(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input).OnAfterRetry((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedAfterTheRetryForReturnableMethod(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input).OnAfterRetry((sender, args) => dispatched = true);

            // Act
            var result = rule.Attempt(() => "Return value", 1, TimeSpan.FromSeconds(1));

            // Assert
            True(dispatched, "Event should be dispatched");
            Equal("Return value", result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeRaisedAsNoRetriesWherePerformedForNonReturnableMethod(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input).OnAfterRetry((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() => { throw new Exception("Custom exception"); }, 1, 
                TimeSpan.FromSeconds(1)));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeRaisedAsNoRetriesWherePerformedForReturnableMethod(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input).OnAfterRetry((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() =>
            {
                throw new Exception("Custom exception");
#pragma warning disable 162
                return "Return value";
#pragma warning restore 162
            }, 1,
                TimeSpan.FromSeconds(1)));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForNonReturnableMethod(Strategies input)
        {
            // Arrange
            var dispatched = false;
            Rule.SetupRules(input).OnAfterRetry((sender, args) => dispatched = true);

            // Act
            Rule.SetupRules(input).Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForReturnableMethod(Strategies input)
        {
            // Arrange
            var dispatched = false;
            Rule.SetupRules(input).OnAfterRetry((sender, args) => dispatched = true);

            // Act
            Rule.SetupRules(input).Attempt(() => "Return value", 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}