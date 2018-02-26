namespace DotNetRetry.Acceptance.Tests.Scenarios.Sequential
{
    using System;
    using System.Diagnostics;
    using Rules;
    using Rules.Configuration;
    using TechTalk.SpecFlow;
    using static Xunit.Assert;

    //[Binding]
    public class SequentialStepDefinitions
    {
        protected const string CustomExceptionErrorMessage = "Custom Exception";

        [BeforeScenario]
        public void SetupExecutionCount()
        {
            ScenarioContext.Current.Add("count", 0);
            ScenarioContext.Current.Add("before", 0);
            ScenarioContext.Current.Add("after", 0);
            ScenarioContext.Current.Add("failure", 0);
        }

        [Given("I have entered a successfull non-returnable operation")]
        public void GivenIHaveEnteredASuccessfullNonReturnableOperation()
        {
            var count = ScenarioContext.Current.Get<int>("count");
            Action action = () =>
            {
                ScenarioContext.Current.Remove("count");
                ScenarioContext.Current.Add("count", count);
            };
            ScenarioContext.Current.Add("action", action);
        }

        [Given(@"I have entered a failing non-returnable operation which succeeds at (\d+) try")]
        public void GivenIHaveEnteredAFailingNonReturnableOperationWhichSucceedsAtTry(int whenToSucceed)
        {
            var count = ScenarioContext.Current.Get<int>("count");
            Action action = () =>
            {
                if (++count == whenToSucceed)
                {
                    return;
                }

                ScenarioContext.Current.Remove("count");
                ScenarioContext.Current.Add("count", count);
                throw new Exception(CustomExceptionErrorMessage);
            };
            ScenarioContext.Current.Add("action", action);
        }

        [Given("I have entered a failing non-returnable operation")]
        public void GivenIHaveEnteredAFailingNonReturnableOperation()
        {
            var count = ScenarioContext.Current.Get<int>("count");
            Action action = () =>
            {
                count++;
                ScenarioContext.Current.Remove("count");
                ScenarioContext.Current.Add("count", count);
                throw new Exception(CustomExceptionErrorMessage);
            };
            ScenarioContext.Current.Add("action", action);
        }

        [Given("I have entered a failing returnable operation")]
        public void GivenIHaveEnteredAFailingReturnableOperation()
        {
            var count = ScenarioContext.Current.Get<int>("count");
            Func<string> function = () =>
            {
                count++;
                ScenarioContext.Current.Remove("count");
                ScenarioContext.Current.Add("count", count);
                throw new Exception(CustomExceptionErrorMessage);
            };
            ScenarioContext.Current.Add("function", function);
        }

        [Given("I have entered (.*) attempts for (.*) milliseconds between")]
        public void GivenIHaveEnteredAttemptsForMillisecondsBetween(int attempts, int time)
        {
            ScenarioContext.Current.Add("attempts", attempts);
            ScenarioContext.Current.Add("time", time);
        }

        [Then("no retry attempts should be made")]
        public void ThenNoRetryAttemptsShouldBeMade()
        {
            int count;
            int before;
            int after;
            int failure;
            if (ScenarioContext.Current.TryGetValue("before", out before) && before > 0)
            {
                False(true);
            }

            if (ScenarioContext.Current.TryGetValue("after", out after) && after > 0)
            {
                False(true);
            }

            if (ScenarioContext.Current.TryGetValue("after", out failure) && failure > 0)
            {
                False(true);
            }

            if (ScenarioContext.Current.TryGetValue("count", out count))
            {
                Equal(0, count);
            }
        }

        [Then("ArgumentOutOfRangeException is thrown when attempts is less than 1")]
        public void ThenArgumentOutOfRangeExceptionIsThrownWhenAttemptsIsLessThanOne()
        {
            var attempts = ScenarioContext.Current.Get<int>("attempts");
            var exception = ScenarioContext.Current.Get<ArgumentOutOfRangeException>("exception");
            Equal($"Argument value <{attempts}> is less than <1>.{Environment.NewLine}Parameter name: attempts", exception.Message);
        }

        [Then("ArgumentOutOfRangeException is thrown when time is less than 1")]
        public void ThenArgumentOutOfRangeExceptionIsThrownWhenTimeIsLessThanOne()
        {
            var time = TimeSpan.FromMilliseconds(ScenarioContext.Current.Get<int>("time"));
            var exception = ScenarioContext.Current.Get<ArgumentOutOfRangeException>("exception");
            Equal($"Argument value <{time}> is less than or equal to <{TimeSpan.Zero}>.{Environment.NewLine}Parameter name: timeBetweenRetries",
                    exception.Message);
        }

        [Then(@"exactly (\d+) retry should happen")]
        public void ThenExactlyRetryShouldHappen(int retries)
        {
            var count = ScenarioContext.Current.Get<int>("count");
            Equal(retries, count);
        }

        [Then(@"took around (\d+) milliseconds in total")]
        public void TookMillisecondsInTotal(long milliseconds)
        {
            var count = ScenarioContext.Current.Get<int>("count");
            var time = ScenarioContext.Current.Get<long>("time");
            InRange(time, milliseconds, milliseconds + ((count + 1) * 20.5)); // TODO: Find a better overhead mean
        }

        [Then(@"took less than (\d+) milliseconds in total because of overhead")]
        public void TookLessThanMillisecondsInTotalBecauseOfOverhead(int milliseconds)
        {
            var time = ScenarioContext.Current.Get<long>("time");
            InRange(time, 0, milliseconds);
        }

        [Then(@"OnFailure will be dispatched exactly (\d+) times")]
        public void ThenOnFailureWillBeDispatchedExactlyTimes(int times)
        {
            var failure = ScenarioContext.Current.Get<int>("failure");
            Equal(times, failure);
        }

        [Then(@"OnBeforeRetry will be dispatched exactly (\d+) times")]
        public void ThenOnBeforeRetryWillBeDispatchedExactlyTimes(int times)
        {
            var before = ScenarioContext.Current.Get<int>("before");
            Equal(times, before);
        }

        [Then(@"OnAfterRetry will be dispatched exactly (\d+) times")]
        public void ThenOnAfterRetryWillBeDispatchedExactlyTimes(int times)
        {
            var after = ScenarioContext.Current.Get<int>("after");
            Equal(times, after);
        }

        [Then(@"AggregateException will be thrown with exactly (\d+) inner exceptions")]
        public void ThenAggregateExceptionWillBeThrownWithExactlyInnerExceptions(int count)
        {
            var exception = ScenarioContext.Current.Get<AggregateException>("aggregateException");
            Equal(count, exception.InnerExceptions.Count);
        }

        [Then(@"no AggregateException is thrown")]
        public void NoAggregateExceptionIsThrown()
        {
            AggregateException exception;
            if (ScenarioContext.Current.TryGetValue("aggregateException", out exception))
            {
                False(true);
            }
        }
    }
}