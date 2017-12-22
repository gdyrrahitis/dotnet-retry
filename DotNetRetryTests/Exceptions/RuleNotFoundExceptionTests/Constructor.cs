namespace DotNetRetry.Tests.Exceptions.RuleNotFoundExceptionTests
{
    using System;
    using Core.Exceptions;
    using Xunit;
    using static Xunit.Assert;

    public class Constructor
    {
        [Fact]
        public void CreatesInstanceWithParameterlessConstructor()
        {
            // Arrange | Act
            var result = new RuleNotFoundException();

            // Assert
            IsAssignableFrom<Exception>(result);
        }

        [Fact]
        public void CreatesInstanceWithMessage()
        {
            // Arrange 
            const string message = "Custom Message";

            // Act
            var result = new RuleNotFoundException(message);

            // Assert
            Equal(message, result.Message);
        }

        [Fact]
        public void CreatesInstanceWithMessageAndInnerException()
        {
            // Arrange 
            const string message = "Custom Message";
            const string innerMessage = "Inner Exception";
            var innerException = new Exception(innerMessage);

            // Act
            var result = new RuleNotFoundException(message, innerException);

            // Assert
            Equal(message, result.Message);
            NotNull(result.InnerException);
            Equal(innerMessage, result.InnerException.Message);
        }
    }
}