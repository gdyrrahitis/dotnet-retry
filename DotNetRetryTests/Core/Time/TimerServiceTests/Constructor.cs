namespace DotNetRetry.Unit.Tests.Core.Time.TimerServiceTests
{
    using System;
    using DotNetRetry.Core.Time;
    using Xunit;

    public class Constructor
    {
        [Fact]
        public void InitializesTimeSpanZeroWithDefaultCall()
        {
            // Arrange | Act
            var result = new TimerService();

            // Assert
            Assert.Equal(TimeSpan.Zero, result.Value);
        }

        [Theory]
        [InlineData(10, "seconds")]
        [InlineData(44, "milliseconds")]
        [InlineData(65, "hour")]
        public void InitializesTimeSpanWithValueDefined(int time, string attribute)
        {
            // Arrange
            var timeSpan = ConvertToTimeStampByAttribute(time, attribute);
            
            // Act
            var result = new TimerService(timeSpan);

            // Assert
            Assert.Equal(timeSpan, result.Value);
        }

        private static TimeSpan ConvertToTimeStampByAttribute(int time, string attribute)
        {
            switch (attribute)
            {
                case "seconds": return TimeSpan.FromSeconds(time);
                case "milliseconds": return TimeSpan.FromMilliseconds(time);
                case "hour": return TimeSpan.FromHours(time);
                default: return TimeSpan.Zero;
            }
        }
    }
}