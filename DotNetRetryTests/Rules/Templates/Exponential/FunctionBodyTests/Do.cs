namespace DotNetRetry.Unit.Tests.Rules.Templates.Exponential.FunctionBodyTests
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
        private readonly FunctionBody _functionBody;
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
            _functionBody = new FunctionBody(_retriableMock.Object, randomMock.Object, _waitableFactoryMock.Object);
        }

        [Fact]
        public void ReturnsTrueWhenActionExecutesSuccessfully()
        {
            // Arrange
            string result;
            const string expected = "Hello World!";
            var exceptions = Enumerable.Repeat(new Exception(), 2).ToList();
            var time = TimeSpan.FromMilliseconds(100);
            var service = new TimerService(time);
            _retriableMock.Object.Options.Config(options =>
            {
                options.Attempts = 3;
                options.Time = time;
            });

            // Act
            var done = _functionBody.Do(() => expected, exceptions, service, 3, out result);

            // Assert
            True(done);
            Equal(expected, result);
        }

        [Fact]
        public void FailingActionDispatchesRetryEventsAddsExceptionsToExceptionListAndReturnsFalse()
        {
            // Arrange
            string result;
            var exceptions = Enumerable.Repeat(new Exception(), 2).ToList();
            var time = TimeSpan.FromMilliseconds(100);
            var service = new TimerService(time);
            _retriableMock.Object.Options.Config(options =>
            {
                options.Attempts = 3;
                options.Time = time;
            });
            var expected = Math.Min(Math.Pow(2, 2) + 10, 100);

            // Act
            var done = _functionBody.Do(() => { throw new Exception("Custom exception"); }, exceptions, service, 3, 
                out result);

            // Assert
            False(done);
            _retriableMock.Verify(m => m.OnBeforeRetryInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnFailureInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnAfterRetryInvocation(), Times.Once);
            _waitableFactoryMock.Verify(m => m.Select(3), Times.Once);
            _waitableMock.Verify(m => m.Wait(TimeSpan.FromMilliseconds(expected)), Times.Once);
            Equal(3, exceptions.Count);
        }

        [Fact]
        public void ThrowsAggregateExceptionWhenExceptionThrownInActionIsInCancellationRuleList()
        {
            // Arrange
            string result;
            var exceptions = new List<Exception>();
            var time = TimeSpan.FromMilliseconds(100);
            var service = new TimerService(time);
            _retriableMock.Object.Options.Config(options =>
            {
                options.Attempts = 3;
                options.Time = time;
            });
            var cancellationRule = new CancellationRule();
            cancellationRule.AddExceptionType(typeof(ArgumentException));
            _retriableMock.Object.CancellationRule = cancellationRule;

            // Act
            var exception = Throws<AggregateException>(() => _functionBody.Do(() =>
            {
                throw new ArgumentException("Custom exception");
            }, exceptions, service, 3, out result));

            // Assert
            _retriableMock.Verify(m => m.OnBeforeRetryInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnFailureInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnAfterRetryInvocation(), Times.Once);
            _waitableFactoryMock.Verify(m => m.Select(3), Times.Never);
            _waitableMock.Verify(m => m.Wait(time), Times.Never);
            Single(exception.InnerExceptions);
        }

        [Fact]
        public void ThrowsAggregateExceptionWhenExceededMaxTimeSetupInCancellationRule()
        {
            // Arrange
            string result;
            var exceptions = new List<Exception>();
            var time = TimeSpan.FromMilliseconds(100);
            var service = new TimerService(time);
            _retriableMock.Object.Options.Config(options =>
            {
                options.Attempts = 3;
                options.Time = time;
            });
            var cancellationRule = new CancellationRule();
            cancellationRule.After(TimeSpan.FromMilliseconds(50));
            _retriableMock.Object.CancellationRule = cancellationRule;
            var expected = Math.Min(Math.Pow(2, 0) + 10, 100);

            // Act
            var exception = Throws<AggregateException>(() => _functionBody.Do(() =>
            {
                throw new ArgumentException("Custom exception");
            }, exceptions, service, 3, out result));

            // Assert
            _retriableMock.Verify(m => m.OnBeforeRetryInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnFailureInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnAfterRetryInvocation(), Times.Once);
            _waitableFactoryMock.Verify(m => m.Select(3), Times.Once);
            _waitableMock.Verify(m => m.Wait(TimeSpan.FromMilliseconds(expected)), Times.Once);
            Single(exception.InnerExceptions);
        }
    }
}