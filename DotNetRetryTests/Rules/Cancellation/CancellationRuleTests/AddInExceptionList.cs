namespace DotNetRetry.Unit.Tests.Rules.Cancellation.CancellationRuleTests
{
    using System;
    using System.Linq;
    using DotNetRetry.Core.Exceptions;
    using DotNetRetry.Rules.Cancellation;
    using Xunit;
    using static Xunit.Assert;

    public class AddInExceptionList
    {
        [Theory]
        [InlineData(typeof(Exception))]
        [InlineData(typeof(ArgumentOutOfRangeException))]
        [InlineData(typeof(ArgumentException))]
        [InlineData(typeof(MissingMethodException))]
        [InlineData(typeof(RuleNotFoundException))]
        public void ShouldCheckIfExceptionTypeIsContainedInCollection(Type type)
        {
            // Arrange
            var cancellationRule = new CancellationRule();

            // Act
            cancellationRule.AddExceptionType(type);

            // Assert
            Single(cancellationRule.StoredExceptions);
            Equal(type, cancellationRule.StoredExceptions.First());
        }
    }
}