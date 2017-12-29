namespace DotNetRetry.Core.Abstractions
{
    using Rules.Cancellation;
    using Rules.Configuration;

    /// <summary>
    /// A base class for retriable.
    /// </summary>
    public abstract partial class Retriable
    {
        /// <summary>
        /// 
        /// </summary>
        internal RuleOptions Options;

        /// <summary>
        /// The cancellation rule object.
        /// </summary>
        internal CancellationRule CancellationRule;

        /// <summary>
        /// The exception rule object.
        /// </summary>
        internal ExceptionRule ExceptionRule;
    }
}
