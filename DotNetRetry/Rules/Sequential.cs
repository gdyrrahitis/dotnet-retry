using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules
{
    using Core.Abstractions;
    using Factories;
    using Templates.Sequential;

    /// <summary>
    /// A sequential retry technique.
    /// </summary>
    internal class Sequential : Policy
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Sequential"/> object.
        /// </summary>
        /// <param name="retriable">A <see cref="Retriable"/> object with global rules.</param>
        internal Sequential(Retriable retriable) : base(retriable, new ActionBody(retriable, new WaitableFactory(retriable)), 
            new FunctionBody(retriable, new WaitableFactory(retriable)))
        {
        }
    }
}
