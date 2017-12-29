using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.TestProject)]
namespace DotNetRetry.Rules
{
    using System;
    using Core;
    using Core.Abstractions;
    using Templates.Exponential;
    using static Guards;

    /// <summary>
    /// An exponential retry technique.
    /// </summary>
    internal class Exponential : IRetry
    {
        private readonly Retriable _retriable;
        private readonly ActionBody _actionBody;
        private readonly FunctionBody _functionBody;

        /// <summary>
        /// Initializes a new instance of <see cref="Exponential"/> object.
        /// </summary>
        /// <param name="retriable">A <see cref="Retriable"/> object with global rules.</param>
        internal Exponential(Retriable retriable)
        {
            _retriable = retriable;
            _actionBody = new ActionBody(retriable);
            _functionBody = new FunctionBody(retriable);
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        public void Attempt(Action action) => Do(() =>
        {
            action();
            _retriable.OnAfterRetryInvocation();
        });

        /// <summary>
        /// Retries an action and if something happens stores the exceptions to aggregate them.
        /// </summary>
        private void Do(Action action)
        {
            ValidateArguments(_retriable.Options.Attempts, _retriable.Options.Time);
            _actionBody.Attempt(action);
        }

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns</typeparam>
        /// <param name="function">The function to try execute</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed</exception>
        /// <returns>The function return value</returns>
        public T Attempt<T>(Func<T> function) => Do(() =>
        {
            var result = function();
            _retriable.OnAfterRetryInvocation();
            return result;
        });

        private T Do<T>(Func<T> function)
        {
            ValidateArguments(_retriable.Options.Attempts, _retriable.Options.Time);
            return _functionBody.Attempt(function);
        }
    }
}