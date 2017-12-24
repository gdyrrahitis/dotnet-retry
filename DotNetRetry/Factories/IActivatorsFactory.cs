namespace DotNetRetry.Factories
{
    using System;
    using Core.Activators;

    /// <summary>
    /// Defined contract for Activators Factory.
    /// </summary>
    internal interface IActivatorsFactory
    {
        /// <summary>
        /// Selects an activator based on given type.
        /// </summary>
        /// <param name="type">The given type.</param>
        /// <returns>A new <see cref="IActivator"/> instance.</returns>
        IActivator Select(Type type);
    }
}