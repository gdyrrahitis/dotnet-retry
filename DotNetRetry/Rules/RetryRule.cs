namespace DotNetRetry.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bytes2you.Validation;
    using Events;

    /// <summary>
    /// Sets up rules for retry actions.
    /// </summary>
    public class RetryRule : Retriable, IRules
    {
        private static RetryRule _instance;

        private RetryRule()
        {
        }

        private static RetryRule Instance => _instance ?? (_instance = new RetryRule());

        /// <summary>
        /// Builds the retry rules.
        /// </summary>
        /// <returns>A new <see cref="RetryRule"/> instance.</returns>
        public static RetryRule SetupRules() => _instance = new RetryRule();

        /// <summary>
        /// Sets up a handler on before retry events.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> for the event.</param>
        /// <returns>The current instance of <see cref="RetryRule"/>.</returns>
        public IRules OnBeforeRetry(EventHandler handler)
        {
            BeforeRetry += handler;
            return Instance;
        }

        /// <summary>
        /// Sets up a handler on after retry events.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> for the event.</param>
        /// <returns>The current instance of <see cref="RetryRule"/>.</returns>
        public IRules OnAfterRetry(EventHandler handler)
        {
            AfterRetry += handler;
            return Instance;
        }

        /// <summary>
        /// Sets up a handler for failure events.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> for the event.</param>
        /// <returns>The current instance of <see cref="RetryRule"/>.</returns>
        public IRules OnFailure(EventHandler handler)
        {
            Failure += handler;
            return Instance;
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <param name="tries">Total tries</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="tries"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        public void Attempt(Action action, int tries, TimeSpan timeBetweenRetries)
        {
            var retry = new Retry(this);
            retry.Attempt(() =>
            {
                action();
                OnAfterRetryInvocation();
            }, tries, timeBetweenRetries);
        }

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="function">The function to try execute</param>
        /// <param name="tries">Total tries</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="tries"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        /// <returns>The function return value</returns>
        public T Attempt<T>(Func<T> function, int tries, TimeSpan timeBetweenRetries)
        {
            var retry = new Retry(this);
            return retry.Attempt(() =>
            {
                var result = function();
                OnAfterRetryInvocation();
                return result;
            }, tries, timeBetweenRetries);
        }
    }
}