using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Core.Auxiliery
{
    using System;

    /// <summary>
    /// Constant values for internal use.
    /// </summary>
    internal class Constants
    {
        /// <summary>
        /// Error message for <see cref="InvalidOperationException"/> exception.
        /// </summary>
        public const string InvalidOperationExceptionErrorMessage = "Fatal error in function retry. Reached unreachable code section.";

        /// <summary>
        /// Error message for <see cref="InvalidOperationException"/> when trying to find an activator.
        /// </summary>
        public const string ActivatorNotFoundErrorMessage = "Could not find activator.";

        /// <summary>
        /// Unit test project namespace.
        /// </summary>
        public const string UnitTestProject = "DotNetRetry.Unit.Tests";

        /// <summary>
        /// Integration test project namespace.
        /// </summary>
        public const string IntegrationTestProject = "DotNetRetry.Integration.Tests";

        /// <summary>
        /// Common test project namespace.
        /// </summary>
        public const string CommonTestProject = "DotNetRetry.Tests.Common";
    }
}