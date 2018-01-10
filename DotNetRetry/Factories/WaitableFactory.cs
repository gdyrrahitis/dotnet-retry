using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Factories
{
    using System.Collections.Generic;
    using Core.Auxiliery;
    using Rules.Waitables;

    /// <summary>
    /// An <see cref="IWaitable"/> instance creation factory.
    /// </summary>
    internal class WaitableFactory : IWaitableFactory
    {
        private static readonly IDictionary<bool, IWaitable> Waitables = new Dictionary<bool, IWaitable>
        {
            { true, new Pauser(new Delayer()) },
            { false, new Stopper() }
        };

        /// <summary>
        /// Selects and returns an <see cref="IWaitable"/> instance
        /// by checking if <paramref name="attempts"/> is greater than 0 or not.
        /// </summary>
        /// <param name="attempts">The attempts to test if greater than 0 or not.</param>
        /// <returns>An <see cref="IWaitable"/> instance.</returns>
        public IWaitable Select(int attempts) => Waitables[attempts > 0];
    }
}