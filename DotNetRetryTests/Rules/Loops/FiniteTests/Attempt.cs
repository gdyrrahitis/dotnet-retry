namespace DotNetRetry.Unit.Tests.Rules.Loops.FiniteTests
{
    using System;
    using System.Collections.Generic;
    using DotNetRetry.Core.Abstractions;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Rules.Loops;
    using DotNetRetry.Rules.Templates;
    using Moq;
    using Xunit;

    public class Attempt
    {
        private readonly Mock<ActionBodyTemplate> _actionBodyMock;
        private readonly Mock<Retriable> _retriableMock;
        private readonly Finite _finite;

        public Attempt()
        {
            _retriableMock = new Mock<Retriable>();
            _actionBodyMock = new Mock<ActionBodyTemplate>(_retriableMock.Object);
            _finite = new Finite(_actionBodyMock.Object, _retriableMock.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(12)]
        public void ShouldDoFiniteAmountOfAttemptsAndEquallyInvokeBeforeAndAfterEvents(int attempts)
        {
            // Arrange
            Action action = () => { };
            var options = new Options(attempts);
            _retriableMock.Object.Options = new RuleOptions(_retriableMock.Object);
            _retriableMock.Object.Options.Config(options);

            // Act
            _finite.Attempt(action);

            // Assert
            _actionBodyMock.Verify(m => m.Do(action, It.IsAny<List<Exception>>(), It.IsAny<TimeSpan>(), It.IsAny<int>()),
                Times.Exactly(attempts));
        }
    }
}