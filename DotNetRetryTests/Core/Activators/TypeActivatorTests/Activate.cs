namespace DotNetRetry.Unit.Tests.Core.Activators.TypeActivatorTests
{
    using System;
    using DotNetRetry.Core;
    using DotNetRetry.Core.Activators;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using Xunit;
    using static Xunit.Assert;

    public class Activate
    {
        [Theory]
        [InlineData(typeof(Sequential), Strategy.Sequential)]
        [InlineData(typeof(Exponential), Strategy.Exponential)]
        public void CreatesInstanceOfType(Type type, Strategy strategy)
        {
            // Arrange
            var rules = Rule.Setup(strategy)
                .Config(options =>
                {
                    options.Attempts = 1;
                    options.Time = TimeSpan.FromMilliseconds(1);
                });
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