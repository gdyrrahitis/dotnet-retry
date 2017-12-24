namespace DotNetRetry.Unit.Tests.Core.Exceptions.RuleNotFoundExceptionTests
{
    using System;
    using DotNetRetry.Core.Exceptions;
    using Xunit;

    public class Constructor
    {
        [Fact]
        public void CreatesInstanceWithParameterlessConstructor()
        {
            // Arrange | Act
            var result = new RuleNotFoundException();

            // Assert
            Assert.IsAssignableFrom<Exception>(result);
        }

        [Fact]
        public void CreatesInstanceWithMessage()
        {
            // Arrange 
            const string message = "Custom Message";

            // Act
            var result = new RuleNotFoundException(message);

            // Assert
            Assert.Equal(message, result.Message);
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
            Assert.Equal(message, result.Message);
            Assert.NotNull(result.InnerException);
            Assert.Equal(innerMessage, result.InnerException.Message);
        }
    }
}