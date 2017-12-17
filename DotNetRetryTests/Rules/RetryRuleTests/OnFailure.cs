namespace DotNetRetry.Tests.Rules.RetryRuleTests
{
    using System;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class OnFailure
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsSelf(Rule input)
        {
            // Arrange 
            var rule = RetryRule.SetupRules(input);

            // Act
            var result = rule.OnFailure((sender, args) => { });

            // Assert
            Same(rule, result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeDispatchedWhenFailureOccursForAction(Rule input)
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() => { throw new Exception("Custom Exception"); }, 1,
                TimeSpan.FromSeconds(1)));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeDispatchedWhenFailureOccursForFunction(Rule input)
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() =>
            {
                throw new Exception("Custom Exception");
            }, 1, TimeSpan.FromSeconds(1)));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForAction(Rule input)
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForFunction(Rule input)
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => "Function Invocation", 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForAction(Rule input)
        {
            // Arrange
            var dispatched = false;
            RetryRule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

            // Act
            RetryRule.SetupRules(input).Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForFunction(Rule input)
        {
            // Arrange
            var dispatched = false;
            RetryRule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

            // Act
            RetryRule.SetupRules(input).Attempt(() => "Function Invocation", 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}