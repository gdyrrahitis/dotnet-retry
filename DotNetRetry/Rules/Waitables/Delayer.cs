namespace DotNetRetry.Rules.Waitables
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    internal class Delayer: IWaitable
    {
        public void Wait(TimeSpan waitTime, List<Exception> exceptions) => 
            Task.Delay(waitTime).Wait();
    }
}