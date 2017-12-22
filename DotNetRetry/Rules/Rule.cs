namespace DotNetRetry.Rules
{
    using System;
    using Cancellation;
    using Core.Abstractions;
    using Factories;

    /// <summary>
    /// Sets up rules for retry actions.
    /// </summary>
    public class Rule : Retriable, IRule
    {
        private readonly IRulesFactory _factory;
        private static Rule _instance;
        private static Strategies _strategies;
        private static IRulesFactory _configuration;

        private Rule(IRulesFactory factory)
        {
            _factory = factory;
        }

        private static Rule Instance => _instance ?? Instantiate();
        private static IRulesFactory Configuration => _configuration ?? (_configuration = Startup.Configure());

        private static Rule Instantiate() => _instance = new Rule(Configuration);

        /// <summary>
        /// Builds the retry rules.
        /// </summary>
        /// <param name="strategies">Retry Strategies to enforce.</param>
        /// <returns>A new <see cref="Rule"/> instance.</returns>
        public static Rule SetupRules(Strategies strategies)
        {
            _strategies = strategies;
            return Instantiate();
        }

        /// <summary>
        /// Sets up a handler on before retry events.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> for the event.</param>
        /// <returns>The current instance of <see cref="Rule"/>.</returns>
        public IRule OnBeforeRetry(EventHandler handler)
        {
            BeforeRetry += handler;
            return Instance;
        }

        /// <summary>
        /// Sets up a handler on after retry events.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> for the event.</param>
        /// <returns>The current instance of <see cref="Rule"/>.</returns>
        public IRule OnAfterRetry(EventHandler handler)
        {
            AfterRetry += handler;
            return Instance;
        }

        /// <summary>
        /// Sets up a handler for failure events.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> for the event.</param>
        /// <returns>The current instance of <see cref="Rule"/>.</returns>
        public IRule OnFailure(EventHandler handler)
        {
            Failure += handler;
            return Instance;
        }

        /// <summary>
        /// Sets cancellation rules for current retry policy.
        /// </summary>
        /// <param name="cancellationRules">A builder object to build cancellation rules on.</param>
        /// <returns>The same <see cref="IRule"/> instance.</returns>
        public IRule Cancel(Action<CancellationRule> cancellationRules)
        {
            CancellationRule = new CancellationRule();
            ExceptionRule = CancellationRule.ExceptionRule;
            cancellationRules(CancellationRule);
            return Instance;
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <param name="attempts">Total attempts</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        public void Attempt(Action action, int attempts, TimeSpan timeBetweenRetries)
        {
            var retry = _factory.Select(_strategies, this);
            retry.Attempt(() =>
            {
                action();
                OnAfterRetryInvocation();
            }, attempts, timeBetweenRetries);
        }

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="function">The function to try execute</param>
        /// <param name="attempts">Total attempts</param>
        /// <param name="timeBetweenRetries">Time between retries</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="timeBetweenRetries"/> Timespan.Zero or Timespan.MinValue values</exception>
        /// <returns>The function return value</returns>
        public T Attempt<T>(Func<T> function, int attempts, TimeSpan timeBetweenRetries)
        {
            var retry = _factory.Select(_strategies, this);
            return retry.Attempt(() =>
            {
                var result = function();
                OnAfterRetryInvocation();
                return result;
            }, attempts, timeBetweenRetries);
        }
    }
}