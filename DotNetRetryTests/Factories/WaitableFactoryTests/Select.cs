namespace DotNetRetry.Unit.Tests.Factories.WaitableFactoryTests
{
    using System;
    using DotNetRetry.Factories;
    using DotNetRetry.Rules.Waitables;
    using Xunit;
    using static Xunit.Assert;

    public class Select
    {
        [Theory]
        [InlineData(1, typeof (Pauser))]
        [InlineData(0, typeof (Stopper))]
        [InlineData(-1, typeof (Stopper))]
        public void AppropriateAwaitable(int attempt, Type type)
        {
            // Arrange
            var factory = new WaitableFactory();

            // Act
            var result = factory.Select(attempt);

            // Assert
            IsType(type, result);
        }
    }
}