﻿namespace DotNetRetry.Rules
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
        private readonly IRulesFactory _factory;
        private static RetryRule _instance;
        private static Rule _rule;

        private RetryRule(IRulesFactory factory)
        {
            _factory = factory;
        }

        private static RetryRule Instance => _instance ?? Instantiate();

        private static RetryRule Instantiate()
        {
            var entry = Startup.Configure();
            return _instance = new RetryRule(entry);
        }

        /// <summary>
        /// Builds the retry rules.
        /// </summary>
        /// <param name="rule">Retry rule to enforce.</param>
        /// <returns>A new <see cref="RetryRule"/> instance.</returns>
        public static RetryRule SetupRules(Rule rule)
        {
            _rule = rule;
            return Instantiate();
        }

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
            var retry = _factory.Select(_rule, this);
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
            var retry = _factory.Select(_rule, this);
            return retry.Attempt(() =>
            {
                var result = function();
                OnAfterRetryInvocation();
                return result;
            }, tries, timeBetweenRetries);
        }
    }
}