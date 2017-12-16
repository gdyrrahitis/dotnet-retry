namespace DotNetRetry.Tests.Rules.RetryRuleTests
{
    using System;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class OnAfterRetry
    {
        [Theory]
        [InlineData(Rule.Sequential)]
        [InlineData(Rule.Exponential, Skip = "Not implemented")]
        public void ReturnsSelf(Rule input)
        {
            // Arrange 
            var rule = RetryRule.SetupRules(input);

            // Act
            var result = rule.OnAfterRetry((sender, args) => { });

            // Assert
            Same(rule, result);
        }

        [Theory]
        [InlineData(Rule.Sequential)]
        [InlineData(Rule.Exponential, Skip = "Not implemented")]
        public void EventShouldBeRaisedAfterTheRetry(Rule input)
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules(input).OnAfterRetry((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [InlineData(Rule.Sequential)]
        [InlineData(Rule.Exponential, Skip = "Not implemented")]
        public void EventShouldNotBeRaisedAsNoRetriesWherePerformed(Rule input)
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules(input).OnAfterRetry((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() => { throw new Exception("Custom exception"); }, 1, 
                TimeSpan.FromSeconds(1)));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [InlineData(Rule.Sequential)]
        [InlineData(Rule.Exponential, Skip = "Not implemented")]
        public void EventShouldNotBeDispatchedByAnotherRule(Rule input)
        {
            // Arrange
            var dispatched = false;
            RetryRule.SetupRules(input).OnAfterRetry((sender, args) => dispatched = true);

            // Act
            RetryRule.SetupRules(input).Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}