namespace DotNetRetry.Unit.Tests.Core.Time.TimerServiceTests
{
    using System;
    using DotNetRetry.Core.Time;
    using Xunit;
    using static Xunit.Assert;

    public class Add
    {
        [Theory]
        [InlineData(10, 5, 15)]
        [InlineData(19, 995, 1014)]
        [InlineData(123, 567, 690)]
        [InlineData(20, 200, 220)]
        public void TimeToCurrentSuccessfully(int initial, int toAdd, int expected)
        {
            // Arrange
            var timer = new TimerService(TimeSpan.FromMilliseconds(initial));
            
            // Act
            var result = timer.Add(TimeSpan.FromMilliseconds(toAdd));

            // Assert
            Equal(TimeSpan.FromMilliseconds(expected), result);
        }
    }
}