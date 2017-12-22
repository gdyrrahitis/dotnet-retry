namespace DotNetRetry.Tests.Strategy.Activators.TypeActivatorTests
{
    using System;
    using Core;
    using Core.Activators;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

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
            IsType<Sequential>(result);
            IsAssignableFrom<IRetry>(result);
        }

        [Fact]
        public void ThrowsMissingMethodExceptionWhenRequiredParametersAreNotPassed()
        {
            // Arrange
            var type = typeof(Sequential);
            var activator = new TypeActivator();

            // Act
            var exception = Throws<MissingMethodException>(() => activator.Activate<IRetry>(type));

            // Assert
            Equal("Constructor on type 'DotNetRetry.Rules.Sequential' not found.", exception.Message);
        }
    }
}