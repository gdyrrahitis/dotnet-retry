namespace DotNetRetry.Rules.Loops
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;
    using Templates;

    /// <summary>
    /// 
    /// </summary>
    internal class Forever: Looper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionBody"></param>
        /// <param name="retriable"></param>
        public Forever(ActionBodyTemplate actionBody, Retriable retriable) : base(actionBody, retriable)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        protected override void Do(Action action)
        {
            var exceptions = new List<Exception>();
            var time = TimeSpan.Zero;
            var attempts = 0;
            while (true)
            {
                var done = ActionBody.Do(action, exceptions, time, attempts++);
                if (done)
                {
                    break;
                }
            }
        }
    }
}