namespace DotNetRetry.Rules
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;

    /// <summary>
    /// 
    /// </summary>
    public abstract class FunctionPolicyBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly Retriable Retriable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="retriable"></param>
        protected FunctionPolicyBase(Retriable retriable)
        {
            Retriable = retriable;
        }

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="function">The function to try execute</param>
        /// <param name="attempts">Total attempts</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        /// <returns>The function return value</returns>
        public T Attempt<T>(Func<T> function, ref int attempts, TimeSpan timeBetweenRetries,
            List<Exception> exceptions, TimeSpan time)
        {
            Retriable.OnBeforeRetryInvocation();
            var result = Do(function, ref attempts, timeBetweenRetries, exceptions, time);
            Retriable.OnAfterRetryInvocation();
            return result;
        }

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="function">The function to try execute</param>
        /// <param name="attempts">Total attempts</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        /// <returns>The function return value</returns>
        protected abstract T Do<T>(Func<T> function, ref int attempts, TimeSpan timeBetweenRetries,
            List<Exception> exceptions, TimeSpan time);
    }
}