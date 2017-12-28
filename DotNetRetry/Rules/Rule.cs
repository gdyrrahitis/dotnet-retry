namespace DotNetRetry.Rules
{
    using System;
    using Cancellation;
    using Configuration;
    using Core;
    using Core.Abstractions;
    using Factories;

    /// <summary>
    /// Sets up rules for retry actions.
    /// </summary>
    public class Rule : Retriable, IRule
    {
        private static Rule _instance;
        private static Strategies _strategies;
        private static IRulesFactory _configuration;
        private readonly IRetry _retry;

        private Rule(IRulesFactory factory)
        {
            _retry = factory.Select(_strategies, this);
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
        /// <param name="action">The action to try execute.</param>
        /// <param name="attempts">Total attempts.</param>
        /// <param name="time">Time between retries.</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed.</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1.</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="time"/> Timespan.Zero or Timespan.MinValue values.</exception>
        public void Attempt(Action action, int attempts, TimeSpan time)
        {
            _retry.Attempt(() =>
            {
                action();
                OnAfterRetryInvocation();
            }, attempts, time);
        }

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns.</typeparam>
        /// <param name="function">The function to try execute.</param>
        /// <param name="attempts">Total attempts.</param>
        /// <param name="time">Time between retries.</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed.</exception>
        /// <exception cref="ArgumentOutOfRangeException">For parameter <paramref name="attempts"/> being less than 1.</exception>
        /// <exception cref="ArgumentException">For parameter <paramref name="time"/> Timespan.Zero or Timespan.MinValue values/</exception>
        /// <returns>The function return value.</returns>
        public T Attempt<T>(Func<T> function, int attempts, TimeSpan time)
        {
            return _retry.Attempt(() =>
            {
                var result = function();
                OnAfterRetryInvocation();
                return result;
            }, attempts, time);
        }
    }
}