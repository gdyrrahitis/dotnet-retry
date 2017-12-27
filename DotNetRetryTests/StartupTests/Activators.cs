namespace DotNetRetry.Unit.Tests.StartupTests
{
    using System.Linq;
    using DotNetRetry.Core.Activators;
    using Xunit;
    using static Xunit.Assert;

    public class Activators
    {
        [Fact]
        public void ShouldScanAndReturnAllActivators()
        {
            // Arrange |  Act
            var result = Startup.Activators.ToList();

            // Assert
            Equal(2, result.Count);
            Contains(result, x => x.GetType() == typeof(NullActivator));
            Contains(result, x => x.GetType() == typeof(TypeActivator));
        }
    }
}