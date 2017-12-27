using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly:InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Core;
    using Core.Activators;
    using Factories;
    using Rules;

    internal static class Startup
    {
        internal static IRulesFactory Configure()
        {
            var activatorsFactory = new ActivatorsFactory(Activators);
            return new RulesFactory(Rules, activatorsFactory);
        }

        internal static IEnumerable<Type> Rules
        {
            get
            {
                var type = typeof (IRetry);
                var assembly = Assembly.GetAssembly(typeof(Startup));
                var rules = assembly.GetTypes().Where(t => type.IsAssignableFrom(t) &&
                    !t.IsInterface &&
                    t != typeof(Rule));
                return rules;
            }
        }

        internal static IEnumerable<IActivator> Activators
        {
            get
            {
                var activators = new List<IActivator>
                {
                    new NullActivator(),
                    new TypeActivator()
                };
                return activators;
            }
        }
    }
}