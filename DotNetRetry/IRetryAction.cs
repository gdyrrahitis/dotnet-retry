namespace DotNetRetry
{
    using System;

    /// <summary>
    /// A contract for non-returnable methods.
    /// </summary>
    public interface IRetryAction
    {
        /// <summary>
        /// Attempts to invoke a method in a sequential order.
        /// </summary>
        /// <param name="action">The method to call.</param>
        /// <param name="attempts">The number of retries.</param>
        /// <param name="timeBetweenRetries">The time between each retry.</param>
        void Attempt(Action action, int attempts, TimeSpan timeBetweenRetries);
    }
}