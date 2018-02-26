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
    /// A finite looper.
    /// </summary>
    internal class Finite: Looper
    {
        /// <summary>
        /// Creates an instance of a finite looper.
        /// </summary>
        /// <param name="actionBody">The policy's action body.</param>
        /// <param name="functionBody"></param>
        /// <param name="retriable">The parent <see cref="Retriable"/> instance.</param>
        public Finite(ActionBodyTemplate actionBody, FunctionBodyTemplate functionBody, Retriable retriable) : base(actionBody, functionBody, retriable)
        {
        }

        /// <summary>
        /// Runs a finite loop, up to specified attempts and then breaks.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        protected override void Do(Action action)
        {
            var exceptions = new List<Exception>();
            var service = new TimerService();
            var attempts = Retriable.Options.Attempts;

            while (attempts-- > 0)
            {
                var done = ActionBody.Do(action, exceptions, service, attempts);
                if (done)
                {
                    break;
                }
            }
        }

        protected override T Do<T>(Func<T> function)
        {
            T result;
            var exceptions = new List<Exception>();
            var service = new TimerService();
            var attempts = Retriable.Options.Attempts;

            while (attempts-- > 0)
            {
                var done = FunctionBody.Do(function, exceptions, service, attempts, out result);
                if (done)
                {
                    return result;
                }
            }

            exceptions.ThrowFlattenAggregateException();
            return default(T);
        }
    }
}