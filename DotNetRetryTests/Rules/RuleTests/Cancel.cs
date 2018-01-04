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
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void PolicyForBaseExceptionForNonReturnableMethod(Strategy input)
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.Setup(input)
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

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void PolicyForBaseExceptionForReturnableMethod(Strategy input)
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.Setup(input)
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

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void PolicyForArgumentExceptionForNonReturnableMethod(Strategy input)
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.Setup(input)
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

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void PolicyForArgumentExceptionForReturnableMethod(Strategy input)
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.Setup(Strategy.Sequential)
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

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void PolicyForExceedingAmountOfTimeForNonReturnableMethod(Strategy input)
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.Setup(input)
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

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void PolicyForExceedingAmountOfTimeForReturnableMethod(Strategy input)
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.Setup(input)
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
        [InlineData(300, 5, 100, 3, typeof(ArgumentException), Strategy.Sequential)]
        [InlineData(300, 6, 100, 3, typeof(ArgumentException), Strategy.Exponential)]
        [InlineData(300, 5, 100, 1, typeof(Exception), Strategy.Sequential)]
        [InlineData(300, 5, 100, 1, typeof(Exception), Strategy.Exponential)]
        public void ApplyingBothPoliciesOnNonReturnableMethod(int cancelAfter, int totalAttempts, int timeToWait, 
            int expected, Type type, Strategy strategy)
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.Setup(strategy)
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
        [InlineData(300, 5, 100, 3, typeof(ArgumentException), Strategy.Sequential)]
        [InlineData(300, 6, 100, 3, typeof(ArgumentException), Strategy.Exponential)]
        [InlineData(300, 5, 100, 1, typeof(Exception), Strategy.Sequential)]
        [InlineData(300, 5, 100, 1, typeof(Exception), Strategy.Exponential)]
        public void ApplyingBothPoliciesOnReturnableMethod(int cancelAfter, int totalAttempts, int timeToWait,
            int expected, Type type, Strategy strategy)
        {
            // Arrange
            var attempts = 0;
            var rule = Rule.Setup(strategy)
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