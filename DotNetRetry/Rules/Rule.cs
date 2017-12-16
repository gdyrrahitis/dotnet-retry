namespace DotNetRetry.Rules
{
    /// <summary>
    /// Defines the retry rules which are available.
    /// </summary>
    public enum Rule
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