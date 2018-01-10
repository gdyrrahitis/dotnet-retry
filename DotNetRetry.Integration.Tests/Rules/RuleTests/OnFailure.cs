namespace DotNetRetry.Integration.Tests.Rules.RuleTests
{
    using System;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Tests.Common;
    using Xunit;

    public class OnFailure
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsSelf(Strategy input)
        {
            // Arrange 
            var rule = Rule.Setup(input).Config(new Options(1, TimeSpan.FromMilliseconds(1)));

            // Act
            var result = rule.OnFailure((sender, args) => { });

            // Assert
            Assert.Same(rule, result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeDispatchedWhenFailureOccursForAction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            Assert.Throws<AggregateException>(() => rule.Attempt(() => { throw new Exception("Custom Exception"); }));

            // Assert
            Assert.True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeDispatchedWhenFailureOccursForFunction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            Assert.Throws<AggregateException>(() => rule.Attempt(() =>
            {
                throw new Exception("Custom Exception");
            }));

            // Assert
            Assert.True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForAction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { });

            // Assert
            Assert.False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForFunction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => "Function Invocation");

            // Assert
            Assert.False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForAction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .Attempt(() => { });

            // Assert
            Assert.False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForFunction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnFailure((sender, args) => dispatched = true);

            // Act
            Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .Attempt(() => "Function Invocation");

            // Assert
            Assert.False(dispatched, "Event should not be dispatched");
        }
    }
}