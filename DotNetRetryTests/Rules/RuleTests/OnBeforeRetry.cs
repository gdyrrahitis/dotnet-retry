namespace DotNetRetry.Unit.Tests.Rules.RuleTests
{
    using System;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using Xunit;
    using static Xunit.Assert;

    public class OnBeforeRetry
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsSelf(Strategies input)
        {
            // Arrange 
            var rule = Rule.SetupRules(input).Config(new Options(1, TimeSpan.FromMilliseconds(1)));

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
            var rule = Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { });

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedBeforeExecutionOfAttemptedMethodForReturnableMethod(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            var result = rule.Attempt(() => "Return value");

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
            Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .Attempt(() => { });

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForReturnableMethod(Strategies input)
        {
            // Arrange
            var dispatched = false;
            Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .Attempt(() => "Return value");

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}