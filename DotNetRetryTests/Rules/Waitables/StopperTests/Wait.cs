namespace DotNetRetry.Unit.Tests.Rules.Waitables.StopperTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DotNetRetry.Rules.Waitables;
    using Xunit;
    using static Xunit.Assert;

    public class Wait
    {
        [Fact]
        public void ShouldNotThrowAggregateExceptionWhenNoExceptionsAreSet()
        {
            // Arrange
            var stopper = new Stopper();

            // Act | Assert
            stopper.Wait(TimeSpan.FromMilliseconds(10));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(12)]
        public void ShouldThrowAggregateExceptionWhenExceptionsAreSet(int count)
        {
            // Arrange
            var exceptions = Enumerable.Repeat(new Exception(), count);
            var stopper = new Stopper { Exceptions = exceptions };

            // Act
            var exception = Throws<AggregateException>(() => stopper.Wait(TimeSpan.FromMilliseconds(10)));

            // Assert
            Equal(count, exception.InnerExceptions.Count);
        }
    }
}