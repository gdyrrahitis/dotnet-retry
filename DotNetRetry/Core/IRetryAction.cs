namespace DotNetRetry.Core
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
        void Attempt(Action action);
    }
}