namespace DotNetRetry.Integration.Tests.Rules.RuleTests
{
    using System;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Tests.Common;
    using Xunit;
    using static Xunit.Assert;

    public class OnFailure
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ReturnsSelf(Strategy input)
        {
            // Arrange 
            var rule = Rule.Setup(input).Config(options =>
            {
                options.Attempts = 1;
                options.Time = TimeSpan.FromMilliseconds(1);
            });

            // Act
            var result = rule.OnFailure((sender, args) => { });

            // Assert
            Same(rule, result);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeDispatchedWhenFailureOccursForAction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 2;
                    options.Time = TimeSpan.FromMilliseconds(1000);
                })
                .OnFailure((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() => { throw new Exception("Custom Exception"); }));

            // Assert
            Console.WriteLine($"This is the failed one ({input}) - OnFailure.EventShouldBeDispatchedWhenFailureOccursForAction");
            True(dispatched, "Event should be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldBeDispatchedWhenFailureOccursForFunction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
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
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForAction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { });

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForFunction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            var rule = Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => "Function Invocation");

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForAction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnFailure((sender, args) => dispatched = true);

            // Act
            Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .Attempt(() => { });

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void EventShouldNotBeDispatchedByAnotherRuleForFunction(Strategy input)
        {
            // Arrange
            var dispatched = false;
            Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .OnFailure((sender, args) => dispatched = true);

            // Act
            Rule.Setup(input)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                })
                .Attempt(() => "Function Invocation");

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}