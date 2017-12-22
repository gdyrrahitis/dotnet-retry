namespace DotNetRetry.Tests.RulesFactoryTests
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;
    using Core.Activators;
    using Core.Exceptions;
    using DotNetRetry.Rules;
    using Factories;
    using Moq;
    using Xunit;
    using static Xunit.Assert;

    public class Select
    {
        private readonly ActivatorsFactory _activatorFactory;

        public Select()
        {
            var activators = new List<IActivator>
            {
                new NullActivator(),
                new TypeActivator()
            };

            _activatorFactory = new ActivatorsFactory(activators);
        }

        [Theory]
        [InlineData(Strategies.Sequential, typeof(Sequential))]
        [InlineData(Strategies.Exponential, typeof(Exponential))]
        public void SelectRule(Strategies strategies, Type type)
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
            var result = factory.Select(strategies, new object[] { retriableMock.Object });

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
            var exception = Throws<RuleNotFoundException>(() => factory.Select((Strategies)3));

            // Assert
            Equal("Could not find rule.", exception.Message);
        }
    }
}