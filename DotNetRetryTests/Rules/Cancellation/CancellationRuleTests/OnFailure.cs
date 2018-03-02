namespace DotNetRetry.Unit.Tests.Rules.Cancellation.CancellationRuleTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DotNetRetry.Core.Exceptions;
    using DotNetRetry.Rules.Cancellation;
    using Xunit;
    using static Xunit.Assert;

    public class OnFailure
    {
        [Fact]
        public void ForGenericMethodAddsExceptionIntoListAndReturnsAnInstanceOfExceptionRule()
        {
            // Arrange
            var cancellationRule = new CancellationRule();

            // Act
            var result = cancellationRule.OnFailure<Exception>();

            // Assert
            IsType<ExceptionRule>(result);
            Single(cancellationRule.StoredExceptions);
            IsAssignableFrom<Type>(cancellationRule.StoredExceptions.First());
            Equal(typeof(Exception), cancellationRule.StoredExceptions.First());
        }

        [Theory]
        [InlineData(typeof(Exception))]
        [InlineData(typeof(ArgumentOutOfRangeException))]
        [InlineData(typeof(ArgumentException))]
        [InlineData(typeof(MissingMethodException))]
        [InlineData(typeof(RuleNotFoundException))]
        public void MethodAddsExceptionIntoListAndReturnsAnInstanceOfExceptionRule(Type type)
        {
            // Arrange
            var cancellationRule = new CancellationRule();

            // Act
            var result = cancellationRule.OnFailure(type);

            // Assert
            IsType<ExceptionRule>(result);
            Single(cancellationRule.StoredExceptions);
            IsAssignableFrom<Type>(cancellationRule.StoredExceptions.First());
            Equal(type, cancellationRule.StoredExceptions.First());
        }
    }
}