﻿using System.Runtime.CompilerServices;
using DotNetRetry.Core.Auxiliery;

[assembly: InternalsVisibleTo(Constants.UnitTestProject)]
[assembly: InternalsVisibleTo(Constants.IntegrationTestProject)]
[assembly: InternalsVisibleTo(Constants.CommonTestProject)]
namespace DotNetRetry.Rules
{
    using System;
    using Core;
    using Core.Abstractions;
    using Loops;
    using Templates;
    using static Guards;

    /// <summary>
    /// Base implementation for each Rule
    /// </summary>
    internal abstract class Policy : IRetry
    {
        private readonly Retriable _retriable;
        private readonly ActionBodyTemplate _actionBody;
        private readonly FunctionBodyTemplate _functionBody;

        /// <summary>
        /// Initializes a new <see cref="Policy"/> object.
        /// </summary>
        /// <param name="retriable">A <see cref="Retriable"/> object with global rules.</param>
        /// <param name="actionBody">The action body related to policy.</param>
        /// <param name="functionBody">The function body related to policy.</param>
        internal Policy(Retriable retriable, ActionBodyTemplate actionBody, FunctionBodyTemplate functionBody)
        {
            _retriable = retriable;
            _actionBody = actionBody;
            _functionBody = functionBody;
        }

        /// <summary>
        /// Attempts to retry an action.
        /// </summary>
        /// <param name="action">The action to try execute.</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed.</exception>
        public virtual void Attempt(Action action) => Do(action);

        /// <summary>
        /// Retries an action and if something happens stores the exceptions to aggregate them.
        /// </summary>
        private void Do(Action action)
        {
            var looper = GetLooper();
            looper.Attempt(action);
        }

        private Looper GetLooper()
        {
            var looper = Selector.Pick(_retriable, _actionBody, _functionBody);
            return looper;
        }

        /// <summary>
        /// Attempts to retry an a method that returns a result.
        /// </summary>
        /// <typeparam name="T">The type of the return value the action returns.</typeparam>
        /// <param name="function">The function to try execute.</param>
        /// <exception cref="AggregateException">All exceptions logged from action(s) executed.</exception>
        /// <returns>The function return value</returns>
        public virtual T Attempt<T>(Func<T> function) =>
            Do(() =>
            {
                var result = function();
                return result;
            });

        /// <summary>
        /// Retries an action and if something happens stores the exceptions to aggregate them.
        /// </summary>
        private T Do<T>(Func<T> function)
        {
            var looper = GetLooper();
            return looper.Attempt(function);
        }
    }
}