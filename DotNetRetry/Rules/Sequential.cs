using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules
{
    using Core.Abstractions;
    using Factories;
    using Templates;
    using Templates.Sequential;
    using Waitables;

    /// <summary>
    /// A sequential retry technique.
    /// </summary>
    internal class Sequential : Policy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Sequential"/> object.
        /// </summary>
        /// <param name="retriable">A <see cref="Retriable"/> object with global rules.</param>
        internal Sequential(Retriable retriable) : base(retriable, new ActionBody(retriable, new WaitableFactory()), 
            new FunctionBody(retriable))
        {
        }
    }
}
