namespace DotNetRetry.Core.Activators
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Activates the given specified type.
    /// </summary>
    internal class TypeActivator: IActivator
    {
        /// <summary>
        /// Returns a new instance of T for the specified type.
        /// </summary>
        /// <typeparam name="T">The type to cast to.</typeparam>
        /// <param name="type">The target type.</param>
        /// <param name="parameters">The optional constructor parameters to pass.</param>
        /// <returns>A new instace of T.</returns>
        /// /// <exception cref="ArgumentNullException">Throws this exception if type is null.</exception>
        public T Activate<T>(Type type, params object[] parameters) => 
            (T)Activator.CreateInstance(type, BindingFlags.NonPublic | BindingFlags.Instance, null, parameters, null);
    }
}