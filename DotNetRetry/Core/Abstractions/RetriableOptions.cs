namespace DotNetRetry.Core.Abstractions
{
    using Rules.Configuration;

    /// <summary>
    /// A base class for retriable.
    /// </summary>
    public abstract partial class Retriable
    {
        /// <summary>
        /// The options for specified retriable.
        /// </summary>
        internal RuleOptions Options;
    }
}