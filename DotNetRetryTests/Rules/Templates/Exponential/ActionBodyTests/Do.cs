namespace DotNetRetry.Unit.Tests.Rules.Templates.Exponential.ActionBodyTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DotNetRetry.Core.Abstractions;
    using DotNetRetry.Core.Time;
    using DotNetRetry.Factories;
    using DotNetRetry.Rules.Cancellation;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Rules.Templates.Exponential;
    using DotNetRetry.Rules.Waitables;
    using Moq;
    using Xunit;
    using static Xunit.Assert;

    public class Do
    {
        private readonly ActionBody _actionBody;
        private readonly Mock<Retriable> _retriableMock;
        private readonly Mock<IWaitable> _waitableMock;
        private readonly Mock<IWaitableFactory> _waitableFactoryMock;

        public Do()
        {
            _retriableMock = new Mock<Retriable>();
            _retriableMock.Object.Options = new RuleOptions(_retriableMock.Object);
            _waitableFactoryMock = new Mock<IWaitableFactory>();
            _waitableMock = new Mock<IWaitable>();
            _waitableFactoryMock.Setup(m => m.Select(It.IsAny<int>())).Returns(_waitableMock.Object);
            var randomMock = new Mock<Random>();
            randomMock.Setup(m => m.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(10);
            _actionBody = new ActionBody(_retriableMock.Object, randomMock.Object, _waitableFactoryMock.Object);
        }

        [Fact]
        public void ReturnsTrueWhenActionExecutesSuccessfully()
        {
            // Arrange
            var exceptions = Enumerable.Repeat(new Exception(), 2).ToList();
            var time = TimeSpan.FromMilliseconds(100);
            var service = new TimerService(time);
            var options = new Options(3, time);
            _retriableMock.Object.Options.Config(options);

            // Act
            var result = _actionBody.Do(() => { }, exceptions, service, options.Attempts);

            // Assert
            True(result);
        }

        [Fact]
        public void FailingActionDispatchesRetryEventsAddsExceptionsToExceptionListAndReturnsFalse()
        {
            // Arrange
            var exceptions = Enumerable.Repeat(new Exception(), 2).ToList();
            var time = TimeSpan.FromMilliseconds(100);
            var service = new TimerService(time);
            var options = new Options(3, time);
            _retriableMock.Object.Options.Config(options);
            var expected = Math.Min(Math.Pow(2, 2) + 10, 100);

            // Act
            var result = _actionBody.Do(() => { throw new Exception("Custom exception"); }, exceptions, service, options.Attempts);

            // Assert
            False(result);
            _retriableMock.Verify(m => m.OnBeforeRetryInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnFailureInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnAfterRetryInvocation(), Times.Once);
            _waitableFactoryMock.Verify(m => m.Select(options.Attempts), Times.Once);
            _waitableMock.Verify(m => m.Wait(TimeSpan.FromMilliseconds(expected)), Times.Once);
            Equal(3, exceptions.Count);
        }

        [Fact]
        public void ThrowsAggregateExceptionWhenExceptionThrownInActionIsInCancellationRuleList()
        {
            // Arrange
            var exceptions = new List<Exception>();
            var time = TimeSpan.FromMilliseconds(100);
            var service = new TimerService(time);
            var options = new Options(3, time);
            _retriableMock.Object.Options.Config(options);
            var cancellationRule = new CancellationRule();
            cancellationRule.AddExceptionType(typeof(ArgumentException));
            _retriableMock.Object.CancellationRule = cancellationRule;

            // Act
            var exception = Throws<AggregateException>(() => _actionBody.Do(() =>
            {
                throw new ArgumentException("Custom exception");
            }, exceptions, service, options.Attempts));

            // Assert
            _retriableMock.Verify(m => m.OnBeforeRetryInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnFailureInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnAfterRetryInvocation(), Times.Once);
            _waitableFactoryMock.Verify(m => m.Select(options.Attempts), Times.Never);
            _waitableMock.Verify(m => m.Wait(options.Time), Times.Never);
            Single(exception.InnerExceptions);
        }

        [Fact]
        public void ThrowsAggregateExceptionWhenExceededMaxTimeSetupInCancellationRule()
        {
            // Arrange
            var exceptions = new List<Exception>();
            var time = TimeSpan.FromMilliseconds(100);
            var service = new TimerService(time);
            var options = new Options(3, time);
            _retriableMock.Object.Options.Config(options);
            var cancellationRule = new CancellationRule();
            cancellationRule.After(TimeSpan.FromMilliseconds(50));
            _retriableMock.Object.CancellationRule = cancellationRule;
            var expected = Math.Min(Math.Pow(2, 0) + 10, 100);

            // Act
            var exception = Throws<AggregateException>(() => _actionBody.Do(() =>
            {
                throw new ArgumentException("Custom exception");
            }, exceptions, service, options.Attempts));

            // Assert
            _retriableMock.Verify(m => m.OnBeforeRetryInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnFailureInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnAfterRetryInvocation(), Times.Once);
            _waitableFactoryMock.Verify(m => m.Select(options.Attempts), Times.Once);
            _waitableMock.Verify(m => m.Wait(TimeSpan.FromMilliseconds(expected)), Times.Once);
            Single(exception.InnerExceptions);
        }
    }
}