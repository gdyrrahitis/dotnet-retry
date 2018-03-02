namespace DotNetRetry.Unit.Tests.StartupTests
{
    using DotNetRetry.Factories;
    using Xunit;
    using static Xunit.Assert;

    public class Configure
    {
        [Fact]
        public void CreatesNewRulesFactoryInstance()
        {
            // Arrange | Act
            var result = Startup.Configure();

            // Assert
            IsType<RulesFactory>(result);
        }
    }
}