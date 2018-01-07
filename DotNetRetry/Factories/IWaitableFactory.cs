using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Factories
{
    using Rules.Waitables;

    /// <summary>
    /// Defines a contract for an <see cref="IWaitable"/> factory.
    /// </summary>
    internal interface IWaitableFactory
    {
        /// <summary>
        /// Selects and returns an <see cref="IWaitable"/> instance
        /// by checking if <paramref name="attempts"/> is greater than 0 or not.
        /// </summary>
        /// <param name="attempts">The attempts to test if greater than 0 or not.</param>
        /// <returns>An <see cref="IWaitable"/> instance.</returns>
        IWaitable Select(int attempts);
    }
}