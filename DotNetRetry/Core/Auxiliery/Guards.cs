namespace DotNetRetry.Core.Auxiliery
{
    using System;
    using Bytes2you.Validation;

    /// <summary>
    /// 
    /// </summary>
    public static class Guards
    {
        /// <summary>
        /// Validates arguments <paramref name="attempts"/> and <paramref name="timeBetweenRetries"/>.
        /// </summary>
        /// <param name="attempts">Number of attempts.</param>
        /// <param name="timeBetweenRetries">Time to wait between retries.</param>
        public static void ValidateArguments(int attempts, TimeSpan timeBetweenRetries)
        {
            Guard.WhenArgument(attempts, nameof(attempts)).IsLessThan(1).Throw();
            Guard.WhenArgument(timeBetweenRetries, nameof(timeBetweenRetries))
                .IsLessThanOrEqual(TimeSpan.Zero)
                .Throw();
        }
    }
}