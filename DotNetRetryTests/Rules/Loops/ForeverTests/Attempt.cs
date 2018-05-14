namespace DotNetRetry.Unit.Tests.Rules.Loops.ForeverTests
{
    using System;
    using System.Collections.Generic;
    using DotNetRetry.Core.Abstractions;
    using DotNetRetry.Core.Time;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Rules.Loops;
    using DotNetRetry.Rules.Templates;
    using Moq;
    using Xunit;
    using static Xunit.Assert;

    public class Attempt
    {
        private readonly Mock<ActionBodyTemplate> _actionBodyMock;
        private readonly Mock<FunctionBodyTemplate> _functionBodyMock;
        private readonly Mock<Retriable> _retriableMock;
        private readonly Forever _forever;

        public Attempt()
        {
            _retriableMock = new Mock<Retriable>();
            _actionBodyMock = new Mock<ActionBodyTemplate>(_retriableMock.Object);
            _functionBodyMock = new Mock<FunctionBodyTemplate>(_retriableMock.Object);
            _forever = new Forever(_actionBodyMock.Object, _functionBodyMock.Object, _retriableMock.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(12)]
        public void ShouldDoInfiniteAmountOfAttemptsAndEquallyInvokeBeforeAndAfterEventsUntilOperationSucceedsForAction(int attempts)
        {
            // Arrange
            var count = 0;
            Action action = () => { };
            _retriableMock.Object.Options = new RuleOptions(_retriableMock.Object);
            _retriableMock.Object.Options.Config(options => options.Attempts = attempts);
            _actionBodyMock.Setup(m => m.Do(action, It.IsAny<List<Exception>>(), It.IsAny<TimerService>(), It.IsAny<int>()))
                .Returns(() => count++ >= attempts);

            // Act
            _forever.Attempt(action);

            // Assert
            _actionBodyMock.Verify(m => m.Do(action, It.IsAny<List<Exception>>(), It.IsAny<TimerService>(), It.IsAny<int>()),
                Times.Exactly(attempts + 1));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(12)]
        public void ShouldDoInfiniteAmountOfAttemptsAndEquallyInvokeBeforeAndAfterEventsUntilOperationSucceedsForFunction(int attempts)
        {
            // Arrange
            var expected = "Hello World!";
            var count = 0;
            Func<string> function = () => "Hello World!";
            _retriableMock.Object.Options = new RuleOptions(_retriableMock.Object);
            _retriableMock.Object.Options.Config(options => options.Attempts = attempts);
            _functionBodyMock.Setup(m => m.Do(function, It.IsAny<List<Exception>>(), It.IsAny<TimerService>(), It.IsAny<int>(), out expected))
                .Returns(() => count++ >= attempts);

            // Act
            var result = _forever.Attempt(function);

            // Assert
            Equal(expected, result);
            _functionBodyMock.Verify(m => m.Do(function, It.IsAny<List<Exception>>(), It.IsAny<TimerService>(), It.IsAny<int>(), out expected),
                Times.Exactly(attempts + 1));
        }
    }
}