namespace DotNetRetry.Unit.Tests.Rules.Loops.FiniteTests
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
        private readonly Finite _finite;

        public Attempt()
        {
            _retriableMock = new Mock<Retriable>();
            _actionBodyMock = new Mock<ActionBodyTemplate>(_retriableMock.Object);
            _functionBodyMock = new Mock<FunctionBodyTemplate>(_retriableMock.Object);
            _finite = new Finite(_actionBodyMock.Object, _functionBodyMock.Object, _retriableMock.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(12)]
        public void ShouldDoFiniteAmountOfAttemptsAndEquallyInvokeBeforeAndAfterEventsForAction(int attempts)
        {
            // Arrange
            Action action = () => { };
            _retriableMock.Object.Options = new RuleOptions(_retriableMock.Object);
            _retriableMock.Object.Options.Config(options => options.Attempts = attempts);

            // Act
            _finite.Attempt(action);

            // Assert
            _actionBodyMock.Verify(m => m.Do(action, It.IsAny<List<Exception>>(), It.IsAny<TimerService>(), It.IsAny<int>()),
                Times.Exactly(attempts));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(12)]
        public void ShouldDoFiniteAmountOfAttemptsAndEquallyInvokeBeforeAndAfterEventsForFunction(int attempts)
        {
            // Arrange
            var count = 0;
            var expected = "Hello World!";
            Func<string> function = () => "Hello World!";
            _retriableMock.Object.Options = new RuleOptions(_retriableMock.Object);
            _retriableMock.Object.Options.Config(options => options.Attempts = attempts);
            _functionBodyMock.Setup(
                m => m.Do(function, It.IsAny<List<Exception>>(), It.IsAny<TimerService>(), It.IsAny<int>(), out expected))
                .Returns(() => count++ >= attempts - 1);

            // Act
            var result = _finite.Attempt(function);

            // Assert
            Equal(expected, result);
            _functionBodyMock.Verify(m => m.Do(function, It.IsAny<List<Exception>>(), It.IsAny<TimerService>(), It.IsAny<int>(), out expected),
                Times.Exactly(attempts));
        }
    }
}