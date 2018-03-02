namespace DotNetRetry.Core.Exceptions
{
    using System;

    /// <summary>
    /// Exception when activator cannot be found.
    /// </summary>
    public class ActivatorNotFoundException: Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="ActivatorNotFoundException"/>.
        /// </summary>
        public ActivatorNotFoundException()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="ActivatorNotFoundException"/>.
        /// </summary>
        public ActivatorNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="ActivatorNotFoundException"/>.
        /// </summary>
        public ActivatorNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}