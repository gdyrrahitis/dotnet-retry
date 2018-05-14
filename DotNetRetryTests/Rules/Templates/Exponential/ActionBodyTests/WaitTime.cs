namespace DotNetRetry.Unit.Tests.Rules.Templates.Exponential.ActionBodyTests
{
    using System;
    using DotNetRetry.Core.Abstractions;
    using DotNetRetry.Factories;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Rules.Templates.Exponential;
    using Moq;
    using Xunit;
    using static Xunit.Assert;

    public class WaitTime
    {
        /// <remarks>
        /// Algorithm:
        /// min((2 ^ n) + random(0, 1000), backoff)
        /// </remarks>
        [Theory]
        [InlineData(1, 500, 12)]
        [InlineData(2, 500, 14)]
        [InlineData(3, 500, 18)]
        [InlineData(4, 500, 26)]
        [InlineData(5, 500, 42)]
        [InlineData(6, 1000, 74)]
        [InlineData(7, 1000, 138)]
        [InlineData(8, 1000, 266)]
        [InlineData(9, 1000, 522)]
        [InlineData(10, 1000, 1000)]
        [InlineData(11, 2000, 2000)]
        [InlineData(12, 5000, 4106)]
        [InlineData(13, 5000, 5000)]
        [InlineData(14, 20000, 16394)]
        [InlineData(15, 100000, 32778)]
        [InlineData(16, 200000, 65546)]
        [InlineData(17, 500000, 131082)]
        [InlineData(18, 1000000, 262154)]
        [InlineData(19, 1000000, 524298)]
        [InlineData(20, 1000000, 1000000)]
        public void ShouldEqualToExpectedValueForFormula(int retries, int seconds, int expected)
        {
            // Arrange
            const int randomValue = 10;
            var time = TimeSpan.FromMilliseconds(seconds);
            var retriableMock = new Mock<Retriable>();
            retriableMock.Object.Options = new RuleOptions(retriableMock.Object);
            retriableMock.Object.Options.Config(options =>
            {
                options.Attempts = randomValue;
                options.Time = time;
            });
            var waitableFactoryMock = new Mock<IWaitableFactory>();
            var randomMock = new Mock<Random>();
            randomMock.Setup(m => m.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(randomValue);
            var actionBody = new ActionBody(retriableMock.Object, randomMock.Object, waitableFactoryMock.Object);
            actionBody.GetType().GetField("_count", System.Reflection.BindingFlags.NonPublic | 
                System.Reflection.BindingFlags.Instance)?
                .SetValue(actionBody, retries);

            // Act
            var result = actionBody.WaitTime();

            // Assert
            Equal(TimeSpan.FromMilliseconds(expected).TotalMilliseconds, result.TotalMilliseconds);
        }
    }
}