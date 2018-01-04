using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules
{
    using Core.Abstractions;
    using Templates.Exponential;

    /// <summary>
    /// An exponential retry technique.
    /// </summary>
    internal class Exponential : Policy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Exponential"/> object.
        /// </summary>
        /// <param name="retriable">A <see cref="Retriable"/> object with global rules.</param>
        internal Exponential(Retriable retriable) : base(retriable, new ActionBody(retriable), new FunctionBody(retriable))
        {
        }
    }
}