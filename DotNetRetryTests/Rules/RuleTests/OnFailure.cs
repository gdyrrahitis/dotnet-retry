namespace DotNetRetry.Unit.Tests.Rules.RuleTests
{
    using System;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using Xunit;
    using static Xunit.Assert;

    public class OnFailure
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsSelf(Strategies input)
        {
            // Arrange 
            var rule = Rule.SetupRules(input).Config(new Options(1, TimeSpan.FromMilliseconds(1)));

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
            var rule = Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() => { throw new Exception("Custom Exception"); }));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeDispatchedWhenFailureOccursForFunction(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() =>
            {
                throw new Exception("Custom Exception");
            }));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForAction(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { });

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForFunction(Strategies input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => "Function Invocation");

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForAction(Strategies input)
        {
            // Arrange
            var dispatched = false;
            Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .Attempt(() => { });

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForFunction(Strategies input)
        {
            // Arrange
            var dispatched = false;
            Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            Rule.SetupRules(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .Attempt(() => "Function Invocation");

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}