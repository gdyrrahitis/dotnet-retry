namespace DotNetRetry.Rules.Configuration
{
    using System;
    using Bytes2you.Validation;
    using Core.Abstractions;

    /// <summary>
    /// Options for <see cref="Rule"/> retry object.
    /// </summary>
    public class RuleOptions
    {
        private readonly Retriable _rule;

        /// <summary>
        /// Initializes a new <see cref="RuleOptions"/> object.
        /// </summary>
        /// <param name="rule">The parent <see cref="Retriable"/>.</param>
        public RuleOptions(Retriable rule)
        {
            _rule = rule;
        }

        /// <summary>
        /// The number of retry attempts.
        /// </summary>
        internal int Attempts { get; private set; }

        /// <summary>
        /// Time specified for rule.
        /// </summary>
        internal TimeSpan Time { get; private set; }

        /// <summary>
        /// Configures the specified <see cref="Rule"/>.
        /// </summary>
        /// <param name="generator">The function to define <see cref="Retriable" /> options.</param>
        /// <returns>The parent <see cref="Retriable"/> instance.</returns>
        public virtual Retriable Config(Action<Options> generator)
        {
            Guard.WhenArgument(generator, nameof(generator))
                .IsNull()
                .Throw();

            var config = new Options();
            generator(config);

            Attempts = config.Attempts;
            Time = config.Time;
            return _rule;
        }
    }
}