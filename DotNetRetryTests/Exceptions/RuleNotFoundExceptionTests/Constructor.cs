namespace DotNetRetry.Tests.Exceptions.RuleNotFoundExceptionTests
{
    using System;
    using DotNetRetry.Exceptions;
    using NUnit.Framework;
    using static NUnit.Framework.Assert;

    [TestFixture]
    public class Constructor
    {
        [Test]
        public void CreatesInstanceWithParameterlessConstructor()
        {
            // Arrange | Act
            var result = new RuleNotFoundException();

            // Assert
            IsInstanceOf<Exception>(result);
        }

        [Test]
        public void CreatesInstanceWithMessage()
        {
            // Arrange 
            const string message = "Custom Message";

            // Act
            var result = new RuleNotFoundException(message);

            // Assert
            AreEqual(message, result.Message);
        }

        [Test]
        public void CreatesInstanceWithMessageAndInnerException()
        {
            // Arrange 
            const string message = "Custom Message";
            const string innerMessage = "Inner Exception";
            var innerException = new Exception(innerMessage);

            // Act
            var result = new RuleNotFoundException(message, innerException);

            // Assert
            AreEqual(message, result.Message);
            NotNull(result.InnerException);
            AreEqual(innerMessage, result.InnerException.Message);
        }
    }
}