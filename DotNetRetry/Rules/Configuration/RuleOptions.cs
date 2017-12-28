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
        /// 
        /// </summary>
        /// <param name="rule"></param>
        public RuleOptions(Rule rule)
        {
            _rule = rule;
        }

        /// <summary>
        /// 
        /// </summary>
        internal int Attempts { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        internal TimeSpan Time { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
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