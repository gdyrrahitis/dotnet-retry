namespace DotNetRetry.Unit.Tests.Rules.Configuration.RuleOptionsTests
{
    using System;
    using DotNetRetry.Core.Abstractions;
    using DotNetRetry.Rules.Configuration;
    using Moq;
    using Xunit;
    using static Xunit.Assert;

    public class Config
    {
        [Fact]
        public void SetsUpBothAttemptAndTime()
        {
            // Arrange
            var retriableMock = new Mock<Retriable>();
            var ruleOptions = new RuleOptions(retriableMock.Object);
            var options = new Options(1, TimeSpan.FromSeconds(1));

            // Act
            var result = ruleOptions.Config(options);

            // Assert
            Same(retriableMock.Object, result);
            Equal(1, ruleOptions.Attempts);
            Equal(TimeSpan.FromSeconds(1), ruleOptions.Time);
        }

        [Fact]
        public void SetupsAttemptsOnlyWithTimeAsDefaultTimespan()
        {
            // Arrange
            var retriableMock = new Mock<Retriable>();
            var ruleOptions = new RuleOptions(retriableMock.Object);
            var options = new Options(1);

            // Act
            var result = ruleOptions.Config(options);

            // Assert
            Same(retriableMock.Object, result);
            Equal(1, ruleOptions.Attempts);
            Equal(TimeSpan.Zero, ruleOptions.Time);
        }

        [Fact]
        public void SetupsTimeOnlyWithAttemptsAsDefaultInteger()
        {
            // Arrange
            var retriableMock = new Mock<Retriable>();
            var ruleOptions = new RuleOptions(retriableMock.Object);
            var options = new Options(TimeSpan.FromSeconds(1));

            // Act
            var result = ruleOptions.Config(options);

            // Assert
            Same(retriableMock.Object, result);
            Equal(0, ruleOptions.Attempts);
            Equal(TimeSpan.FromSeconds(1), ruleOptions.Time);
        }
    }
}