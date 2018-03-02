namespace DotNetRetry.Unit.Tests.Core.Auxiliery.ExceptionsExtensionTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DotNetRetry.Core.Auxiliery;
    using Xunit;
    using static Xunit.Assert;

    public class ThrowFlattenAggregateException
    {
        [Fact]
        public void ReturnsNewAggregateExceptionInstance()
        {
            // Arrange
            var exceptions = new List<Exception>
            {
                new Exception("Exception #1"),
                new Exception("Exception #2"),
                new Exception("Exception #3")
            };

            // Act
            var result = Throws<AggregateException>(() => 
                exceptions.ThrowFlattenAggregateException());

            // Assert
            Equal(3, result.InnerExceptions.Count);
            NotEmpty(result.InnerExceptions.Where(e => e.Message == "Exception #1"));
            NotEmpty(result.InnerExceptions.Where(e => e.Message == "Exception #2"));
            NotEmpty(result.InnerExceptions.Where(e => e.Message == "Exception #3"));
        }
    }
}