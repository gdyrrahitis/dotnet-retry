namespace DotNetRetry.Unit.Tests.Rules.Cancellation.CancellationRuleTests
{
    using System;
    using System.Collections.Generic;
    using DotNetRetry.Core.Exceptions;
    using DotNetRetry.Rules.Cancellation;
    using Xunit;
    using static Xunit.Assert;

    public class IsIn
    {
        private readonly List<Type> _exceptions;

        public IsIn()
        {
            _exceptions = new List<Type>
            {
                typeof(Exception),
                typeof(ArgumentOutOfRangeException),
                typeof(NotImplementedException)
            };
        }

        [Theory]
        [InlineData(typeof(Exception), true)]
        [InlineData(typeof(ArgumentOutOfRangeException), true)]
        [InlineData(typeof(NotImplementedException), true)]
        [InlineData(typeof(ArgumentException), false)]
        [InlineData(typeof(MissingMethodException), false)]
        [InlineData(typeof(RuleNotFoundException), false)]
        public void ShouldCheckIfExceptionTypeIsContainedInCollection(Type type, bool isIn)
        {
            // Arrange
            var cancellationRule = new CancellationRule();
            _exceptions.ForEach(t => cancellationRule.AddExceptionType(t));
            var exception = Activator.CreateInstance(type);

            // Act
            var result = cancellationRule.IsIn(exception);

            // Assert
            Equal(isIn, result);
        }
    }
}