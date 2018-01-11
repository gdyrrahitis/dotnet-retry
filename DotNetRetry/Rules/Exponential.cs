using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules
{
    using System;
    using Core.Abstractions;
    using Factories;
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
        internal Exponential(Retriable retriable) : base(retriable, new ActionBody(retriable, new Random(), new WaitableFactory()), 
            new FunctionBody(retriable, new Random(), new WaitableFactory()))
        {
        }
    }
}