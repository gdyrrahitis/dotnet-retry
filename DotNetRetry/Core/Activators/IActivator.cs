namespace DotNetRetry.Core.Activators
{
    using System;

    /// <summary>
    /// Activates a rule type.
    /// </summary>
    public interface IActivator
    {
        /// <summary>
        /// Activates a rule type to the generic T provided.
        /// </summary>
        /// <typeparam name="T">The type to cast to.</typeparam>
        /// <param name="type">The raw rule type.</param>
        /// <param name="parameters">The optional constructor parameters to pass.</param>
        /// <returns>The type casted as T.</returns>
        T Activate<T>(Type type, params object[] parameters);
    }
}