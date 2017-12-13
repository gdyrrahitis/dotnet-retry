using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DotNetRetry.Tests")]
namespace DotNetRetry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Events;
    using Strategy.Activators;

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
        public RulesFactory(IEnumerable<Type> rules, IActivatorsFactory factory)
        {
            _rules = rules;
            _factory = factory;
        }

        /// <summary>
        /// Selects a concrete rule using the role hint technique.
        /// </summary>
        /// <param name="rule">The rule to fetch.</param>
        /// <param name="parameter">The parameter for each rule.</param>
        /// <returns>An instance of <see cref="IRetry"/> rule.</returns>
        public IRetry Select(Rules.Rules rule, Retriable parameter) => 
            Select(rule, new object[] { parameter });

        /// <summary>
        /// Selects a concrete rule using the role hint technique.
        /// </summary>
        /// <param name="rule">The rule to fetch.</param>
        /// <param name="parameters">The parameters for each rule.</param>
        /// <returns>An instance of <see cref="IRetry"/> rule.</returns>
        public IRetry Select(Rules.Rules rule, params object[] parameters)
        {
            var type = (from r in _rules
                        let t = r.Name
                        where t == rule.ToString()
                        select r).FirstOrDefault();

            return _factory.Select(type).Activate<IRetry>(type, parameters);
        }
    }
}