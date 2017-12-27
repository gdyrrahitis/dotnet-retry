using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules.Templates
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;

    /// <summary>
    /// Template class for non-returnable functions.
    /// </summary>
    internal abstract class ActionBodyTemplate
    {
        /// <summary>
        /// The rules object.
        /// </summary>
        protected readonly Retriable Retriable;

        /// <summary>
        /// Initializes a new instance of <see cref="ActionBodyTemplate"/>.
        /// </summary>
        /// <param name="retriable">The <see cref="Retriable"/> rules object.</param>
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
            BeforeRetry();
            Do(action, ref attempts, timeBetweenRetries, exceptions, time);
            AfterRetry();
        }

        /// <summary>
        /// Hook to execute before retry policy execution.
        /// </summary>
        protected virtual void BeforeRetry() => Retriable.OnBeforeRetryInvocation();

        /// <summary>
        /// Hook to execute after retry policy execution.
        /// </summary>
        protected virtual void AfterRetry() => Retriable.OnAfterRetryInvocation();

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The function to try execute</param>
        /// <param name="attempts">Total attempts</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <param name="exceptions"></param>
        /// <param name="time"></param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        protected abstract void Do(Action action, ref int attempts, TimeSpan timeBetweenRetries,
            List<Exception> exceptions, TimeSpan time);
    }
}
