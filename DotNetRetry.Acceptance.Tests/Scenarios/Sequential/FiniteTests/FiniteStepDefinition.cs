namespace DotNetRetry.Acceptance.Tests.Scenarios.Sequential.FiniteTests
{
    using System;
    using System.Diagnostics;
    using Rules;
    using Rules.Configuration;
    using TechTalk.SpecFlow;

    [Binding, Scope(Tag = "Finite")]
    public sealed class FiniteStepDefinition: SequentialStepDefinitions
    {
        [When("I attempt to run it"), Scope(Tag = "Finite")]
        public void WhenIAttemptToRunIt()
        {
            var stopWatch = new Stopwatch();
            var attempts = ScenarioContext.Current.Get<int>("attempts");
            var time = TimeSpan.FromMilliseconds(ScenarioContext.Current.Get<int>("time"));
            var action = ScenarioContext.Current.Get<Action>("action");
            var before = ScenarioContext.Current.Get<int>("before");
            var after = ScenarioContext.Current.Get<int>("after");
            var failure = ScenarioContext.Current.Get<int>("failure");

            try
            {
                stopWatch.Start();
                Rule.Setup(Strategy.Sequential)
                    .Config(new Options(attempts, time))
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
                Console.WriteLine($"Ticks: {stopWatch.ElapsedTicks}");
            }
        }

        [When("I setup rule configuration"), Scope(Tag = "Finite")]
        public void WhenSetupRuleConfiguration()
        {
            var attempts = ScenarioContext.Current.Get<int>("attempts");
            var time = TimeSpan.FromMilliseconds(ScenarioContext.Current.Get<int>("time"));

            try
            {
                Rule.Setup(Strategy.Sequential)
                    .Config(new Options(attempts, time));
            }
            catch (ArgumentOutOfRangeException exception)
            {
                ScenarioContext.Current.Add("exception", exception);
            }
        }
    }
}
