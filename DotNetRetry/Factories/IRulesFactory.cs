using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Factories
{
    using Core;
    using Core.Abstractions;

    /// <summary>
    /// Defines a contract for a rules factory
    /// </summary>
    internal interface IRulesFactory
    {
        /// <summary>
        /// Selects a concrete Strategies using the role hint technique.
        /// </summary>
        /// <param name="strategies">The Strategies to fetch.</param>
        /// <param name="parameter">The parameter for each Strategies.</param>
        /// <returns>An instance of <see cref="IRetry"/> Strategies.</returns>
        IRetry Select(Rules.Strategies strategies, Retriable parameter);

        /// <summary>
        /// Selects a concrete Strategies using the role hint technique.
        /// </summary>
        /// <param name="strategies">The Strategies to fetch.</param>
        /// <param name="parameters">The parameters for each Strategies.</param>
        /// <returns>An instance of <see cref="IRetry"/> Strategies.</returns>
        IRetry Select(Rules.Strategies strategies, params object[] parameters);
    }
}