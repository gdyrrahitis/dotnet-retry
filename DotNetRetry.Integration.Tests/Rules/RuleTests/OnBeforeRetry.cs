namespace DotNetRetry.Integration.Tests.Rules.RuleTests
{
    using System;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Tests.Common;
    using Xunit;

    public class OnBeforeRetry
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsSelf(Strategy input)
        {
            // Arrange 
            var rule = Rule.Setup(input).Config(new Options(1, TimeSpan.FromMilliseconds(1)));

            // Act
            var result = rule.OnBeforeRetry((sender, args) => { });

            // Assert
            Assert.Same(rule, result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedBeforeExecutionOfAttemptedMethodForNonReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { });

            // Assert
            Assert.True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeRaisedBeforeExecutionOfAttemptedMethodForReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            var result = rule.Attempt(() => "Return value");

            // Assert
            Assert.True(dispatched, "Event should be dispatched");
            Assert.Equal("Return value", result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForNonReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .Attempt(() => { });

            // Assert
            Assert.False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForReturnableMethod(Strategy input)
        {
            // Arrange
            var dispatched = false;
            Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .OnBeforeRetry((sender, args) => dispatched = true);

            // Act
            Rule.Setup(input)
                .Config(new Options(1, TimeSpan.FromMilliseconds(1)))
                .Attempt(() => "Return value");

            // Assert
            Assert.False(dispatched, "Event should not be dispatched");
        }
    }
}