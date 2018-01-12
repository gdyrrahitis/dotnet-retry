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
    using Core.Time;
    using Templates;

    /// <summary>
    /// An infinite looper
    /// </summary>
    internal class Forever : Looper
    {
        /// <summary>
        /// Creates an instance of a finite looper.
        /// </summary>
        /// <param name="actionBody">The policy's action body.</param>
        /// <param name="functionBody"></param>
        /// <param name="retriable">The parent <see cref="Retriable"/> instance.</param>
        public Forever(ActionBodyTemplate actionBody, FunctionBodyTemplate functionBody, Retriable retriable) : base(actionBody, functionBody, retriable)
        {
        }

        /// <summary>
        /// Runs an infinite loop, which only breaks if a cancellation policy is set.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        protected override void Do(Action action)
        {
            var exceptions = new List<Exception>();
            var service = new TimerService();
            var attempts = 0;

            while (true)
            {
                var done = ActionBody.Do(action, exceptions, service, attempts++);
                if (done)
                {
                    break;
                }
            }
        }

        protected override T Do<T>(Func<T> function)
        {
            var exceptions = new List<Exception>();
            var service = new TimerService();
            var attempts = 0;
            T result;

            while (true)
            {
                var done = FunctionBody.Do(function, exceptions, service, attempts++, out result);
                if (done)
                {
                    return result;
                }
            }
        }
    }
}