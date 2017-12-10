namespace DotNetRetry.Tests.Rules.RetryRuleTests
{
    using System;
    using DotNetRetry.Rules;
    using NUnit.Framework;
    using static NUnit.Framework.Assert;

    [TestFixture]
    public class OnAfterRetry
    {
        [Test]
        public void ReturnsSelf()
        {
            // Arrange 
            var rule = RetryRule.SetupRules();

            // Act
            var result = rule.OnAfterRetry((sender, args) => { });

            // Assert
            AreSame(rule, result);
        }

        [Test]
        public void EventShouldBeRaisedAfterTheRetry()
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules().OnAfterRetry((sender, args) => dispatched = true);

            // Act
            rule.Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            True(dispatched, "Event should be dispatched");
        }

        [Test]
        public void EventShouldNotBeRaisedAsNoRetriesWherePerformed()
        {
            // Arrange
            var dispatched = false;
            var rule = RetryRule.SetupRules().OnAfterRetry((sender, args) => dispatched = true);

            // Act
            Throws<AggregateException>(() => rule.Attempt(() => { throw new Exception("Custom exception"); }, 1, 
                TimeSpan.FromSeconds(1)));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }

        [Test]
        public void EventShouldNotBeDispatchedByAnotherRule()
        {
            // Arrange
            var dispatched = false;
            RetryRule.SetupRules().OnAfterRetry((sender, args) => dispatched = true);

            // Act
            RetryRule.SetupRules().Attempt(() => { }, 1, TimeSpan.FromSeconds(1));

            // Assert
            False(dispatched, "Event should not be dispatched");
        }
    }
}