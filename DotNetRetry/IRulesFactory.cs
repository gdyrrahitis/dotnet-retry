﻿namespace DotNetRetry
{
    using Events;

    /// <summary>
    /// Defines a contract for a rules factory
    /// </summary>
    public interface IRulesFactory
    {
        /// <summary>
        /// Selects a concrete rule using the role hint technique.
        /// </summary>
        /// <param name="rule">The rule to fetch.</param>
        /// <param name="parameter">The parameter for each rule.</param>
        /// <returns>An instance of <see cref="IRetry"/> rule.</returns>
        IRetry Select(Rules.Rules rule, Retriable parameter);

        /// <summary>
        /// Selects a concrete rule using the role hint technique.
        /// </summary>
        /// <param name="rule">The rule to fetch.</param>
        /// <param name="parameters">The parameters for each rule.</param>
        /// <returns>An instance of <see cref="IRetry"/> rule.</returns>
        IRetry Select(Rules.Rules rule, params object[] parameters);
    }
}