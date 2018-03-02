namespace DotNetRetry.Unit.Tests.Rules.Loops.SelectorTests
{
    using System;
    using DotNetRetry.Core.Abstractions;
    using DotNetRetry.Rules.Configuration;
    using DotNetRetry.Rules.Loops;
    using DotNetRetry.Rules.Templates;
    using Moq;
    using Xunit;
    using static Xunit.Assert;

    public class Pick
    {
        [Theory]
        [InlineData(1, typeof(Finite))]
        [InlineData(0, typeof(Forever))]
        public void SelectsLooperDerivedClassByNumberOfAttempts(int attempts, Type type)
        {
            // Arrange
            var retriableMock = new Mock<Retriable>();
            var actionBodyMock = new Mock<ActionBodyTemplate>(retriableMock.Object);
            var functionBodyMock = new Mock<FunctionBodyTemplate>(retriableMock.Object);
            var options = new RuleOptions(retriableMock.Object);
            if (attempts > 0)
                options.Config(new Options(attempts));
            retriableMock.Object.Options = options;

            // Act
            var result = Selector.Pick(retriableMock.Object, actionBodyMock.Object, functionBodyMock.Object);

            // Assert
            IsType(type, result);
        }
    }
}