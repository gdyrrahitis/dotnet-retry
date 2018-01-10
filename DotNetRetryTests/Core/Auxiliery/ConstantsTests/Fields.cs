namespace DotNetRetry.Unit.Tests.Core.Auxiliery.ConstantsTests
{
    using DotNetRetry.Core.Auxiliery;
    using Xunit;
    using static Xunit.Assert;

    public class Fields
    {
        [Fact]
        public void InvalidOperationExceptionErrorMessageShouldReturnValue()
        {
            Equal("Fatal error in function retry. Reached unreachable code section.", 
                Constants.InvalidOperationExceptionErrorMessage);
        }

        [Fact]
        public void TestProjectShouldReturnValue()
        {
            Equal("DotNetRetry.Unit.Tests", Constants.UnitTestProject);
        }

        [Fact]
        public void ActivatorNotFoundErrorMessageReturnValue()
        {
            Equal("Could not find activator.", Constants.ActivatorNotFoundErrorMessage);
        }
    }
}