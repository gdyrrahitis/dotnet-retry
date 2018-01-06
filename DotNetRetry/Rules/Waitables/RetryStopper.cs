namespace DotNetRetry.Rules.Waitables
{
    using System;
    using System.Collections.Generic;
    using Core.Auxiliery;

    /// <summary>
    /// 
    /// </summary>
    internal class RetryStopper: IWaitable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waitTime"></param>
        /// <param name="exceptions"></param>
        public void Wait(TimeSpan waitTime, List<Exception> exceptions) => 
            exceptions.ThrowFlattenAggregateException();
    }
}