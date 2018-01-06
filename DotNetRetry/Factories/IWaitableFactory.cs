namespace DotNetRetry.Factories
{
    using Rules.Waitables;

    /// <summary>
    /// 
    /// </summary>
    internal interface IWaitableFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attempts"></param>
        /// <returns></returns>
        IWaitable Select(int attempts);
    }
}