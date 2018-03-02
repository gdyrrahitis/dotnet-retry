namespace DotNetRetry.Unit.Tests.Rules.Cancellation.ExceptionRuleTests
{
    using System;
    using System.Linq;
    using DotNetRetry.Core.Exceptions;
    using DotNetRetry.Rules.Cancellation;
    using Xunit;
    using static Xunit.Assert;

    public class Or
    {
        [Fact]
        public void ForGenericMethodAddsExceptionIntoListAndReturnsAnInstanceOfExceptionRule()
        {
            // Arrange
            var cancellationRule = new CancellationRule();
            var exceptionRule = new ExceptionRule(cancellationRule);

            // Act
            var result = exceptionRule.Or<Exception>();

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
            var exceptionRule = new ExceptionRule(cancellationRule);

            // Act
            var result = exceptionRule.Or(type);

            // Assert
            IsType<ExceptionRule>(result);
            Single(cancellationRule.StoredExceptions);
            IsAssignableFrom<Type>(cancellationRule.StoredExceptions.First());
            Equal(type, cancellationRule.StoredExceptions.First());
        }
    }
}