using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Factories
{
    using Core;
    using Core.Abstractions;

    /// <summary>
    /// Defines a contract for a rules factory.
    /// </summary>
    internal interface IRulesFactory
    {
        /// <summary>
        /// Selects a concrete Strategy using the role hint technique.
        /// </summary>
        /// <param name="strategy">The Strategy to fetch.</param>
        /// <param name="parameter">The parameter for each Strategy.</param>
        /// <returns>An instance of <see cref="IRetry"/> Strategy.</returns>
        IRetry Select(Rules.Strategy strategy, Retriable parameter);

        /// <summary>
        /// Selects a concrete Strategy using the role hint technique.
        /// </summary>
        /// <param name="strategy">The Strategy to fetch.</param>
        /// <param name="parameters">The parameters for each Strategy.</param>
        /// <returns>An instance of <see cref="IRetry"/> Strategy.</returns>
        IRetry Select(Rules.Strategy strategy, params object[] parameters);
    }
}