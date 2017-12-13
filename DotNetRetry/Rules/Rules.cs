namespace DotNetRetry.Rules
{
    /// <summary>
    /// Defines the retry rules which are available.
    /// </summary>
    public enum Rules
    {
        /// <summary>
        /// Defines a sequential retry technique.
        /// </summary>
        Sequential,

        /// <summary>
        /// Defines an exponential backoff technique.
        /// </summary>
        Exponential
    }
}