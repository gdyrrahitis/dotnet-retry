using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly:InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry
{
    using System;
    using System.Collections.Generic;
    using Core.Activators;
    using Factories;
    using Rules;

    internal static class Startup
    {
        internal static IRulesFactory Configure()
        {
            var activators = new List<IActivator>
            {
                new NullActivator(),
                new TypeActivator()
            };

            var activatorsFactory = new ActivatorsFactory(activators);

            var rules = new List<Type>
            {
                typeof(Sequential),
                typeof(Exponential)
            };

            return new RulesFactory(rules, activatorsFactory);
        }
    }
}