namespace DotNetRetry.Acceptance.Tests.Scenarios
{
    using System;
    using System.Diagnostics;
    using Rules;
    using Rules.Configuration;
    using TechTalk.SpecFlow;

    [Binding, Scope(Tag = "Cancellation")]
    public sealed class CancellationStepDefinitions : GlobalStepDefinitions
    {
        [Given("I have entered (.*) milliseconds between"), Scope(Tag = "Cancellation")]
        public void GivenIHaveEnteredAttemptsForMillisecondsBetween(int time) => ScenarioContext.Current.Add("time", time);

        [Given(@"I have entered a failing non-returnable operation which fails at step (\d+) with (.*) and all others are (.*)")]
        public void GivenIHaveEnteredAFailingNon_ReturnableOperationWhichFailsAtStepWithArgumentExceptionAndAllOthersAreException(int step,
            Exceptions toFailWith, Exceptions others)
        {
            var count = ScenarioContext.Current.Get<int>("count");
            Action action = () =>
            {
                if (++count == step)
                {
                    ThrowWithExceptionByType(toFailWith);
                    return;
                }

                ScenarioContext.Current.Remove("count");
                ScenarioContext.Current.Add("count", count);
                ThrowWithExceptionByType(others);
            };
            ScenarioContext.Current.Add("action", action);
        }

        [Given(@"I have entered a failing returnable operation which fails at step (\d+) with (.*) and all others are (.*)")]
        public void GivenIHaveEnteredAFailingReturnableOperationWhichFailsAtStepWithArgumentExceptionAndAllOthersAreException(int step,
    Exceptions toFailWith, Exceptions others)
        {
            var count = ScenarioContext.Current.Get<int>("count");
            Func<string> function = () =>
            {
                if (++count == step)
                {
                    ThrowWithExceptionByType(toFailWith);
                }

                ScenarioContext.Current.Remove("count");
                ScenarioContext.Current.Add("count", count);
                ThrowWithExceptionByType(others);
                return "Hello world!";
            };
            ScenarioContext.Current.Add("function", function);
        }

        private void ThrowWithExceptionByType(Exceptions toFailWith)
        {
            switch (toFailWith)
            {
                case Exceptions.ArgumentException:
                    throw new ArgumentException();
                case Exceptions.ArgumentOutOfRangeException:
                    throw new ArgumentOutOfRangeException();
                case Exceptions.NullReferenceException:
                    throw new NullReferenceException();
                case Exceptions.Exception:
                    throw new Exception();
                default:
                    throw new ArgumentOutOfRangeException(nameof(toFailWith), toFailWith, null);
            }
        }

        [When(@"I setup up to fail on (.*) for (.*) cancellation policy and attempt to run operation"), Scope(Tag = "Finite")]
        public void WhenISetupUpToRetriesCancellationPolicyAndAttemptToRunOperation(Exceptions exception, Strategy strategy)
        {
            Action action;
            Func<string> function;
            int attempts;
            int whenToStop;
            var stopWatch = new Stopwatch();
            var haveWhenToStopSetup = ScenarioContext.Current.TryGetValue("whenToStop", out whenToStop);
            var haveAttemptsSetup = ScenarioContext.Current.TryGetValue("attempts", out attempts);
            var time = TimeSpan.FromMilliseconds(ScenarioContext.Current.Get<int>("time"));
            var haveActionSetup = ScenarioContext.Current.TryGetValue("action", out action);
            var haveFunctionSetup = ScenarioContext.Current.TryGetValue("function", out function);
            var before = ScenarioContext.Current.Get<int>("before");
            var after = ScenarioContext.Current.Get<int>("after");
            var failure = ScenarioContext.Current.Get<int>("failure");

            try
            {
                stopWatch.Start();
                var rule = haveAttemptsSetup ? Rule.Setup(strategy).Config(new Options(attempts, time)) :
                    Rule.Setup(strategy).Config(new Options(time));

                rule.Cancel(r =>
                {
                    if (haveWhenToStopSetup)
                    {
                        r.After(TimeSpan.FromMilliseconds(whenToStop));
                    }

                    r.OnFailure(GetExceptionType(exception));
                })
                    .OnBeforeRetry((sender, args) =>
                    {
                        before++;
                        ScenarioContext.Current.Remove("before");
                        ScenarioContext.Current.Add("before", before);
                    }).OnAfterRetry((sender, args) =>
                    {
                        after++;
                        ScenarioContext.Current.Remove("after");
                        ScenarioContext.Current.Add("after", after);
                    }).OnFailure((sender, args) =>
                    {
                        failure++;
                        ScenarioContext.Current.Remove("failure");
                        ScenarioContext.Current.Add("failure", failure);
                    });

                if (haveActionSetup)
                {
                    rule.Attempt(action);
                }
                else if (haveFunctionSetup)
                {
                    rule.Attempt(function);
                }
                else
                {
                    throw new InvalidOperationException("No action, nor function is setup");
                }
            }
            catch (AggregateException ex)
            {
                ScenarioContext.Current.Add("aggregateException", ex);
            }
            finally
            {
                stopWatch.Stop();
                ScenarioContext.Current.Remove("time");
                ScenarioContext.Current.Add("time", stopWatch.ElapsedMilliseconds);
            }
        }

