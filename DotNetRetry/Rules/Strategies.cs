namespace DotNetRetry.Rules
{
    /// <summary>
    /// Defines the retry strategies which are available.
    /// </summary>
    public enum Strategies
    {
        /// <summary>
        /// Defines a sequential retry strategy.
        /// </summary>
        Sequential,

        /// <summary>
        /// Defines an exponential backoff strategy.
        /// </summary>
        Exponential
    }
}