using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules.Loops
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;
    using Templates;

    /// <summary>
    /// An infinite looper
    /// </summary>
    internal class Forever: Looper
    {
        /// <summary>
        /// Creates an instance of a finite looper.
        /// </summary>
        /// <param name="actionBody">The policy's action body.</param>
        /// <param name="retriable">The parent <see cref="Retriable"/> instance.</param>
        public Forever(ActionBodyTemplate actionBody, Retriable retriable) : base(actionBody, retriable)
        {
        }

        /// <summary>
        /// Runs an infinite loop, which only breaks if a cancellation policy is set.
        /// </summary>
        /// <param name="action">The action to execute.</param>
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