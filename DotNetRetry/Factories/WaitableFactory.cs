namespace DotNetRetry.Factories
{
    using System.Collections.Generic;
    using Rules.Waitables;

    /// <summary>
    /// 
    /// </summary>
    internal class WaitableFactory : IWaitableFactory
    {
        private static readonly IDictionary<bool, IWaitable> Waitables = new Dictionary<bool, IWaitable>
        {
            { true, new Delayer() },
            { false, new RetryStopper() }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attempts"></param>
        /// <returns></returns>
        public IWaitable Select(int attempts) => Waitables[attempts > 0];
    }
}