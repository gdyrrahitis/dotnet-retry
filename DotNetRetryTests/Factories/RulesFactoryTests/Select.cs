namespace DotNetRetry.Unit.Tests.Factories.RulesFactoryTests
{
    using System;
    using System.Collections.Generic;
    using DotNetRetry.Core.Abstractions;
    using DotNetRetry.Core.Activators;
    using DotNetRetry.Core.Exceptions;
    using DotNetRetry.Factories;
    using DotNetRetry.Rules;
    using Moq;
    using Xunit;
    using static Xunit.Assert;

    public class Select
    {
        private readonly ActivatorsFactory _activatorFactory;

        public Select()
        {
            _activatorFactory = new ActivatorsFactory(RulesDataSource.Activators);
        }

        [Theory]
        [InlineData(Strategy.Sequential, typeof(Sequential))]
        [InlineData(Strategy.Exponential, typeof(Exponential))]
        public void SelectRule(Strategy strategy, Type type)
        {
            // Arrange
            var rules = new List<Type>
            {
                typeof(Sequential),
                typeof(Exponential)
            };
            var factory = new RulesFactory(rules, _activatorFactory);
            var retriableMock = new Mock<Retriable>();

            // Act
            var result = factory.Select(strategy, new object[] { retriableMock.Object });

            // Assert
            Equal(type, result.GetType());
        }

        [Fact]
        public void ThrowsRuleNotFoundExceptionWhenRuleDoesNotExist()
        {
            // Arrange
            var rules = new List<Type>
            {
                typeof(Sequential),
                typeof(Exponential)
            };
            var factory = new RulesFactory(rules, _activatorFactory);

            // Act
            var exception = Throws<RuleNotFoundException>(() => factory.Select((Strategy)3));

            // Assert
            Equal("Could not find rule.", exception.Message);
        }
    }
}