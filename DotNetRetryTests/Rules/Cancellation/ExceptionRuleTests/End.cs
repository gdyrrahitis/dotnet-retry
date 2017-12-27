namespace DotNetRetry.Unit.Tests.Rules.Cancellation.ExceptionRuleTests
{
    using DotNetRetry.Rules.Cancellation;
    using Xunit;
    using static Xunit.Assert;

    public class End
    {
        [Fact]
        public void ReturnsTheParentCancellationRuleObject()
        {
            // Arrange
            var cancellationRule = new CancellationRule();
            var exceptionRule = new ExceptionRule(cancellationRule);

            // Act
            var result = exceptionRule.End();

            // Assert
            Same(cancellationRule, result);
        }
    }
}