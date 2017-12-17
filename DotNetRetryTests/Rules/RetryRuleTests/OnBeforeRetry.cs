namespace DotNetRetry.Tests.Rules.RetryRuleTests
{
    using System;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class OnBeforeRetry
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsSelf(Rule input)
        {
            // Arrange 
            var rule = RetryRule.SetupRules(input);

            // Act
            var result = rule.OnBeforeRetry((sender, args) => { });

            // Assert
            Same(rule, result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedBeforeExecutionOfAttemptedMethod(Rule input)
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules(input).OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRule(Rule input)
        {
            // Arrange
            var dispatched = false;
            RetryRule.SetupRules(input).OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            RetryRule.SetupRules(input).Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}