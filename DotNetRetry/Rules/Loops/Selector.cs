using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
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
        /// <returns></returns>
        internal static Looper Pick(Retriable retriable, ActionBodyTemplate actionBody) => 
            retriable.Options.Attempts > 0 ? 
                new Finite(actionBody, retriable) as Looper :
                new Forever(actionBody, retriable) as Looper;
    }
}