namespace DotNetRetry.Unit.Tests.Factories.WaitableFactoryTests
{
    using System;
    using DotNetRetry.Core.Abstractions;
    using DotNetRetry.Factories;
    using DotNetRetry.Rules.Waitables;
    using Moq;
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
            var retriableMock = new Mock<Retriable>();
            var factory = new WaitableFactory(retriableMock.Object);

            // Act
            var result = factory.Select(attempt);

            // Assert
            IsType(type, result);
        }
    }
}