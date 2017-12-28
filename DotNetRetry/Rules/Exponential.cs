using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules
{
    using System;
    using Core;
    using Core.Abstractions;

    /// <summary>
    /// An exponential retry technique.
    /// </summary>
    internal class Exponential : IRetry
    {
        private readonly Retriable _retriable;

        /// <summary>
        /// Initializes a new instance of <see cref="Exponential"/> object.
        /// </summary>
        /// <param name="retriable">A <see cref="Retriable"/> object with global rules.</param>
        internal Exponential(Retriable retriable)
        {
            _retriable = retriable;
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <param name="attempts">Total attempts</param>
        /// <param name="maxBackoffTime">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="maxBackoffTime"/> Timespan.Zero or Timespan.MinValue values</exception>
        public void Attempt(Action action, int attempts, TimeSpan maxBackoffTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="function">The function to try execute</param>
        /// <param name="attempts">Total attempts</param>
        /// <param name="maxBackoffTime">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="maxBackoffTime"/> Timespan.Zero or Timespan.MinValue values</exception>
        /// <returns>The function return value</returns>
        public T Attempt<T>(Func<T> function, int attempts, TimeSpan maxBackoffTime)
        {
            throw new NotImplementedException();
        }
    }
}