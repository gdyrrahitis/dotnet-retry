namespace DotNetRetry.Rules.Waitables
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    internal interface IWaitable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waitTime"></param>
        /// <param name="exceptions"></param>
        void Wait(TimeSpan waitTime, List<Exception> exceptions);
    }
}