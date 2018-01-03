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

    /// <summary>
    /// Initializes a retry <see cref="Rule"/>.
    /// </summary>
    internal static class Startup
    {
        /// <summary>
        /// Configures a <see cref="RulesFactory"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="RulesFactory"/>.</returns>
        internal static IRulesFactory Configure()
        {
            var activatorsFactory = new ActivatorsFactory(Activators);
            return new RulesFactory(Rules, activatorsFactory);
        }

        /// <summary>
        /// Returns all <see cref="Rule"/> types.
        /// </summary>
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

        /// <summary>
        /// Returns all <see cref="IActivator"/> objects.
        /// </summary>
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