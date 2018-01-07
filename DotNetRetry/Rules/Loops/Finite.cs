using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules.Loops
{
    using System;
    using System.Collections.Generic;
    using Core.Abstractions;
    using Templates;

    /// <summary>
    /// A finite looper.
    /// </summary>
    internal class Finite: Looper
    {
        /// <summary>
        /// Creates an instance of a finite looper.
        /// </summary>
        /// <param name="actionBody">The policy's action body.</param>
        /// <param name="retriable">The parent <see cref="Retriable"/> instance.</param>
        public Finite(ActionBodyTemplate actionBody, Retriable retriable) : base(actionBody, retriable)
        {
        }

        /// <summary>
        /// Runs a finite loop, up to specified attempts and then breaks.
        /// </summary>
        /// <param name="action">The action to execute.</param>
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