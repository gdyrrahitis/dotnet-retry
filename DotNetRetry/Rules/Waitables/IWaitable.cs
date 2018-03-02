using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules.Waitables
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a contract for an <see cref="IWaitable"/>.
    /// </summary>
    internal interface IWaitable
    {
        /// <summary>
        /// Property injector for failures happened up to this point.
        /// </summary>
        IEnumerable<Exception> Exceptions { set; }

        /// <summary>
        /// Waits for <paramref name="waitTime"/>.
        /// </summary>
        /// <param name="waitTime">The time to wait.</param>
        void Wait(TimeSpan waitTime);
    }
}