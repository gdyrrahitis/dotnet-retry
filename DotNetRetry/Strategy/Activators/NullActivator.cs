namespace DotNetRetry.Strategy.Activators
{
    using System;
    using Exceptions;

    /// <summary>
    /// Handles null types.
    /// </summary>
    public class NullActivator: IActivator
    {
        private const string CouldNotFindRuleErrorMessage = "Could not find rule.";

        /// <summary>
        /// Generates a <see cref="RuleNotFoundException"/> for null types.
        /// </summary>
        /// <param name="type">The type to activate</param>
        /// <param name="parameters">The optional constructor parameters to pass.</param>
        /// <exception cref="RuleNotFoundException">Throws this exception because type is null.</exception>
        /// <exception cref="ArgumentException">Throws this exception because type is not null, so invalid.</exception>
        public T Activate<T>(Type type, params object[] parameters)
        {
            if (type != null)
            {
                throw new ArgumentException($"Type provided is not null, invalid for {nameof(NullActivator)} instance.", 
                    nameof(type));
            }

            throw new RuleNotFoundException(CouldNotFindRuleErrorMessage);
        }
    }
}