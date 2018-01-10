using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Core.Auxiliery
{
    using System;

    /// <summary>
    /// Specifies a synchronous delayer contract.
    /// </summary>
    internal interface ISyncDelayer
    {
        /// <summary>
        /// Delays by <paramref name="time"/> specified.
        /// </summary>
        /// <param name="time">The time to wait.</param>
        void Delay(TimeSpan time);
    }
}