namespace DotNetRetry.Unit.Tests.Rules.Templates.Sequential.FunctionBodyTests
{
    using System;
    using DotNetRetry.Core.Abstractions;
    using DotNetRetry.Factories;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Rules.Templates.Sequential;
    using Moq;
    using Xunit;
    using static Xunit.Assert;

    public class WaitTime
    {
        [Theory]
        [InlineData(1, 1000)]
        [InlineData(2, 1000)]
        [InlineData(4, 1000)]
        [InlineData(8, 1000)]
        [InlineData(12, 1000)]
        public void ShouldEqualToWhatIsSetForRetriableOptions(int attempts, int seconds)
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
            var functionBody = new FunctionBody(retriableMock.Object, waitableFactoryMock.Object);

            // Act
            var result = functionBody.WaitTime();

            // Assert
            Equal(time, result);
        }
    }
}