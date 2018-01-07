using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules.Waitables
{
    using System;
    using System.Collections.Generic;
    using Core.Auxiliery;

    /// <summary>
    /// Pauses execution for specified amount of time.
    /// </summary>
    internal class Pauser: IWaitable
    {
        private readonly IDelayer _delayer;

        /// <summary>
        /// Creates a new <see cref="Pauser"/> instance.
        /// </summary>
        /// <param name="delayer"></param>
        public Pauser(IDelayer delayer)
        {
            _delayer = delayer;
        }

        /// <summary>
        /// Property injector for failures happened up to this point.
        /// </summary>
        public IEnumerable<Exception> Exceptions { get; set; }

        /// <summary>
        /// Waits for <paramref name="waitTime"/>.
        /// </summary>
        /// <param name="waitTime">The time to wait.</param>
        public void Wait(TimeSpan waitTime) => _delayer.Delay(waitTime);
    }
}