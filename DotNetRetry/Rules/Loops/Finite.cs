namespace DotNetRetry.Rules.Loops
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;
    using Templates;

    /// <summary>
    /// 
    /// </summary>
    internal class Finite: Looper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionBody"></param>
        /// <param name="retriable"></param>
        public Finite(ActionBodyTemplate actionBody, Retriable retriable) : base(actionBody, retriable)
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
            var attempts = Retriable.Options.Attempts;

            while (attempts-- > 0)
            {
                var done = ActionBody.Do(action, exceptions, time, attempts);
                if (done)
                {
                    break;
                }
            }
        }
    }
}