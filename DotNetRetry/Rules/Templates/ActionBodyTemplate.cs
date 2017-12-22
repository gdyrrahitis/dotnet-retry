namespace DotNetRetry.Rules.Templates
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;

    /// <summary>
    /// 
    /// </summary>
    public abstract class ActionBodyTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly Retriable Retriable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="retriable"></param>
        protected ActionBodyTemplate(Retriable retriable)
        {
            Retriable = retriable;
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <param name="attempts">Total attempts</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        public void Attempt(Action action, ref int attempts, TimeSpan timeBetweenRetries,
            List<Exception> exceptions, TimeSpan time)
        {
            Retriable.OnBeforeRetryInvocation();
            Do(action, ref attempts, timeBetweenRetries, exceptions, time);
            Retriable.OnAfterRetryInvocation();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="attempts"></param>
        /// <param name="timeBetweenRetries"></param>
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        protected abstract void Do(Action action, ref int attempts, TimeSpan timeBetweenRetries,
            List<Exception> exceptions, TimeSpan time);
    }
}
