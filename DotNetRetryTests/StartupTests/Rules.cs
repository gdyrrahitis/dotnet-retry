namespace DotNetRetry.Unit.Tests.StartupTests
{
    using System.Linq;
    using DotNetRetry.Rules;
    using Xunit;
    using static Xunit.Assert;

    public class Rules
    {
        [Fact]
        public void ShouldScanAndReturnAllActivators()
        {
            // Arrange |  Act
            var result = Startup.Rules.ToList();

            // Assert
            Equal(2, result.Count);
            Contains(typeof(Sequential), result);
            Contains(typeof(Exponential), result);
        }
    }
}