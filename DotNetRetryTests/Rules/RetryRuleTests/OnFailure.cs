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
        public void ReturnsSelf(Strategies input)
        {
            // Arrange 
            var rule = Rule.SetupRules(input);

            // Act
            var result = rule.OnFailure((sender, args) => { });

            // Assert
            Same(rule, result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeDispatchedWhenFailureOccursForAction(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() => { throw new Exception("Custom Exception"); }, 1,
                TimeSpan.FromSeconds(1)));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeDispatchedWhenFailureOccursForFunction(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

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
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForAction(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForFunction(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => "Function Invocation", 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForAction(Strategies input)
        {
            // Arrange
            var dispatched = false;
            Rule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

            // Act
            Rule.SetupRules(input).Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForFunction(Strategies input)
        {
            // Arrange
            var dispatched = false;
            Rule.SetupRules(input).OnFailure((sender, args) => dispatched = true);

            // Act
            Rule.SetupRules(input).Attempt(() => "Function Invocation", 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}