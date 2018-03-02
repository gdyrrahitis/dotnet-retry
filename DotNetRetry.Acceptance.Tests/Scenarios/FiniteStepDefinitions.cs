namespace DotNetRetry.Acceptance.Tests.Scenarios
{
    using System;
    using System.Diagnostics;
    using Rules;
    using Rules.Configuration;
    using TechTalk.SpecFlow;

    [Binding, Scope(Tag = "Finite")]
    public sealed class FiniteStepDefinitions: GlobalStepDefinitions
    {
        public FiniteStepDefinitions(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [When("I attempt to run it"), Scope(Tag = "Finite")]
        public void WhenIAttemptToRunIt()
        {
            var stopWatch = new Stopwatch();
            var attempts = InjectedScenarioContext.Get<int>("attempts");
            var time = TimeSpan.FromMilliseconds(InjectedScenarioContext.Get<int>("time"));
            var action = InjectedScenarioContext.Get<Action>("action");
            var before = InjectedScenarioContext.Get<int>("before");
            var after = InjectedScenarioContext.Get<int>("after");
            var failure = InjectedScenarioContext.Get<int>("failure");

            try
            {
                stopWatch.Start();
                Rule.Setup(Strategy.Sequential)
                    .Config(new Options(attempts, time))
                    .OnBeforeRetry((sender, args) =>
                    {
                        before++;
                        InjectedScenarioContext.Remove("before");
                        InjectedScenarioContext.Add("before", before);
                    })
                    .OnAfterRetry((sender, args) =>
                    {
                        after++;
                        InjectedScenarioContext.Remove("after");
                        InjectedScenarioContext.Add("after", after);
                    })
                    .OnFailure((sender, args) =>
                    {
                        failure++;
                        InjectedScenarioContext.Remove("failure");
                        InjectedScenarioContext.Add("failure", failure);
                    })
                    .Attempt(action);
            }
            catch (AggregateException exception)
            {
                InjectedScenarioContext.Add("aggregateException", exception);
            }
            finally
            {
                stopWatch.Stop();
                InjectedScenarioContext.Remove("time");
                InjectedScenarioContext.Add("time", stopWatch.ElapsedMilliseconds);
            }
        }

        [When("I attempt to run exponential"), Scope(Tag = "Finite")]
        public void WhenIAttemptToRunExponential()
        {
            var stopWatch = new Stopwatch();
            var attempts = InjectedScenarioContext.Get<int>("attempts");
            var time = TimeSpan.FromMilliseconds(InjectedScenarioContext.Get<int>("time"));
            var action = InjectedScenarioContext.Get<Action>("action");
            var before = InjectedScenarioContext.Get<int>("before");
            var after = InjectedScenarioContext.Get<int>("after");
            var failure = InjectedScenarioContext.Get<int>("failure");

            try
            {
                stopWatch.Start();
                Rule.Setup(Strategy.Exponential)
                    .Config(new Options(attempts, time))
                    .OnBeforeRetry((sender, args) =>
                    {
                        before++;
                        InjectedScenarioContext.Remove("before");
                        InjectedScenarioContext.Add("before", before);
                    })
                    .OnAfterRetry((sender, args) =>
                    {
                        after++;
                        InjectedScenarioContext.Remove("after");
                        InjectedScenarioContext.Add("after", after);
                    })
                    .OnFailure((sender, args) =>
                    {
                        failure++;
                        InjectedScenarioContext.Remove("failure");
                        InjectedScenarioContext.Add("failure", failure);
                    })
                    .Attempt(action);
            }
            catch (AggregateException exception)
            {
                InjectedScenarioContext.Add("aggregateException", exception);
            }
            finally
            {
                stopWatch.Stop();
                InjectedScenarioContext.Remove("time");
                InjectedScenarioContext.Add("time", stopWatch.ElapsedMilliseconds);
            }
        }

        [When("I setup rule configuration"), Scope(Tag = "Finite")]
        public void WhenSetupRuleConfiguration()
        {
            var attempts = InjectedScenarioContext.Get<int>("attempts");
            var time = TimeSpan.FromMilliseconds(InjectedScenarioContext.Get<int>("time"));

            try
            {
                Rule.Setup(Strategy.Sequential)
                    .Config(new Options(attempts, time));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                InjectedScenarioContext.Add("exception", exception);
            }
        }

        [When("I setup exponential rule configuration"), Scope(Tag = "Finite")]
        public void WhenSetupExponentialRuleConfiguration()
        {
            var attempts = InjectedScenarioContext.Get<int>("attempts");
            var time = TimeSpan.FromMilliseconds(InjectedScenarioContext.Get<int>("time"));

            try
            {
                Rule.Setup(Strategy.Exponential)
                    .Config(new Options(attempts, time));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                InjectedScenarioContext.Add("exception", exception);
            }
        }
    }
}
