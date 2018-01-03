namespace DotNetRetry.Rules.Configuration
{
    using System;
    using Bytes2you.Validation;

    /// <summary>
    /// Options for <see cref="Rule"/> retry object.
    /// </summary>
    public class RuleOptions
    {
        private readonly Rule _rule;

        /// <summary>
        /// Initializes a new <see cref="RuleOptions"/> object.
        /// </summary>
        /// <param name="rule">The parent <see cref="Rule"/>.</param>
        public RuleOptions(Rule rule)
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
        /// <param name="config">The <see cref="Options"/> for that <see cref="Rule"/>.</param>
        /// <returns>The parent <see cref="Rule"/> instance.</returns>
        public virtual Rule Config(Options config)
        {
            Guard.WhenArgument(config, nameof(config))
                .IsNull()
                .Throw();

            Attempts = config.Attempts;
            Time = config.Time;
            return _rule;
        }
    }
}