        [When(@"I setup up to fail after (.*) milliseconds for (.*) cancellation policy and attempt to run operation")]
        public void WhenISetupUpToFailAfterMillisecondsCancellationPolicyAndAttemptToRunOperation(int whenToStop, Strategy strategy)
        {
            Action action;
            Func<string> function;
            int attempts;
            var stopWatch = new Stopwatch();
            var haveAttemptsSetup = ScenarioContext.Current.TryGetValue("attempts", out attempts);
            var time = TimeSpan.FromMilliseconds(ScenarioContext.Current.Get<int>("time"));
            var haveActionSetup = ScenarioContext.Current.TryGetValue("action", out action);
            var haveFunctionSetup = ScenarioContext.Current.TryGetValue("function", out function);
            var before = ScenarioContext.Current.Get<int>("before");
            var after = ScenarioContext.Current.Get<int>("after");
            var failure = ScenarioContext.Current.Get<int>("failure");

            try
            {
                stopWatch.Start();
                var rule = haveAttemptsSetup ? Rule.Setup(strategy).Config(new Options(attempts, time)) :
                    Rule.Setup(Strategy.Sequential).Config(new Options(time));

                rule.Cancel(r => r.After(TimeSpan.FromMilliseconds(whenToStop)))
                .OnBeforeRetry((sender, args) =>
                {
                    before++;
                    ScenarioContext.Current.Remove("before");
                    ScenarioContext.Current.Add("before", before);
                }).OnAfterRetry((sender, args) =>
                {
                    after++;
                    ScenarioContext.Current.Remove("after");
                    ScenarioContext.Current.Add("after", after);
                }).OnFailure((sender, args) =>
                {
                    failure++;
                    ScenarioContext.Current.Remove("failure");
                    ScenarioContext.Current.Add("failure", failure);
                });

                if (haveActionSetup)
                {
                    rule.Attempt(action);
                }
                else if (haveFunctionSetup)
                {
                    rule.Attempt(function);
                }
                else
                {
                    throw new InvalidOperationException("No action, nor function is setup");
                }
            }
            catch (AggregateException ex)
            {
                ScenarioContext.Current.Add("aggregateException", ex);
            }
            finally
            {
                stopWatch.Stop();
                ScenarioContext.Current.Remove("time");
                ScenarioContext.Current.Add("time", stopWatch.ElapsedMilliseconds);
            }
        }


        [Given(@"stops after (.*) milliseconds")]
        public void GivenStopsAfterMillisecond(int time)
        {
            ScenarioContext.Current.Remove("whenToStop");
            ScenarioContext.Current.Add("whenToStop", time);
        }

        private Type GetExceptionType(Exceptions exception)
        {
            switch (exception)
            {
                case Exceptions.ArgumentException:
                    return typeof(ArgumentException);
                case Exceptions.ArgumentOutOfRangeException:
                    return typeof(ArgumentOutOfRangeException);
                case Exceptions.NullReferenceException:
                    return typeof(NullReferenceException);
                case Exceptions.Exception:
                    return typeof(Exception);
                default:
                    throw new ArgumentOutOfRangeException(nameof(exception), exception, null);
            }
        }
    }
}
