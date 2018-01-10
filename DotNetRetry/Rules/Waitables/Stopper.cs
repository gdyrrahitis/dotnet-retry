using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules.Waitables
{
    using System;
    using System.Collections.Generic;
    using Core.Auxiliery;

    /// <summary>
    /// Stops execution.
    /// </summary>
    internal class Stopper: IWaitable
    {
        /// <summary>
        /// Property injector for failures happened up to this point.
        /// </summary>
        public IEnumerable<Exception> Exceptions { get; set; }

        /// <summary>
        /// Waits for <paramref name="waitTime"/>.
        /// </summary>
        /// <param name="waitTime">The time to wait.</param>
        public void Wait(TimeSpan waitTime) =>
            Exceptions?.ThrowFlattenAggregateException();
    }
}