namespace DotNetRetry.Unit.Tests.Core.Activators.TypeActivatorTests
{
    using System;
    using DotNetRetry.Core;
    using DotNetRetry.Core.Activators;
    using DotNetRetry.Rules;
    using Xunit;

    public class Activate
    {
        [Fact]
        public void CreatesInstanceOfType()
        {
            // Arrange
            var type = typeof(Sequential);
            var rules = Rule.SetupRules(Strategies.Sequential);
            var activator = new TypeActivator();

            // Act
            var result = activator.Activate<IRetry>(type, rules);

            // Assert
            Assert.IsType<Sequential>(result);
            Assert.IsAssignableFrom<IRetry>(result);
        }

        [Fact]
        public void ThrowsMissingMethodExceptionWhenRequiredParametersAreNotPassed()
        {
            // Arrange
            var type = typeof(Sequential);
            var activator = new TypeActivator();

            // Act
            var exception = Assert.Throws<MissingMethodException>(() => activator.Activate<IRetry>(type));

            // Assert
            Assert.Equal("Constructor on type 'DotNetRetry.Rules.Sequential' not found.", exception.Message);
        }
    }
}