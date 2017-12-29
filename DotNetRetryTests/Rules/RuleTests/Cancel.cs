namespace DotNetRetry.Unit.Tests.Rules.RuleTests
{
    using System;
    using System.Linq;
    using DotNetRetry.Rules;
    using DotNetRetry.Rules.Configuration;
    using Xunit;
    using static Xunit.Assert;

    public class Cancel
    {
        // Cancel for specified exceptions (data driven)
        [Fact]
        public void PolicyForBaseExceptionForNonReturnableMethod()
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.SetupRules(Strategies.Sequential)
                .Config(new Options(3, TimeSpan.FromMilliseconds(100)));
            rule.Cancel(c => c.OnFailure<Exception>());

            // Act
            var exception = Throws<AggregateException>(() => rule.Attempt(() =>
            {
                attempts++;
               throw new Exception("Custom Exception");
            }));

            // Assert
            Equal(1, attempts);
            Single(exception.InnerExceptions);
        }

        [Fact]
        public void PolicyForBaseExceptionForReturnableMethod()
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.SetupRules(Strategies.Sequential)
                .Config(new Options(3, TimeSpan.FromMilliseconds(100)));
            rule.Cancel(c => c.OnFailure<Exception>());

            // Act
            var exception = Throws<AggregateException>(() => rule.Attempt(() =>
            {
                attempts++;
                throw new Exception("Custom Exception");
#pragma warning disable 162
                return "Return value";
#pragma warning restore 162
            }));

            // Assert
            Equal(1, attempts);
            Single(exception.InnerExceptions);
        }

        [Fact]
        public void PolicyForArgumentExceptionForNonReturnableMethod()
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.SetupRules(Strategies.Sequential)
                .Config(new Options(3, TimeSpan.FromMilliseconds(100)));
            rule.Cancel(c => c.OnFailure<ArgumentException>());

            // Act
            var exception = Throws<AggregateException>(() => rule.Attempt(() =>
            {
                attempts++;
                if (attempts == 3)
                {
                    throw new ArgumentException();
                }
                throw new Exception("Custom Exception");
            }));

            // Assert
            Equal(3, attempts);
            Equal(3, exception.InnerExceptions.Count);
            Equal(2, exception.InnerExceptions.Count(e => e.GetType().IsEquivalentTo(typeof(Exception))));
            Equal(1, exception.InnerExceptions.Count(e => e.GetType().IsEquivalentTo(typeof(ArgumentException))));
        }

        [Fact]
        public void PolicyForArgumentExceptionForReturnableMethod()
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.SetupRules(Strategies.Sequential)
                .Config(new Options(3, TimeSpan.FromMilliseconds(100)));
            rule.Cancel(c => c.OnFailure<ArgumentException>());

            // Act
            var exception = Throws<AggregateException>(() => rule.Attempt(() =>
            {
                attempts++;
                if (attempts == 3)
                {
                    throw new ArgumentException();
                }
                throw new Exception("Custom Exception");
#pragma warning disable 162
                return "Return value";
#pragma warning restore 162
            }));

            // Assert
            Equal(3, attempts);
            Equal(3, exception.InnerExceptions.Count);
            Equal(2, exception.InnerExceptions.Count(e => e.GetType().IsEquivalentTo(typeof(Exception))));
            Equal(1, exception.InnerExceptions.Count(e => e.GetType().IsEquivalentTo(typeof(ArgumentException))));
        }

        // Cancel for specified amount of time (data driven)
        [Fact]
        public void PolicyForExceedingAmountOfTimeForNonReturnableMethod()
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.SetupRules(Strategies.Sequential)
                .Config(new Options(3, TimeSpan.FromMilliseconds(100)));
            rule.Cancel(c => c.After(TimeSpan.FromMilliseconds(300)));

            // Act
            var exception = Throws<AggregateException>(() => rule.Attempt(() =>
            {
                attempts++;
                throw new Exception("Custom Exception");
            }));

            // Assert
            Equal(3, attempts);
            Equal(3, exception.InnerExceptions.Count);
        }

        [Fact]
        public void PolicyForExceedingAmountOfTimeForReturnableMethod()
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.SetupRules(Strategies.Sequential)
                .Config(new Options(3, TimeSpan.FromMilliseconds(100)));
            rule.Cancel(c => c.After(TimeSpan.FromMilliseconds(300)));

            // Act
            var exception = Throws<AggregateException>(() => rule.Attempt(() =>
            {
                attempts++;
                throw new Exception("Custom Exception");
#pragma warning disable 162
                return "Return value";
#pragma warning restore 162
            }));

            // Assert
            Equal(3, attempts);
            Equal(3, exception.InnerExceptions.Count);
        }

        // Mix and match the above (data driven)
        [Theory]
        [InlineData(300, 5, 100, 3, typeof(ArgumentException))]
        [InlineData(300, 5, 100, 1, typeof(Exception))]
        public void ApplyingBothPoliciesOnNonReturnableMethod(int cancelAfter, int totalAttempts, int timeToWait, 
            int expected, Type type)
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.SetupRules(Strategies.Sequential)
                .Config(new Options(totalAttempts, TimeSpan.FromMilliseconds(timeToWait)));
            rule.Cancel(c => c.After(TimeSpan.FromMilliseconds(cancelAfter)).OnFailure(type));

            // Act
            var exception = Throws<AggregateException>(() => rule.Attempt(() =>
            {
                attempts++;
                throw new Exception("Custom Exception");
            }));

            // Assert
            Equal(expected, attempts);
            Equal(expected, exception.InnerExceptions.Count);
        }

        [Theory]
        [InlineData(300, 5, 100, 3, typeof(ArgumentException))]
        [InlineData(300, 5, 100, 1, typeof(Exception))]
        public void ApplyingBothPoliciesOnReturnableMethod(int cancelAfter, int totalAttempts, int timeToWait,
    int expected, Type type)
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.SetupRules(Strategies.Sequential)
                .Config(new Options(totalAttempts, TimeSpan.FromMilliseconds(timeToWait)));
            rule.Cancel(c => c.After(TimeSpan.FromMilliseconds(cancelAfter)).OnFailure(type));

            // Act
            var exception = Throws<AggregateException>(() => rule.Attempt(() =>
            {
                attempts++;
                throw new Exception("Custom Exception");
#pragma warning disable 162
                return "Return value";
#pragma warning restore 162
            }));

            // Assert
            Equal(expected, attempts);
            Equal(expected, exception.InnerExceptions.Count);
        }
    }
}