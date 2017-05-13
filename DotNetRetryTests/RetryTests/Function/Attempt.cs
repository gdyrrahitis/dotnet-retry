namespace DotNetRetry.Tests.RetryTests.Function
{
    using System;
    using System.Diagnostics;
    using NUnit.Framework;
    using static NUnit.Framework.Assert;

    [TestFixture]
    public class Attempt
    {
        private IRetry _retry;

        [SetUp]
        public void Setup()
        {
            _retry = new Retry();
        }

        [Test]
        public void ReturnsListOfExceptionsAfterFailedInAllTries()
        {
            // Arrange
            var tries = 0;
            Func<string> function = () =>
            {
                tries++;
                throw new Exception("Unhandled exception");
            };

            // Act
            TestDelegate action = () => _retry.Attempt(function, 3, TimeSpan.FromSeconds(1));

            // Assert
            var exception = Throws<AggregateException>(action);
            AreEqual(3, exception.InnerExceptions.Count);
            AreEqual(3, tries);
        }

        [Test]
        public void TakesTwoSecondsToCompleteAfterThreeRetriesOneSecondEach()
        {
            // Arrange
            var stopwatch = Stopwatch.StartNew();
            Func<string> function = () =>
            {
                throw new Exception("Unhandled exception");
            };

            // Act
            stopwatch.Start();
            Throws<AggregateException>(() => _retry.Attempt(function, 3, TimeSpan.FromSeconds(1)));
            stopwatch.Stop();
            var elapsed = stopwatch.Elapsed;

            // Assert
            AreEqual(2, elapsed.Seconds);
        }

        [Test]
        public void FailsTheFirstTimeButSucceedsOnSecondTryReturningStringValue()
        {
            // Arrange
            var tries = 0;
            Func<string> function = () =>
            {
                if (tries++ < 1)
                {
                    throw new Exception("Unhandled exception");
                }

                return "abc";
            };

            // Act
            var result = _retry.Attempt(function, 3, TimeSpan.FromSeconds(1));

            // Assert
            AreEqual(2, tries);
            AreEqual("abc", result);
        }

        [Test]
        public void FailsTheSecondTimeButSucceedsOnThirdTryReturningStringValue()
        {
            // Arrange
            var tries = 0;
            Func<string> function = () =>
            {
                if (tries++ < 2)
                {
                    throw new Exception("Unhandled exception");
                }

                return "abc";
            };

            // Act
            var result = _retry.Attempt(function, 3, TimeSpan.FromSeconds(1));

            // Assert
            AreEqual(3, tries);
            AreEqual("abc", result);
        }
    }
}