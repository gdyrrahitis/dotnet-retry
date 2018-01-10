using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Core.Auxiliery
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Timer service to use timer's delay.
    /// </summary>
    internal class Delayer: IDelayer
    {
        /// <summary>
        /// Delays by <paramref name="time"/> specified.
        /// </summary>
        /// <param name="time">The time to wait.</param>
        public void Delay(TimeSpan time) => Task.Delay(time).Wait();
    }
}