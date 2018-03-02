using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Factories
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;
    using Core.Auxiliery;
    using Rules.Waitables;

    /// <summary>
    /// An <see cref="IWaitable"/> instance creation factory.
    /// </summary>
    internal class WaitableFactory : IWaitableFactory
    {
        private readonly IDictionary<bool, IWaitable> _waitables;

        /// <summary>
        /// Creates new instance of <see cref="WaitableFactory"/>.
        /// </summary>
        /// <param name="retriable">A <see cref="Retriable"/> dependency.</param>
        internal WaitableFactory(Retriable retriable)
        {
            _waitables = new Dictionary<bool, IWaitable>
            {
                { true, new Pauser(new Delayer()) },
                { false, new Stopper(retriable) }
            };
        }

        /// <summary>
        /// Selects and returns an <see cref="IWaitable"/> instance
        /// by checking if <paramref name="attempts"/> is greater than 0 or not.
        /// </summary>
        /// <param name="attempts">The attempts to test if greater than 0 or not.</param>
        /// <returns>An <see cref="IWaitable"/> instance.</returns>
        public IWaitable Select(int attempts) => _waitables[attempts > 0];
    }
}