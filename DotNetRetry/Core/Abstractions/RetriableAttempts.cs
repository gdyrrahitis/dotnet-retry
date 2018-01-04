namespace DotNetRetry.Core.Abstractions
{
    using System;

    /// <summary>
    /// A base class for retriable.
    /// </summary>
    public abstract partial class Retriable: IRetry
    {
        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute.</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed.</exception>
        public abstract void Attempt(Action action);

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns.</typeparam>
        /// <param name="function">The function to try execute.</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed.</exception>
        /// <returns>The function return value.</returns>
        public abstract T Attempt<T>(Func<T> function);
    }
}