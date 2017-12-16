namespace DotNetRetry.Tests.Rules.SequentialTests.Action
{
    using System;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class Attempt
    {
        private readonly Sequential _rule;

        public Attempt()
        {
            var rule = RetryRule.SetupRules(Rule.Sequential);
            _rule = new Sequential(rule);
        }

        [Fact]
        public void SuccessAtFirstTry()
        {
            // Arrange
            var actual = 0;
            Action successFullAction = () =>
            {
                const string intAsString = "15";
                var stringToInt = int.Parse(intAsString);
                actual = stringToInt;
            };

            // Act
            _rule.Attempt(successFullAction, 3, TimeSpan.FromSeconds(2));

            // Assert
            Equal(15, actual);
        }

        [Fact]
        public void SuccessAtSecondTry()
        {
            // Arrange
            var actual = 0;
            var tries = 0;
            Action successAtSecondTryAction = () =>
            {
                const string invalidNumber = "ab123";
                if (tries == 2)
                {
                    var validNumber = invalidNumber.Replace("ab", "");
                    actual = int.Parse(validNumber);
                }
                else
                {
                    tries++;
                    actual = int.Parse(invalidNumber);
                }
            };

            // Act
            _rule.Attempt(successAtSecondTryAction, 5, TimeSpan.FromSeconds(1));

            // Assert
            Equal(2, tries);
            Equal(123, actual);
        }

        [Fact]
        public void SuccessAtThirdTry()
        {
            // Arrange
            var actual = 0;
            var tries = 0;
            Action successAtThirdTryAction = () =>
            {
                const string invalidNumber = "ab123";
                if (tries == 3)
                {
                    var validNumber = invalidNumber.Replace("ab", "");
                    actual = int.Parse(validNumber);
                }
                else
                {
                    tries++;
                    actual = int.Parse(invalidNumber);
                }
            };

            // Act
            _rule.Attempt(successAtThirdTryAction, 5, TimeSpan.FromSeconds(1));

            // Assert
            Equal(3, tries);
            Equal(123, actual);
        }

        [Fact]
        public void FailureAfterAllTriesReturnsAggregateExceptionWithAllTheExceptionsOccurred()
        {
            // Arrange
            var actual = 0;
            var tries = 0;
            Action failureAction = () =>
            {
                tries++;
                const string invalidNumber = "123abc";
                actual = int.Parse(invalidNumber);
            };

            // Act
            var exception = Throws<AggregateException>(() => _rule.Attempt(failureAction, 3, TimeSpan.FromSeconds(1)));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
            Equal(0, actual);
        }

        [Fact]
        public void SuccessAtFirstTryWithParameterPassed()
        {
            // Arrange
            const string parameter = "123456";
            var actual = 0;
            Action<string> convertToIntAction = s => actual = int.Parse(s);

            // Act
            _rule.Attempt(() => convertToIntAction(parameter), 3, TimeSpan.FromSeconds(1));

            // Assert
            Equal(123456, actual);
        }

        [Fact]
        public void SuccessAtSecondTryWithParameterPassed()
        {
            // Arrange
            const string parameter = "abc123456";
            var actual = 0;
            var tries = 0;
            Action<string> convertToIntAction = s =>
            {
                if (tries == 2)
                {
                    s = s.Replace("abc", "");
                    actual = int.Parse(s);
                }
                else
                {
                    tries++;
                    actual = int.Parse(s);
                }
            };

            // Act
            _rule.Attempt(() => convertToIntAction(parameter), 6, TimeSpan.FromSeconds(1));

            // Assert
            Equal(2, tries);
            Equal(123456, actual);
        }

        [Fact]
        public void FailureAfterAllTriesWithParameterPassed()
        {
            // Arrange
            const string parameter = "abcd123";
            var actual = 0;
            var tries = 0;
            Action<string> failureAction = s =>
            {
                tries++;
                actual = int.Parse(s);
            };

            // Act
            var exception = Throws<AggregateException>(() => _rule.Attempt(() => failureAction(parameter), 3, TimeSpan.FromSeconds(1)));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
            Equal(0, actual);
        }

        [Fact]
        public void ThrowsArgumentOutOfRangeExceptionForTriesBeingLessThanOne()
        {
            // Arrange | Act
            var exception = Throws<ArgumentOutOfRangeException>(() => _rule.Attempt(() => { }, 0, TimeSpan.FromSeconds(1)));

            // Assert
            Equal("", exception.Message);
        }

        [Fact]
        public void ThrowsArgumentExceptionForTimespanBeingZero()
        {
            // Arrange | Act
            var exception = Throws<ArgumentException>(() => _rule.Attempt(() => { }, 3, TimeSpan.Zero));

            // Assert
            Equal("", exception.Message);
        }

        [Fact]
        public void ThrowsArgumentExceptionForTimespanBeingMinValue()
        {
            // Arrange | Act
            var exception = Throws<ArgumentException>(() => _rule.Attempt(() => { }, 3, TimeSpan.MinValue));

            // Assert
            Equal("", exception.Message);
        }
    }
}
