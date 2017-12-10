namespace DotNetRetry.Tests.Rules.RetryRuleTests
{
    using System;
    using DotNetRetry.Rules;
    using NUnit.Framework;
    using static NUnit.Framework.Assert;

    [TestFixture]
    public class OnFailure
    {
        [Test]
        public void ReturnsSelf()
        {
            // Arrange 
            var rule = RetryRule.SetupRules();

            // Act
            var result = rule.OnFailure((sender, args) => { });

            // Assert
            AreSame(rule, result);
        }

        [Test]
        public void EventShouldBeDispatchedWhenFailureOccursForAction()
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules().OnFailure((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() => { throw new Exception("Custom Exception"); }, 1,
                TimeSpan.FromSeconds(1)));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Test]
        public void EventShouldBeDispatchedWhenFailureOccursForFunction()
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules().OnFailure((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() =>
            {
                throw new Exception("Custom Exception");
                return "Function Invocation";
            }, 1,
                TimeSpan.FromSeconds(1)));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Test]
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForAction()
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules().OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Test]
        public void EventShouldNotBeDispatchedWhenThereIsNoFailureForFunction()
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules().OnFailure((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => "Function Invocation", 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Test]
        public void EventShouldNotBeDispatchedByAnotherRuleForAction()
        {
            // Arrange
            var dispatched = false;
            RetryRule.SetupRules().OnFailure((sender, args) => dispatched = true);

            // Act
            RetryRule.SetupRules().Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Test]
        public void EventShouldNotBeDispatchedByAnotherRuleForFunction()
        {
            // Arrange
            var dispatched = false;
            RetryRule.SetupRules().OnFailure((sender, args) => dispatched = true);

            // Act
            RetryRule.SetupRules().Attempt(() => "Function Invocation", 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}