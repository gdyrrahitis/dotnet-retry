namespace DotNetRetry.Unit.Tests.Rules.Waitables.PauserTests
{
    using System;
    using DotNetRetry.Core.Auxiliery;
    using DotNetRetry.Rules.Waitables;
    using Moq;
    using Xunit;

    public class Wait
    {
        [Fact]
        public void CallsDelayWithTimeSpanValue()
        {
            // Arrange
            var delayerMock = new Mock<IDelayer>();
            var pauser = new Pauser(delayerMock.Object);
            var waitTime = TimeSpan.FromSeconds(1);

            // Act
            pauser.Wait(waitTime);

            // Assert
            delayerMock.Verify(m => m.Delay(waitTime), Times.Once);
        }
    }
}