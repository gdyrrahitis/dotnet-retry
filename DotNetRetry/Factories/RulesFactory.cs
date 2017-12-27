using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Abstractions;
    using Core.Activators;

    /// <summary>
    /// Factory to instantiate retry rules
    /// </summary>
    internal class RulesFactory: IRulesFactory
    {
        private readonly IEnumerable<Type> _rules;
        private readonly IActivatorsFactory _factory;

        /// <summary>
        /// Initializes factory method, providing all registered rules
        /// </summary>
        /// <param name="rules">All current registered rules</param>
        /// <param name="factory">The factory to pick activators.</param>
        internal RulesFactory(IEnumerable<Type> rules, IActivatorsFactory factory)
        {
            _rules = rules;
            _factory = factory;
        }

        /// <summary>
        /// Selects a concrete Strategies using the role hint technique.
        /// </summary>
        /// <param name="strategies">The Strategies to fetch.</param>
        /// <param name="parameter">The parameter for each Strategies.</param>
        /// <returns>An instance of <see cref="IRetry"/> Strategies.</returns>
        public IRetry Select(Rules.Strategies strategies, Retriable parameter) => 
            Select(strategies, new object[] { parameter });

        /// <summary>
        /// Selects a concrete strategy using the role hint technique.
        /// </summary>
        /// <param name="strategies">The strategy to fetch.</param>
        /// <param name="parameters">The parameters for each strategy.</param>
        /// <returns>An instance of <see cref="IRetry"/> strategy.</returns>
        public IRetry Select(Rules.Strategies strategies, params object[] parameters)
        {
            var rule = (from r in _rules
                        let t = r.Name
                        where t == strategies.ToString()
                        select r).FirstOrDefault();
            var type = rule == null ? typeof (NullActivator) : typeof (TypeActivator);
            return _factory.Select(type).Activate<IRetry>(rule, parameters);
        }
    }
}