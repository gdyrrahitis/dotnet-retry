using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("DotNetRetry.Tests")]
namespace DotNetRetry
{
    using System;
    using System.Collections.Generic;
    using Rules;
    using Strategy.Activators;

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