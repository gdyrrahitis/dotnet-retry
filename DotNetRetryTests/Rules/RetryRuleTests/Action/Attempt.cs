namespace DotNetRetry.Tests.Rules.RetryRuleTests.Action
{
    using System;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class Attempt
    {
        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void SuccessAtFirstTry(Strategies input)
        {
            // Arrange
            var actual = 0;
            var rules = Rule.SetupRules(input);
            Action successFullAction = () =>
            {
                const string intAsString = "15";
                var stringToInt = int.Parse(intAsString);
                actual = stringToInt;
            };

            // Act
            rules.Attempt(successFullAction, 3, TimeSpan.FromSeconds(2));

            // Assert
            Equal(15, actual);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void SuccessAtSecondTry(Strategies input)
        {
            // Arrange
            var actual = 0;
            var tries = 0;
            var rules = Rule.SetupRules(input);
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
            rules.Attempt(successAtSecondTryAction, 5, TimeSpan.FromSeconds(1));

            // Assert
            Equal(2, tries);
            Equal(123, actual);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void SuccessAtThirdTry(Strategies input)
        {
            // Arrange
            var actual = 0;
            var tries = 0;
            var rules = Rule.SetupRules(input);
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
            rules.Attempt(successAtThirdTryAction, 5, TimeSpan.FromSeconds(1));

            // Assert
            Equal(3, tries);
            Equal(123, actual);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void FailureAfterAllTriesReturnsAggregateExceptionWithAllTheExceptionsOccurred(Strategies input)
        {
            // Arrange
            var actual = 0;
            var tries = 0;
            var rules = Rule.SetupRules(input);
            Action failureAction = () =>
            {
                tries++;
                const string invalidNumber = "123abc";
                actual = int.Parse(invalidNumber);
            };

            // Act
            var exception = Throws<AggregateException>(() => rules.Attempt(failureAction, 3, TimeSpan.FromSeconds(1)));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
            Equal(0, actual);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void SuccessAtFirstTryWithParameterPassed(Strategies input)
        {
            // Arrange
            var actual = 0;
            const string parameter = "123456";
            var rules = Rule.SetupRules(input);
            Action<string> convertToIntAction = s => actual = int.Parse(s);

            // Act
            rules.Attempt(() => convertToIntAction(parameter), 3, TimeSpan.FromSeconds(1));

            // Assert
            Equal(123456, actual);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void SuccessAtSecondTryWithParameterPassed(Strategies input)
        {
            // Arrange
            var actual = 0;
            var tries = 0;
            const string parameter = "abc123456";
            var rules = Rule.SetupRules(input);
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
            rules.Attempt(() => convertToIntAction(parameter), 6, TimeSpan.FromSeconds(1));

            // Assert
            Equal(2, tries);
            Equal(123456, actual);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void FailureAfterAllTriesWithParameterPassed(Strategies input)
        {
            // Arrange
            var actual = 0;
            var tries = 0;
            const string parameter = "abcd123";
            var rules = Rule.SetupRules(input);
            Action<string> failureAction = s =>
            {
                tries++;
                actual = int.Parse(s);
            };

            // Act
            var exception = Throws<AggregateException>(() => 
                rules.Attempt(() => failureAction(parameter), 3, TimeSpan.FromSeconds(1)));

            // Assert
            Equal(3, exception.InnerExceptions.Count);
            Equal(3, tries);
            Equal(0, actual);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ThrowsArgumentOutOfRangeExceptionForTriesBeingLessThanOne(Strategies input)
        {
            // Arrange
            var rules = Rule.SetupRules(input);

            // Act
            var exception = Throws<ArgumentOutOfRangeException>(() => 
                rules.Attempt(() => { }, 0, TimeSpan.FromSeconds(1)));

            // Assert
            Equal("Argument value <0> is less than <1>.\r\nParameter name: attempts", exception.Message);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ThrowsArgumentExceptionForTimespanBeingZero(Strategies input)
        {
            // Arrange
            var rules = Rule.SetupRules(input);

            // Act
            var exception = Throws<ArgumentOutOfRangeException>(() => rules.Attempt(() => { }, 3, TimeSpan.Zero));

            // Assert
            Equal($"Argument value <{TimeSpan.Zero}> is less than or equal to <{TimeSpan.Zero}>.\r\nParameter name: timeBetweenRetries", exception.Message);
        }

        [Theory]
        [MemberData(nameof(RulesDataSource.Data), MemberType = typeof(RulesDataSource))]
        public void ThrowsArgumentExceptionForTimespanBeingMinValue(Strategies input)
        {
            // Arrange
            var rules = Rule.SetupRules(input);

            // Act
            var exception = Throws<ArgumentOutOfRangeException>(() => rules.Attempt(() => { }, 3, TimeSpan.MinValue));

            // Assert
            Equal($"Argument value <{TimeSpan.MinValue}> is less than or equal to <{TimeSpan.Zero}>.\r\nParameter name: timeBetweenRetries", exception.Message);
        }
    }
}
