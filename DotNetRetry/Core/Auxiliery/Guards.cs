using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Core.Auxiliery
{
    using System;
    using Bytes2you.Validation;

    /// <summary>
    /// Guard class for internal validation.
    /// </summary>
    internal static class Guards
    {
        /// <summary>
        /// Validates arguments <paramref name="attempts"/> and <paramref name="timeBetweenRetries"/>.
        /// </summary>
        /// <param name="attempts">Number of attempts.</param>
        /// <param name="timeBetweenRetries">Time to wait between retries.</param>
        internal static void ValidateArguments(int attempts, TimeSpan timeBetweenRetries)
        {
            Guard.WhenArgument(attempts, nameof(attempts)).IsLessThan(1).Throw();
            Guard.WhenArgument(timeBetweenRetries, nameof(timeBetweenRetries))
                .IsLessThanOrEqual(TimeSpan.Zero)
                .Throw();
        }

        /// <summary>
        /// Validates argument <paramref name="attempts"/>.
        /// </summary>
        /// <param name="attempts">Number of attempts.</param>
        internal static void ValidateArguments(int attempts) => 
            Guard.WhenArgument(attempts, nameof(attempts)).IsLessThan(1).Throw();

        /// <summary>
        /// Validates argument <paramref name="timeBetweenRetries"/>.
        /// </summary>
        /// <param name="timeBetweenRetries">Time to wait between retries.</param>
        internal static void ValidateArguments(TimeSpan timeBetweenRetries) => 
            Guard.WhenArgument(timeBetweenRetries, nameof(timeBetweenRetries))
            .IsLessThanOrEqual(TimeSpan.Zero)
            .Throw();
    }
}