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
        public CancellationStepDefinitions(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [Given("I have entered (.*) milliseconds between"), Scope(Tag = "Cancellation")]
        public void GivenIHaveEnteredAttemptsForMillisecondsBetween(int time) => InjectedScenarioContext.Add("time", time);

        [Given(@"I have entered a failing non-returnable operation which fails at step (\d+) with (.*) and all others are (.*)")]
        public void GivenIHaveEnteredAFailingNon_ReturnableOperationWhichFailsAtStepWithArgumentExceptionAndAllOthersAreException(int step,
            Exceptions toFailWith, Exceptions others)
        {
            var count = InjectedScenarioContext.Get<int>("count");
            Action action = () =>
            {
                if (++count == step)
                {
                    ThrowWithExceptionByType(toFailWith);
                    return;
                }

                InjectedScenarioContext.Remove("count");
                InjectedScenarioContext.Add("count", count);
                ThrowWithExceptionByType(others);
            };
            InjectedScenarioContext.Add("action", action);
        }

        [Given(@"I have entered a failing returnable operation which fails at step (\d+) with (.*) and all others are (.*)")]
        public void GivenIHaveEnteredAFailingReturnableOperationWhichFailsAtStepWithArgumentExceptionAndAllOthersAreException(int step,
    Exceptions toFailWith, Exceptions others)
        {
            var count = InjectedScenarioContext.Get<int>("count");
            Func<string> function = () =>
            {
                if (++count == step)
                {
                    ThrowWithExceptionByType(toFailWith);
                }

                InjectedScenarioContext.Remove("count");
                InjectedScenarioContext.Add("count", count);
                ThrowWithExceptionByType(others);
                return "Hello world!";
            };
            InjectedScenarioContext.Add("function", function);
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
            var haveWhenToStopSetup = InjectedScenarioContext.TryGetValue("whenToStop", out whenToStop);
            var haveAttemptsSetup = InjectedScenarioContext.TryGetValue("attempts", out attempts);
            var time = TimeSpan.FromMilliseconds(InjectedScenarioContext.Get<int>("time"));
            var haveActionSetup = InjectedScenarioContext.TryGetValue("action", out action);
            var haveFunctionSetup = InjectedScenarioContext.TryGetValue("function", out function);
            var before = InjectedScenarioContext.Get<int>("before");
            var after = InjectedScenarioContext.Get<int>("after");
            var failure = InjectedScenarioContext.Get<int>("failure");

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
                        InjectedScenarioContext.Remove("before");
                        InjectedScenarioContext.Add("before", before);
                    }).OnAfterRetry((sender, args) =>
                    {
                        after++;
                        InjectedScenarioContext.Remove("after");
                        InjectedScenarioContext.Add("after", after);
                    }).OnFailure((sender, args) =>
                    {
                        failure++;
                        InjectedScenarioContext.Remove("failure");
                        InjectedScenarioContext.Add("failure", failure);
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
                InjectedScenarioContext.Add("aggregateException", ex);
            }
            finally
            {
                stopWatch.Stop();
                InjectedScenarioContext.Remove("time");
                InjectedScenarioContext.Add("time", stopWatch.ElapsedMilliseconds);
            }
        }

        [When(@"I setup up to fail after (.*) milliseconds for (.*) cancellation policy and attempt to run operation")]
        public void WhenISetupUpToFailAfterMillisecondsCancellationPolicyAndAttemptToRunOperation(int whenToStop, Strategy strategy)
        {
            Action action;
            Func<string> function;
            int attempts;
            var stopWatch = new Stopwatch();
            var haveAttemptsSetup = InjectedScenarioContext.TryGetValue("attempts", out attempts);
            var time = TimeSpan.FromMilliseconds(InjectedScenarioContext.Get<int>("time"));
            var haveActionSetup = InjectedScenarioContext.TryGetValue("action", out action);
            var haveFunctionSetup = InjectedScenarioContext.TryGetValue("function", out function);
            var before = InjectedScenarioContext.Get<int>("before");
            var after = InjectedScenarioContext.Get<int>("after");
            var failure = InjectedScenarioContext.Get<int>("failure");

            try
            {
                stopWatch.Start();
                var rule = haveAttemptsSetup ? Rule.Setup(strategy).Config(new Options(attempts, time)) :
                    Rule.Setup(Strategy.Sequential).Config(new Options(time));

                rule.Cancel(r => r.After(TimeSpan.FromMilliseconds(whenToStop)))
                .OnBeforeRetry((sender, args) =>
                {
                    before++;
                    InjectedScenarioContext.Remove("before");
                    InjectedScenarioContext.Add("before", before);
                }).OnAfterRetry((sender, args) =>
                {
                    after++;
                    InjectedScenarioContext.Remove("after");
                    InjectedScenarioContext.Add("after", after);
                }).OnFailure((sender, args) =>
                {
                    failure++;
                    InjectedScenarioContext.Remove("failure");
                    InjectedScenarioContext.Add("failure", failure);
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
                InjectedScenarioContext.Add("aggregateException", ex);
            }
            finally
            {
                stopWatch.Stop();
                InjectedScenarioContext.Remove("time");
                InjectedScenarioContext.Add("time", stopWatch.ElapsedMilliseconds);
            }
        }


        [Given(@"stops after (.*) milliseconds")]
        public void GivenStopsAfterMillisecond(int time)
        {
            InjectedScenarioContext.Remove("whenToStop");
            InjectedScenarioContext.Add("whenToStop", time);
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
