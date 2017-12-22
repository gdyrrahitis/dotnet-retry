namespace DotNetRetry.Core.Abstractions
{
    using Rules.Cancellation;

    public abstract partial class Retriable
    {
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
