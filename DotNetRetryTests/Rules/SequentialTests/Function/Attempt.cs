namespace DotNetRetry.Tests.Rules.SequentialTests.Function
{
    using System;
    using System.Diagnostics;
    using DotNetRetry.Rules;
    using NUnit.Framework;
    using static NUnit.Framework.Assert;

    [TestFixture]
    public class Attempt
    {
        private Sequential _rule;

        [SetUp]
        public void Setup()
        {
            var rule = RetryRule.SetupRules();
            _rule = new Sequential(rule);
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
            TestDelegate action = () => _rule.Attempt(function, 3, TimeSpan.FromSeconds(1));

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
            Throws<AggregateException>(() => _rule.Attempt(function, 3, TimeSpan.FromSeconds(1)));
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
            var result = _rule.Attempt(function, 3, TimeSpan.FromSeconds(1));

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
            var result = _rule.Attempt(function, 3, TimeSpan.FromSeconds(1));

            // Assert
            AreEqual(3, tries);
            AreEqual("abc", result);
        }

        [Test]
        public void ThrowsArgumentOutOfRangeExceptionForTriesBeingLessThanOne()
        {
            // Arrange | Act
            TestDelegate action = () => _rule.Attempt(() => "abc", 0, TimeSpan.FromSeconds(1));

            // Assert
            Throws<ArgumentOutOfRangeException>(action);
        }

        [Test]
        public void ThrowsArgumentExceptionForTimespanBeingZero()
        {
            // Arrange | Act
            TestDelegate action = () => _rule.Attempt(() => "abc", 3, TimeSpan.Zero);

            // Assert
            Throws<ArgumentException>(action);
        }

        [Test]
        public void ThrowsArgumentExceptionForTimespanBeingMinValue()
        {
            // Arrange | Act
            TestDelegate action = () => _rule.Attempt(() => "abc", 3, TimeSpan.MinValue);

            // Assert
            Throws<ArgumentException>(action);
        }
    }
}