namespace DotNetRetry.Unit.Tests.Factories.ActivatorsFactoryTests
{
    using System;
    using DotNetRetry.Core.Activators;
    using DotNetRetry.Core.Auxiliery;
    using DotNetRetry.Core.Exceptions;
    using DotNetRetry.Factories;
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
        [InlineData(typeof (NullActivator))]
        [InlineData(typeof (TypeActivator))]
        public void ActivatorByType(Type type)
        {
            // Arrange | Act
            var result = _activatorFactory.Select(type);

            // Assert
            NotNull(result);
            IsType(type, result);
        }

        [Fact]
        public void ThrowsActivatorNotFoundExceptionWhenActivatorDoesNotExist()
        {
            // Arrange | Act
            var exception = Throws<ActivatorNotFoundException>(() => _activatorFactory.Select(typeof(Select)));

            // Assert
            Equal(Constants.ActivatorNotFoundErrorMessage, exception.Message);
        }
    }
}