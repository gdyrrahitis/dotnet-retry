namespace DotNetRetry.Unit.Tests.Rules.Cancellation.CancellationRuleTests
{
    using System;
    using DotNetRetry.Rules.Cancellation;
    using Xunit;
    using static Xunit.Assert;

    public class After
    {
        [Fact]
        public void SetsStoredTimeAndReturnsCurrentInstance()
        {
            // Arrange
            var time = TimeSpan.FromSeconds(2);
            var cancellationRule = new CancellationRule();

            // Act
            var result = cancellationRule.After(time);

            // Assert
            NotNull(cancellationRule.StoredTime);
            Equal(time, cancellationRule.StoredTime.Value);
            Same(cancellationRule, result);
        }
    }
}