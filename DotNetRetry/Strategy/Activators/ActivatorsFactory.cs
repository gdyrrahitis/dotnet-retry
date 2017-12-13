namespace DotNetRetry.Strategy.Activators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Factory class to create new activators.
    /// </summary>
    public class ActivatorsFactory: IActivatorsFactory
    {
        private readonly IEnumerable<IActivator> _activators;

        /// <summary>
        /// Initializes a new instance of <see cref="ActivatorsFactory"/>.
        /// </summary>
        /// <param name="activators">The list of available <see cref="IActivator"/>.</param>
        public ActivatorsFactory(IEnumerable<IActivator> activators)
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
                GetActivatorThatStartsWith("Type");
        }

        private IActivator GetActivatorThatStartsWith(string name) => 
            _activators.First(a => a.GetType().Name.StartsWith(name));
    }
}