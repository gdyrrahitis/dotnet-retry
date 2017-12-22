namespace DotNetRetry.Tests.Rules.RetryRuleTests
{
    using System;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class Cancel
    {
        // Cancel for specified exceptions (data driven)
        [Fact]
        public void PolicyForSpecificExceptions()
        {
            // Arrange
            var attempts = 0;
            var rule = RetryRule.SetupRules(Rule.Sequential);
            rule.Cancel(c => c.OnFailure<Exception>());

            // Act
            var exception = Throws<AggregateException>(() => rule.Attempt(() =>
            {
                attempts++;
               throw new Exception("Custom Exception");
            }, 5, TimeSpan.FromMilliseconds(100)));

            // Assert
            Equal(1, attempts);
            Single(exception.InnerExceptions);
        }

        // Cancel for specified amount of time (data driven)
        [Fact]
        public void PolicyForExceedingAmountOfTime()
        {
            // Arrange
            var attempts = 0;
            var rule = RetryRule.SetupRules(Rule.Sequential);
            rule.Cancel(c => c.After(TimeSpan.FromMilliseconds(300)));

            // Act
            var exception = Throws<AggregateException>(() => rule.Attempt(() =>
            {
                attempts++;
                throw new Exception("Custom Exception");
            }, 5, TimeSpan.FromMilliseconds(100)));

            // Assert
            Equal(3, attempts);
            Equal(3, exception.InnerExceptions.Count);
        }

        // Mix and match the above (data driven)
        [Theory]
        [InlineData(300, 5, 100, 3, typeof(ArgumentException))]
        [InlineData(300, 5, 100, 1, typeof(Exception))]
        public void ApplyingBothPolicies(int cancelAfter, int totalAttempts, int timeToWait, 
            int expected, Type type)
        {
            // Arrange
            var attempts = 0;
            var rule = RetryRule.SetupRules(Rule.Sequential);
            rule.Cancel(c => c.After(TimeSpan.FromMilliseconds(cancelAfter)).OnFailure(type));

            // Act
            var exception = Throws<AggregateException>(() => rule.Attempt(() =>
            {
                attempts++;
                throw new Exception("Custom Exception");
            }, totalAttempts, TimeSpan.FromMilliseconds(timeToWait)));

            // Assert
            Equal(expected, attempts);
            Equal(expected, exception.InnerExceptions.Count);
        }
    }
}