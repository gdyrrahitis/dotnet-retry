namespace DotNetRetry.Acceptance.Tests.Scenarios.Sequential.FiniteTests
{
    using System;
    using Rules;
    using Rules.Configuration;
    using TechTalk.SpecFlow;
    using static Xunit.Assert;

    [Binding]
    public sealed class FiniteStepDefinition
    {
        [Given("I have entered a successfull non-returnable operation")]
        public void GivenIHaveEnteredASuccessfullNonReturnableOperation()
        {
            //TODO: implement arrange (precondition) logic
            // For storing and retrieving scenario-specific data see http://go.specflow.org/doc-sharingdata 
            // To use the multiline text or the table argument of the scenario,
            // additional string/Table parameters can be defined on the step definition
            // method. 
            Action action = () => { };
            ScenarioContext.Current.Add("action", action);
        }

        [Given("I have entered (.*) attempts for (.*) milliseconds between")]
        public void GivenIHaveEnteredAttemptsForMillisecondsBetween(int attempts, int time)
        {
            ScenarioContext.Current.Add("attempts", attempts);
            ScenarioContext.Current.Add("time", time);
        }

        [When("I attempt to run it")]
        public void WhenIAttemptToRunIt()
        {
            var attempts = ScenarioContext.Current.Get<int>("attempts");
            var time = TimeSpan.FromMilliseconds(ScenarioContext.Current.Get<int>("time"));
            var action = ScenarioContext.Current.Get<Action>("action");
            Rule.Setup(Strategy.Sequential)
                .Config(new Options(attempts, time))
                .OnBeforeRetry((sender, args) => ScenarioContext.Current.Add("before", true))
                .OnAfterRetry((sender, args) => ScenarioContext.Current.Add("after", true))
                .OnFailure((sender, args) => ScenarioContext.Current.Add("failure", true))
                .Attempt(action);
        }

        [Then("no retry attempts should be made")]
        public void ThenNoRetryAttemptsShouldBeMade()
        {
            bool before;
            bool after;
            bool failure;
            if (ScenarioContext.Current.TryGetValue("before", out before))
            {
                False(true);
            }

            if (ScenarioContext.Current.TryGetValue("after", out after))
            {
                False(true);
            }

            if (ScenarioContext.Current.TryGetValue("after", out failure))
            {
                False(true);
            }
        }
    }
}
