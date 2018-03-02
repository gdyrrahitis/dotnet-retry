namespace DotNetRetry.Core.Exceptions
{
    using System;

    /// <summary>
    /// Exception when rule cannot be found.
    /// </summary>
    public class RuleNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="RuleNotFoundException"/>.
        /// </summary>
        public RuleNotFoundException()
        { 
        }

        /// <summary>
        /// Creates a new instance of <see cref="RuleNotFoundException"/>.
        /// </summary>
        public RuleNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="RuleNotFoundException"/>.
        /// </summary>
        public RuleNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}