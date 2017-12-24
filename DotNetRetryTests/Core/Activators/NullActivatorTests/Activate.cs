namespace DotNetRetry.Unit.Tests.Core.Activators.NullActivatorTests
{
    using System;
    using DotNetRetry.Core;
    using DotNetRetry.Core.Activators;
    using DotNetRetry.Core.Exceptions;
    using DotNetRetry.Rules;
    using Xunit;

    public class Activate
    {
        [Fact]
        public void ThrowsArgumentExceptionWhenTypeIsNotNull()
        {
            // Arrange
            var type = typeof(Sequential);
            var activator = new NullActivator();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => activator.Activate<IRetry>(type));

            // Assert
            Assert.Equal($"Type provided is not null, invalid for {nameof(NullActivator)} instance.\r\nParameter name: type", exception.Message);
        }

        [Fact]
        public void ThrowsRuleNotFoundExceptionForNullType()
        {
            // Arrange
            var activator = new NullActivator();

            // Act
            var exception = Assert.Throws<RuleNotFoundException>(() => activator.Activate<IRetry>(null));

            // Assert
            Assert.Equal("Could not find rule.", exception.Message);
        }
    }
}