namespace DotNetRetry.Unit.Tests.Rules.Templates.Sequential.FunctionBodyTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DotNetRetry.Core.Abstractions;
    using DotNetRetry.Factories;
    using DotNetRetry.Rules.Cancellation;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Rules.Templates.Sequential;
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
            _functionBody = new FunctionBody(_retriableMock.Object, _waitableFactoryMock.Object);
        }

        [Fact]
        public void ReturnsTrueWhenActionExecutesSuccessfully()
        {
            // Arrange
            string result;
            const string expected = "Hello World!";
            var exceptions = Enumerable.Repeat(new Exception(), 2).ToList();
            var time = TimeSpan.FromMilliseconds(100);
            var options = new Options(3, time);
            _retriableMock.Object.Options.Config(options);

            // Act
            var done = _functionBody.Do(() => expected, exceptions, options.Time, options.Attempts, out result);

            // Assert
            True(done);
            Equal(expected, expected);
        }

        [Fact]
        public void FailingActionDispatchesRetryEventsAddsExceptionsToExceptionListAndReturnsFalse()
        {
            // Arrange
            string result;
            var exceptions = Enumerable.Repeat(new Exception(), 2).ToList();
            var time = TimeSpan.FromMilliseconds(100);
            var options = new Options(3, time);
            _retriableMock.Object.Options.Config(options);

            // Act
            var done = _functionBody.Do(() => { throw new Exception("Custom exception"); }, exceptions, options.Time, 
                options.Attempts, out result);

            // Assert
            False(done);
            _retriableMock.Verify(m => m.OnBeforeRetryInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnFailureInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnAfterRetryInvocation(), Times.Once);
            _waitableFactoryMock.Verify(m => m.Select(options.Attempts), Times.Once);
            _waitableMock.Verify(m => m.Wait(options.Time), Times.Once);
            Equal(3, exceptions.Count);
        }

        [Fact]
        public void ThrowsAggregateExceptionWhenExceptionThrownInFunctionIsInCancellationRuleList()
        {
            // Arrange
            string result;
            var exceptions = new List<Exception>();
            var time = TimeSpan.FromMilliseconds(100);
            var options = new Options(3, time);
            _retriableMock.Object.Options.Config(options);
            var cancellationRule = new CancellationRule();
            cancellationRule.AddExceptionType(typeof(ArgumentException));
            _retriableMock.Object.CancellationRule = cancellationRule;

            // Act
            var exception = Throws<AggregateException>(() => _functionBody.Do(() =>
            {
                throw new ArgumentException("Custom exception");
            }, exceptions, options.Time, options.Attempts, out result));

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
            string result;
            var exceptions = new List<Exception>();
            var time = TimeSpan.FromMilliseconds(100);
            var options = new Options(3, time);
            _retriableMock.Object.Options.Config(options);
            var cancellationRule = new CancellationRule();
            cancellationRule.After(TimeSpan.FromMilliseconds(50));
            _retriableMock.Object.CancellationRule = cancellationRule;

            // Act
            var exception = Throws<AggregateException>(() => _functionBody.Do(() =>
            {
                throw new ArgumentException("Custom exception");
            }, exceptions, options.Time, options.Attempts, out result));

            // Assert
            _retriableMock.Verify(m => m.OnBeforeRetryInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnFailureInvocation(), Times.Once);
            _retriableMock.Verify(m => m.OnAfterRetryInvocation(), Times.Once);
            _waitableFactoryMock.Verify(m => m.Select(options.Attempts), Times.Once);
            _waitableMock.Verify(m => m.Wait(options.Time), Times.Once);
            Single(exception.InnerExceptions);
        }
    }
}