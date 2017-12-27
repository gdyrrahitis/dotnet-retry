using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Activators;
    using Core.Exceptions;

    /// <summary>
    /// Factory class to create new activators.
    /// </summary>
    internal class ActivatorsFactory: IActivatorsFactory
    {
        private readonly IEnumerable<IActivator> _activators;

        /// <summary>
        /// Initializes a new instance of <see cref="ActivatorsFactory"/>.
        /// </summary>
        /// <param name="activators">The list of available <see cref="IActivator"/>.</param>
        internal ActivatorsFactory(IEnumerable<IActivator> activators)
        {
            _activators = activators;
        }

        /// <summary>
        /// Selects an activator based on given type.
        /// </summary>
        /// <param name="type">The given type.</param>
        /// <returns>A new <see cref="IActivator"/> instance.</returns>
        public IActivator Select(Type type)
        {
            return type == null ? GetActivatorThatStartsWith("Null") :
                GetActivatorThatStartsWith(type.Name);
        }

        private IActivator GetActivatorThatStartsWith(string name)
        {
            try
            {
                return _activators.First(a => a.GetType().Name.StartsWith(name));
            }
            catch (InvalidOperationException ex)
            {
                throw new ActivatorNotFoundException(Constants.ActivatorNotFoundErrorMessage, ex.InnerException);
            }
        }
    }
}