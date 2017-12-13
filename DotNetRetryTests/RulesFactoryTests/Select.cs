namespace DotNetRetry.Tests.RulesFactoryTests
{
    using System;
    using System.Collections.Generic;
    using DotNetRetry.Exceptions;
    using DotNetRetry.Rules;
    using NUnit.Framework;
    using Strategy.Activators;
    using static NUnit.Framework.Assert;

    [TestFixture]
    public class Select
    {
        private ActivatorsFactory _activatorFactory;
        private Dictionary<Type, object[]> _rules;

        [SetUp]
        public void Setup()
        {
            var retryRule = RetryRule.SetupRules();
            var activators = new List<IActivator>
            {
                new NullActivator(),
                new TypeActivator()
            };

            _activatorFactory = new ActivatorsFactory(activators);
            _rules = new Dictionary<Type, object[]>
            {
                { typeof(Sequential), new object[]{ retryRule } },
                { typeof(Exponential), new object[]{retryRule } }
            };
        }

        [TestCase(Rules.Sequential, typeof(Sequential))]
        [TestCase(Rules.Exponential, typeof(Exponential))]
        public void SelectRule(Rules rule, Type type)
        {
            // Arrange
            var factory = new RulesFactory(_rules, _activatorFactory);

            // Act
            var result = factory.Select(rule);

            // Assert
            AreEqual(type, result.GetType());
        }

        [Test]
        public void ThrowsRuleNotFoundExceptionWhenRuleDoesNotExist()
        {
            // Arrange
            var factory = new RulesFactory(_rules, _activatorFactory);

            // Act
            var exception = Throws<RuleNotFoundException>(() => factory.Select((Rules) 3));

            // Assert
            AreEqual("Could not find rule.", exception.Message);
        }
    }
}