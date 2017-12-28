namespace DotNetRetry.Core
{
    using System;

    /// <summary>
    /// A contract for returnable methods.
    /// </summary>
    public interface IRetryFunction
    {
        /// <summary>
        /// Attempts to invoke a method in a sequential order and return its result.
        /// </summary>
        /// <typeparam name="T">The return type of the method to call.</typeparam>
        /// <param name="function">The method to call.</param>
        /// <param name="attempts">The number of retries.</param>
        /// <param name="time">The time between each retry.</param>
        /// <returns>The same return type as the <paramref name="function"/> passed.</returns>
        T Attempt<T>(Func<T> function, int attempts, TimeSpan time);
    }
}