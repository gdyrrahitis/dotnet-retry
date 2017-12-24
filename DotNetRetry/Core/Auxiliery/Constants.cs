using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Core.Auxiliery
{
    /// <summary>
    /// 
    /// </summary>
    internal class Constants
    {
        /// <summary>
        /// 
        /// </summary>
        public const string InvalidOperationExceptionErrorMessage = "Fatal error in function retry. Reached unreachable code section.";

        /// <summary>
        /// Unit test project namespace.
        /// </summary>
        public const string TestProject = "DotNetRetry.Unit.Tests";
    }
}