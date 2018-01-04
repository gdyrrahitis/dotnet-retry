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
    public class Rule : Retriable
    {
        private static Rule _instance;
        private static Strategy _strategy;
        private static IRulesFactory _configuration;
        private readonly IRetry _retry;

        private Rule(IRulesFactory factory)
        {
            _retry = factory.Select(_strategy, this);
            Options = new RuleOptions(this);
        }

        private static Rule Instance => _instance ?? Instantiate();
        private static IRulesFactory Configuration => _configuration ?? (_configuration = Startup.Configure());

        private static Rule Instantiate() => _instance = new Rule(Configuration);

        /// <summary>
        /// Builds the retry rules.
        /// </summary>
        /// <param name="strategy">Retry Strategy to enforce.</param>
        /// <returns>A new <see cref="Rule"/> instance.</returns>
        public static RuleOptions Setup(Strategy strategy)
        {
            _strategy = strategy;
            var rule = Instantiate();
            return rule.Options;
        }

        /// <summary>
        /// Sets up a handler on before retry events.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> for the event.</param>
        /// <returns>The current instance of <see cref="Rule"/>.</returns>
        public override Retriable OnBeforeRetry(EventHandler handler)
        {
            BeforeRetry += handler;
            return Instance;
        }

        /// <summary>
        /// Sets up a handler on after retry events.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> for the event.</param>
        /// <returns>The current instance of <see cref="Rule"/>.</returns>
        public override Retriable OnAfterRetry(EventHandler handler)
        {
            AfterRetry += handler;
            return Instance;
        }

        /// <summary>
        /// Sets up a handler for failure events.
        /// </summary>
        /// <param name="handler">The <see cref="EventHandler"/> for the event.</param>
        /// <returns>The current instance of <see cref="Rule"/>.</returns>
        public override Retriable OnFailure(EventHandler handler)
        {
            Failure += handler;
            return Instance;
        }

        /// <summary>
        /// Sets cancellation rules for current retry policy.
        /// </summary>
        /// <param name="cancellationRules">A builder object to build cancellation rules on.</param>
        /// <returns>The same <see cref="Retriable"/> instance.</returns>
        public override Retriable Cancel(Action<CancellationRule> cancellationRules)
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
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed.</exception>
        public override void Attempt(Action action)
        {
            _retry.Attempt(() =>
            {
                action();
                OnAfterRetryInvocation();
            });
        }

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns.</typeparam>
        /// <param name="function">The function to try execute.</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed.</exception>
        /// <returns>The function return value.</returns>
        public override T Attempt<T>(Func<T> function)
        {
            return _retry.Attempt(() =>
            {
                var result = function();
                OnAfterRetryInvocation();
                return result;
            });
        }
    }
}