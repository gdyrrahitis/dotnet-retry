namespace DotNetRetry.Unit.Tests.Core.Activators.TypeActivatorTests
{
    using System;
    using DotNetRetry.Core;
    using DotNetRetry.Core.Activators;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class Activate
    {
        [Theory]
        [InlineData(typeof(Sequential), Strategies.Sequential)]
        [InlineData(typeof(Exponential), Strategies.Exponential)]
        public void CreatesInstanceOfType(Type type, Strategies strategy)
        {
            // Arrange
            var rules = Rule.SetupRules(strategy);
            var activator = new TypeActivator();

            // Act
            var result = activator.Activate<IRetry>(type, rules);

            // Assert
            IsType(type, result);
            IsAssignableFrom<IRetry>(result);
        }

        [Theory]
        [InlineData(typeof(Sequential), "Sequential")]
        public void ThrowsMissingMethodExceptionWhenRequiredParametersAreNotPassed(Type type, string rule)
        {
            // Arrange
            var activator = new TypeActivator();

            // Act
            var exception = Throws<MissingMethodException>(() => activator.Activate<IRetry>(type));

            // Assert
            Equal($"Constructor on type 'DotNetRetry.Rules.{rule}' not found.", exception.Message);
        }
    }
}