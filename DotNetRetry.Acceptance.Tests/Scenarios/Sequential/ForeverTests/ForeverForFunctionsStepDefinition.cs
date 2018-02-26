namespace DotNetRetry.Acceptance.Tests.Scenarios.Sequential.ForeverTests
{
    using System;
    using TechTalk.SpecFlow;
    using static Xunit.Assert;

    //[Binding, Scope(Tag = "Forever, Functions")]
    public sealed class ForeverFunctionsStepDefinition
    {
        private const string CustomExceptionErrorMessage = "Custom Exception";

        [Given("I have entered a successfull returnable operation"), Scope(Tag = "Forever")]
        public void GivenIHaveEnteredASuccessfullReturnableOperation()
        {
            var count = ScenarioContext.Current.Get<int>("count");
            Func<string> action = () =>
            {
                ScenarioContext.Current.Remove("count");
                ScenarioContext.Current.Add("count", count);
                return "Hello world!";
            };
            ScenarioContext.Current.Add("action", action);
        }

        [Given(@"I have entered a failing returnable operation which succeeds at (\d+) try"), Scope(Tag = "Forever")]
        public void GivenIHaveEnteredAFailingReturnableOperationWhichSucceedsAtTry(int whenToSucceed)
        {
            var count = ScenarioContext.Current.Get<int>("count");
            Func<string> action = () =>
            {
                if (++count == whenToSucceed)
                {
                    return "Hello world!";
                }

                ScenarioContext.Current.Remove("count");
                ScenarioContext.Current.Add("count", count);
                throw new Exception(CustomExceptionErrorMessage);
            };
            ScenarioContext.Current.Add("action", action);
        }

        [Given("I have entered a failing returnable operation"), Scope(Tag = "Forever")]
        public void GivenIHaveEnteredAFailingReturnableOperation()
        {
            var count = ScenarioContext.Current.Get<int>("count");
            Func<string> action = () =>
            {
                count++;
                ScenarioContext.Current.Remove("count");
                ScenarioContext.Current.Add("count", count);
                throw new Exception(CustomExceptionErrorMessage);
            };
            ScenarioContext.Current.Add("action", action);
        }

        [Then("result is (.*)"), Scope(Tag = "Forever")]
        public void ResultIs(string result)
        {
            Equal("Hello world!", result);
        }
    }
}