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
        [Given("I have entered (.*) milliseconds between"), Scope(Tag = "Forever")]
        public void GivenIHaveEnteredAttemptsForMillisecondsBetween(int time) => ScenarioContext.Current.Add("time", time);

        [When("I attempt to run it"), Scope(Tag = "Forever")]
        public void WhenIAttemptToRunIt()
        {
            var stopWatch = new Stopwatch();
            var time = TimeSpan.FromMilliseconds(ScenarioContext.Current.Get<int>("time"));
            var action = ScenarioContext.Current.Get<Action>("action");
            var before = ScenarioContext.Current.Get<int>("before");
            var after = ScenarioContext.Current.Get<int>("after");
            var failure = ScenarioContext.Current.Get<int>("failure");

            try
            {
                stopWatch.Start();
                Rule.Setup(Strategy.Sequential)
                    .Config(new Options(time))
                    .OnBeforeRetry((sender, args) =>
                    {
                        before++;
                        ScenarioContext.Current.Remove("before");
                        ScenarioContext.Current.Add("before", before);
                    })
                    .OnAfterRetry((sender, args) =>
                    {
                        after++;
                        ScenarioContext.Current.Remove("after");
                        ScenarioContext.Current.Add("after", after);
                    })
                    .OnFailure((sender, args) =>
                    {
                        failure++;
                        ScenarioContext.Current.Remove("failure");
                        ScenarioContext.Current.Add("failure", failure);
                    })
                    .Attempt(action);
            }
            catch (AggregateException exception)
            {
                ScenarioContext.Current.Add("aggregateException", exception);
            }
            finally
            {
                stopWatch.Stop();
                ScenarioContext.Current.Remove("time");
                ScenarioContext.Current.Add("time", stopWatch.ElapsedMilliseconds);
            }
        }

        [When("I attempt to run exponential"), Scope(Tag = "Forever")]
        public void WhenIAttemptToRunExponential()
        {
            var stopWatch = new Stopwatch();
            var time = TimeSpan.FromMilliseconds(ScenarioContext.Current.Get<int>("time"));
            var action = ScenarioContext.Current.Get<Action>("action");
            var before = ScenarioContext.Current.Get<int>("before");
            var after = ScenarioContext.Current.Get<int>("after");
            var failure = ScenarioContext.Current.Get<int>("failure");

            try
            {
                stopWatch.Start();
                Rule.Setup(Strategy.Exponential)
                    .Config(new Options(time))
                    .OnBeforeRetry((sender, args) =>
                    {
                        before++;
                        ScenarioContext.Current.Remove("before");
                        ScenarioContext.Current.Add("before", before);
                    })
                    .OnAfterRetry((sender, args) =>
                    {
                        after++;
                        ScenarioContext.Current.Remove("after");
                        ScenarioContext.Current.Add("after", after);
                    })
                    .OnFailure((sender, args) =>
                    {
                        failure++;
                        ScenarioContext.Current.Remove("failure");
                        ScenarioContext.Current.Add("failure", failure);
                    })
                    .Attempt(action);
            }
            catch (AggregateException exception)
            {
                ScenarioContext.Current.Add("aggregateException", exception);
            }
            finally
            {
                stopWatch.Stop();
                ScenarioContext.Current.Remove("time");
                ScenarioContext.Current.Add("time", stopWatch.ElapsedMilliseconds);
            }
        }

        [When("I setup rule configuration"), Scope(Tag = "Forever")]
        public void WhenSetupRuleConfiguration()
        {
            var time = TimeSpan.FromMilliseconds(ScenarioContext.Current.Get<int>("time"));

            try
            {
                Rule.Setup(Strategy.Sequential)
                    .Config(new Options(time));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                ScenarioContext.Current.Add("exception", exception);
            }
        }

        [When("I setup exponential configuration"), Scope(Tag = "Forever")]
        public void WhenSetupExponentialConfiguration()
        {
            var time = TimeSpan.FromMilliseconds(ScenarioContext.Current.Get<int>("time"));

            try
            {
                Rule.Setup(Strategy.Exponential)
                    .Config(new Options(time));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                ScenarioContext.Current.Add("exception", exception);
            }
        }
    }
}
