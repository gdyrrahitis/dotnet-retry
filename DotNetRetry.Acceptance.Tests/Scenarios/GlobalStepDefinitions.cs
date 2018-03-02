namespace DotNetRetry.Acceptance.Tests.Scenarios
{
    using System;
    using TechTalk.SpecFlow;
    using Xunit;

    public class GlobalStepDefinitions
    {
        protected const string CustomExceptionErrorMessage = "Custom Exception";
        protected readonly ScenarioContext InjectedScenarioContext;

        public GlobalStepDefinitions(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException(nameof(scenarioContext));
            }

            InjectedScenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void SetupExecutionCount()
        {
            InjectedScenarioContext.Add("count", 0);
            InjectedScenarioContext.Add("before", 0);
            InjectedScenarioContext.Add("after", 0);
            InjectedScenarioContext.Add("failure", 0);
        }

        [Given("I have entered a successfull non-returnable operation")]
        public void GivenIHaveEnteredASuccessfullNonReturnableOperation()
        {
            var count = InjectedScenarioContext.Get<int>("count");
            Action action = () =>
            {
                InjectedScenarioContext.Remove("count");
                InjectedScenarioContext.Add("count", count);
            };
            InjectedScenarioContext.Add("action", action);
        }

        [Given(@"I have entered a failing non-returnable operation which succeeds at (\d+) try")]
        public void GivenIHaveEnteredAFailingNonReturnableOperationWhichSucceedsAtTry(int whenToSucceed)
        {
            var count = InjectedScenarioContext.Get<int>("count");
            Action action = () =>
            {
                if (++count == whenToSucceed)
                {
                    return;
                }

                InjectedScenarioContext.Remove("count");
                InjectedScenarioContext.Add("count", count);
                throw new Exception(CustomExceptionErrorMessage);
            };
            InjectedScenarioContext.Add("action", action);
        }

        [Given("I have entered a failing non-returnable operation")]
        public void GivenIHaveEnteredAFailingNonReturnableOperation()
        {
            var count = InjectedScenarioContext.Get<int>("count");
            Action action = () =>
            {
                count++;
                InjectedScenarioContext.Remove("count");
                InjectedScenarioContext.Add("count", count);
                throw new Exception(CustomExceptionErrorMessage);
            };
            InjectedScenarioContext.Add("action", action);
        }

        [Given("I have entered a failing returnable operation")]
        public void GivenIHaveEnteredAFailingReturnableOperation()
        {
            var count = InjectedScenarioContext.Get<int>("count");
            Func<string> function = () =>
            {
                count++;
                InjectedScenarioContext.Remove("count");
                InjectedScenarioContext.Add("count", count);
                throw new Exception(CustomExceptionErrorMessage);
            };
            InjectedScenarioContext.Add("function", function);
        }

        [Given("I have entered (.*) attempts for (.*) milliseconds between")]
        public void GivenIHaveEnteredAttemptsForMillisecondsBetween(int attempts, int time)
        {
            InjectedScenarioContext.Add("attempts", attempts);
            InjectedScenarioContext.Add("time", time);
        }

        [Then("no retry attempts should be made")]
        public void ThenNoRetryAttemptsShouldBeMade()
        {
            int count;
            int before;
            int after;
            int failure;
            if (InjectedScenarioContext.TryGetValue("before", out before) && before > 0)
            {
                Assert.False(true);
            }

            if (InjectedScenarioContext.TryGetValue("after", out after) && after > 0)
            {
                Assert.False(true);
            }

            if (InjectedScenarioContext.TryGetValue("after", out failure) && failure > 0)
            {
                Assert.False(true);
            }

            if (InjectedScenarioContext.TryGetValue("count", out count))
            {
                Assert.Equal(0, count);
            }
        }

        [Then("ArgumentOutOfRangeException is thrown when attempts is less than 1")]
        public void ThenArgumentOutOfRangeExceptionIsThrownWhenAttemptsIsLessThanOne()
        {
            var attempts = InjectedScenarioContext.Get<int>("attempts");
            var exception = InjectedScenarioContext.Get<ArgumentOutOfRangeException>("exception");
            Assert.Equal($"Argument value <{attempts}> is less than <1>.{Environment.NewLine}Parameter name: attempts", exception.Message);
        }

        [Then("ArgumentOutOfRangeException is thrown when time is less than 1")]
        public void ThenArgumentOutOfRangeExceptionIsThrownWhenTimeIsLessThanOne()
        {
            var time = TimeSpan.FromMilliseconds(InjectedScenarioContext.Get<int>("time"));
            var exception = InjectedScenarioContext.Get<ArgumentOutOfRangeException>("exception");
            Assert.Equal($"Argument value <{time}> is less than or equal to <{TimeSpan.Zero}>.{Environment.NewLine}Parameter name: timeBetweenRetries",
                    exception.Message);
        }

        [Then(@"exactly (\d+) retry should happen")]
        public void ThenExactlyRetryShouldHappen(int retries)
        {
            var count = InjectedScenarioContext.Get<int>("count");
            Assert.Equal(retries, count);
        }

        [Then(@"took around (\d+) milliseconds in total")]
        public void TookMillisecondsInTotal(long milliseconds)
        {
            var count = InjectedScenarioContext.Get<int>("count");
            var time = InjectedScenarioContext.Get<long>("time");
            Assert.InRange(time, milliseconds, milliseconds + ((count + 1) * 50.5)); // TODO: Find a better overhead mean
        }

        [Then(@"took less than (\d+) milliseconds in total because of overhead")]
        public void TookLessThanMillisecondsInTotalBecauseOfOverhead(int milliseconds)
        {
            var time = InjectedScenarioContext.Get<long>("time");
            Assert.InRange(time, 0, milliseconds);
        }

        [Then(@"OnFailure will be dispatched exactly (\d+) times")]
        public void ThenOnFailureWillBeDispatchedExactlyTimes(int times)
        {
            var failure = InjectedScenarioContext.Get<int>("failure");
            Assert.Equal(times, failure);
        }

        [Then(@"OnBeforeRetry will be dispatched exactly (\d+) times")]
        public void ThenOnBeforeRetryWillBeDispatchedExactlyTimes(int times)
        {
            var before = InjectedScenarioContext.Get<int>("before");
            Assert.Equal(times, before);
        }

        [Then(@"OnAfterRetry will be dispatched exactly (\d+) times")]
        public void ThenOnAfterRetryWillBeDispatchedExactlyTimes(int times)
        {
            var after = InjectedScenarioContext.Get<int>("after");
            Assert.Equal(times, after);
        }

        [Then(@"AggregateException will be thrown with exactly (\d+) inner exceptions")]
        public void ThenAggregateExceptionWillBeThrownWithExactlyInnerExceptions(int count)
        {
            var exception = InjectedScenarioContext.Get<AggregateException>("aggregateException");
            Assert.Equal(count, exception.InnerExceptions.Count);
        }

        [Then(@"no AggregateException is thrown")]
        public void NoAggregateExceptionIsThrown()
        {
            AggregateException exception;
            if (InjectedScenarioContext.TryGetValue("aggregateException", out exception))
            {
                Assert.False(true);
            }
        }
    }
}