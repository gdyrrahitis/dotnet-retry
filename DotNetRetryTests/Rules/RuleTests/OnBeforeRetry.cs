namespace DotNetRetry.Unit.Tests.Rules.RuleTests
{
    using System;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class OnBeforeRetry
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsSelf(Strategies input)
        {
            // Arrange 
            var rule = Rule.SetupRules(input);

            // Act
            var result = rule.OnBeforeRetry((sender, args) => { });

            // Assert
            Same(rule, result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedBeforeExecutionOfAttemptedMethodForNonReturnableMethod(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input).OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedBeforeExecutionOfAttemptedMethodForReturnableMethod(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input).OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            var result = rule.Attempt(() => "Return value", 1, TimeSpan.FromSeconds(1));

            // Assert
            True(dispatched, "Event should be dispatched");
            Equal("Return value", result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForNonReturnableMethod(Strategies input)
        {
            // Arrange
            var dispatched = false;
            Rule.SetupRules(input).OnBeforeRetry((sender, args) => dispatched = true);

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
            Rule.SetupRules(input).OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            Rule.SetupRules(input).Attempt(() => "Return value", 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}