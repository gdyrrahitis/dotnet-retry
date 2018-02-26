namespace DotNetRetry.Unit.Tests.Core.Activators.NullActivatorTests
{
    using System;
    using DotNetRetry.Core;
    using DotNetRetry.Core.Activators;
    using DotNetRetry.Core.Exceptions;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class Activate
    {
        [Fact]
        public void ThrowsArgumentExceptionWhenTypeIsNotNull()
        {
            // Arrange
            var type = typeof(Sequential);
            var activator = new NullActivator();

            // Act
            var exception = Throws<ArgumentException>(() => activator.Activate<IRetry>(type));

            // Assert
            Equal($"Type provided is not null, invalid for {nameof(NullActivator)} instance.{Environment.NewLine}Parameter name: type", 
                exception.Message);
        }

        [Fact]
        public void ThrowsRuleNotFoundExceptionForNullType()
        {
            // Arrange
            var activator = new NullActivator();

            // Act
            var exception = Throws<RuleNotFoundException>(() => activator.Activate<IRetry>(null));

            // Assert
            Equal("Could not find rule.", exception.Message);
        }
    }
}