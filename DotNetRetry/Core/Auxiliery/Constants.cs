using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
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
        public const string TestProject = "DotNetRetry.Unit.Tests";
    }
}