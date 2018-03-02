using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules.Loops
{
    using Core.Abstractions;
    using Templates;

    /// <summary>
    /// A <see cref="Looper"/> selector.
    /// </summary>
    internal class Selector
    {
        /// <summary>
        /// Picks a <see cref="Looper"/> based on attempts defined by parent <see cref="Retriable"/>.
        /// </summary>
        /// <param name="retriable">A parent <see cref="Retriable"/> instance.</param>
        /// <param name="actionBody">The specified policy's action body.</param>
        /// <param name="functionBody"></param>
        /// <returns></returns>
        internal static Looper Pick(Retriable retriable, ActionBodyTemplate actionBody, FunctionBodyTemplate functionBody) => 
            retriable.Options.Attempts > 0 ? 
                new Finite(actionBody, functionBody, retriable) as Looper :
                new Forever(actionBody, functionBody, retriable) as Looper;
    }
}