namespace DotNetRetry.Unit.Tests.Rules.Templates.Sequential.ActionBodyTests
{
    using System;
    using System.Linq;
    using DotNetRetry.Core.Abstractions;
    using DotNetRetry.Factories;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Rules.Templates.Sequential;
    using DotNetRetry.Rules.Waitables;
    using Moq;
    using Xunit;

    public class Delay
    {
        [Theory]
        [InlineData(1, 1000)]
        [InlineData(2, 500)]
        [InlineData(4, 800)]
        [InlineData(8, 1500)]
        [InlineData(12, 2000)]
        public void ShouldCallWaitWithSpecifiedTime(int attempts, int seconds)
        {
            // Arrange
            var time = TimeSpan.FromMilliseconds(seconds);
            var retriableMock = new Mock<Retriable>();
            retriableMock.Object.Options = new RuleOptions(retriableMock.Object);
            retriableMock.Object.Options.Config(options =>
            {
                options.Attempts = attempts;
                options.Time = time;
            });
            var waitableFactoryMock = new Mock<IWaitableFactory>();
            var actionBody = new ActionBody(retriableMock.Object, waitableFactoryMock.Object);
            var exceptions = Enumerable.Repeat(new Exception(), 2).ToList();
            var waitableMock = new Mock<IWaitable>();
            waitableFactoryMock.Setup(m => m.Select(It.IsAny<int>())).Returns(waitableMock.Object);

            // Act
            actionBody.Delay(attempts, time, exceptions);

            // Assert
            waitableMock.VerifySet(m => m.Exceptions = exceptions, Times.Once());
            waitableMock.Verify(m => m.Wait(time), Times.Once);
        }
    }
}