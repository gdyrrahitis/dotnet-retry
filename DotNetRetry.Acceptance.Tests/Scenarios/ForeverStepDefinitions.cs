namespace DotNetRetry.Acceptance.Tests.Scenarios
{
    using System;
    using System.Diagnostics;
    using Rules;
    using Rules.Configuration;
    using TechTalk.SpecFlow;

    [Binding, Scope(Tag = "Forever")]
    public sealed class ForeverStepDefinitions: GlobalStepDefinitions
    {
        public ForeverStepDefinitions(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [Given("I have entered (.*) milliseconds between"), Scope(Tag = "Forever")]
        public void GivenIHaveEnteredAttemptsForMillisecondsBetween(int time) => InjectedScenarioContext.Add("time", time);

        [When("I attempt to run it"), Scope(Tag = "Forever")]
        public void WhenIAttemptToRunIt()
        {
            var stopWatch = new Stopwatch();
            var time = TimeSpan.FromMilliseconds(InjectedScenarioContext.Get<int>("time"));
            var action = InjectedScenarioContext.Get<Action>("action");
            var before = InjectedScenarioContext.Get<int>("before");
            var after = InjectedScenarioContext.Get<int>("after");
            var failure = InjectedScenarioContext.Get<int>("failure");

            try
            {
                stopWatch.Start();
                Rule.Setup(Strategy.Sequential)
                    .Config(new Options(time))
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

        [When("I attempt to run exponential"), Scope(Tag = "Forever")]
        public void WhenIAttemptToRunExponential()
        {
            var stopWatch = new Stopwatch();
            var time = TimeSpan.FromMilliseconds(InjectedScenarioContext.Get<int>("time"));
            var action = InjectedScenarioContext.Get<Action>("action");
            var before = InjectedScenarioContext.Get<int>("before");
            var after = InjectedScenarioContext.Get<int>("after");
            var failure = InjectedScenarioContext.Get<int>("failure");

            try
            {
                stopWatch.Start();
                Rule.Setup(Strategy.Exponential)
                    .Config(new Options(time))
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

        [When("I setup rule configuration"), Scope(Tag = "Forever")]
        public void WhenSetupRuleConfiguration()
        {
            var time = TimeSpan.FromMilliseconds(InjectedScenarioContext.Get<int>("time"));

            try
            {
                Rule.Setup(Strategy.Sequential)
                    .Config(new Options(time));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                InjectedScenarioContext.Add("exception", exception);
            }
        }

        [When("I setup exponential configuration"), Scope(Tag = "Forever")]
        public void WhenSetupExponentialConfiguration()
        {
            var time = TimeSpan.FromMilliseconds(InjectedScenarioContext.Get<int>("time"));

            try
            {
                Rule.Setup(Strategy.Exponential)
                    .Config(new Options(time));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                InjectedScenarioContext.Add("exception", exception);
            }
        }
    }
}
