# DotNetRetry
Retry mechanism for C#

[![Build Status](https://travis-ci.org/gdyrrahitis/dotnet-retry.svg?branch=features)](https://travis-ci.org/gdyrrahitis/dotnet-retry)
[![NuGet](https://img.shields.io/nuget/v/gdyrra.dotnet.retry.svg)](https://www.nuget.org/packages/gdyrra.dotnet.retry/1.0.0-alpha)
[![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/gdyrrahitis/dotnet-retry)

# Table of contents
* Description
* Retry policies
	* Sequential
	* Exponential
* Classes
* Examples
* Tests
* License

## Description
A retry library for C#, which attempts to execute a given operation using a given retry policy. Operations can be void or return a value.
If an operation fails for any reason, a flatten `AggregateException` will be thrown, which contains all the exceptions occured for the method invocation.

## Retry policies
### Sequential
Sequential or else linear retry policy, is basically a technique which tries to execute a certain operation X amount of times with a fixed delay specified between retries.
This policy will attempt to execute the given operation as many times as it is instructed to do so and will always have the same delay between these attempts. After maximum retry limit is reached, it throws a flatten AggregateException back to the caller, with all exceptions listed.

### Exponential
Exponential retry policy, or exponential backoff technique, increases the wait time between retries after each failure in exponential fashion. For example, when an operation fails for the first time, the retry will be after 1 second. The second time, the retry will be after 2 seconds, after the third try, the retry will be after 4 seconds and so on and so forth. In other words, wait time increases incrementally between consecutive retry operations after each failure.

## Classes
### `Rule`

This class contains the public API for the retry attempts.
It exposes a static method, `SetupRules(Strategies strategy)` that takes a `Strategies` enum, indicating which retry policy to use. This method returns a `RuleOptions` instance, which is used to setup global options for the selected retry policy. After options have been set, you can use `Retry`'s public API to setup cancellation policies, utilize events and of course execute the operation in question.

#### Methods
* `RuleOptions SetupRules(Strategies strategy)`. Public static method. Sets up the retry policy. It returns a `RuleOptions` class, which can be used to setup global options for specified policy.
* `void Attempt(Action action)`. Attempts to try and run a void synchronous action.
* `T Attempt<T>(Func<T> function)`. Attempts to try and run a synchronous `Func<T>`.
* `IRule Cancel(Action<CancellationRule> cancellationRules)`. Specifies cancellation policies via `CancellationRule` instance.

#### Events
It also exposes events for callers to subscribe to. Events are:
* `OnBeforeRetry(EventHandler handler)`. This event is dispatched before any attempt to invoke target operation.
* `OnAfterRetry(EventHandler handler)`. This event is dispatched after a retry, which means after a failed but also after a successful call.
* `OnFailure(EventHandler handler)`. This event is dispatched after an exception has been thrown from the target operation.

### `RuleOptions`
#### Methods
* `Rule Config(Options options)`. It receives an `Option` object as input, to specify global options for retry policy. It returns the parent `Rule` class.

### `Options`
A simple immutable class that sets up global options for specified retry policy. Two options are included, `Attempts` and `Time`. They can be both set, or one of them.
* `int Attempts` which is the maximum number of retries. Zero indicates infinite retries.
* `TimeSpan Time` which is used differently in various policies. For example, `Sequential` policy uses this fixed time to wait after a failure before retrying again. `Exponential` policy uses this time as the maximum backoff time, which is used in algorithm to calculate the mean backoff time. In other words, this is the maximum wait time for each retry attempt on exponential policy.

### `CancellationRule`
#### Methods
* `CancellationRule After(TimeSpan time)`. This method sets up a rule to stop retrying after a specified time has passed.
* `ExceptionRule OnFailure<TException>()`. This method sets up a rule to stop retrying when a specific exception occurs, passing the exception as a generic parameter.
* `ExceptionRule OnFailure(Type type)`. This method sets up a rule to stop retrying when a specific exception occurs, passing the exception as a method parameter.

### `ExceptionRule`
#### Methods
* `ExceptionRule Or<TException>()`. This is used to chain exception rules when calling `OnFailure` method of `CancellationRule` class. It adds the specified generic parameter in the exception list and cancels the retry attempts when this exception occurs.
* `ExceptionRule Or(Type type)`. This is used to chain exception rules when calling `OnFailure` method of `CancellationRule` class. It adds the specified method parameter in the exception list and cancels the retry attempts when this exception occurs.
* `CancellationRule End()`. Stops the exception rules chaining and returns the parent `CancellationRule` instance.

## Examples
### **Picking a policy**

Use the `Strategies` enum
```
// Sets up a Sequential/Linear retry policy
var rule = Rule.SetupRules(Strategies.Sequential); 

// Sets up an expontial backoff retry policy
var rule = Rule.SetupRules(Strategies.Exponential); 
```

### **Define options**
Call the `Config` method and provide a new instance of `Options` object, indicating the max number of retries and the timing for these retries, based on the policy chosen.
```
// Sets up a Sequential retry policy with maximum 3 tries, having an 100 millisecond window between each retry
var rule = Rule.SetupRules(Strategies.Sequential).Config(new Options(3, TimeSpan.FromMilliseconds(100)));

// Sets up an Exponential retry policy with maximum 6 tries, having a 4 second maximum backoff time.
var rule = Rule.SetupRules(Strategies.Exponential).Config(new Options(6, TimeSpan.FromSeconds(4)));
```

### **Just a method invocation**

Just retrying a method, twice, waiting for two seconds between retries.
```
var rule = Rule.SetupRules(Strategies.Sequential).Config(new Options(2, TimeSpan.FromSeconds(2)));

rule.Attempt(() => TryThisOperation());
// or just rule.Attempt(TryThisOperation);
```

### **Or passing variables**
```
// x = 1, y = "abc"
var rule = Rule.SetupRules(Strategies.Sequential).Config(new Options(2, TimeSpan.FromSeconds(2)));

rule.Attempt(() => TryThisOperation(x, y));
```

### **Using methods that return a value**

They will be treated as `Func<T>`
```
// Greet() method returns "Hello there!"
var rule = Rule.SetupRules(Strategies.Sequential).Config(new Options(2, TimeSpan.FromSeconds(2)));

var greeting = rule.Attempt(Greet);
Console.WriteLine(greeting); // Hello there!
```

### **Handling exceptions**

The following will fail in all tries. If that happens, `Attempt` method will throw an `AggregateException` with all exceptions listed.

**Note: Be sure to wrap your `Attempt` call in a `try...catch` block.**
```
var rule = Rule.SetupRules(Strategies.Sequential).Config(new Options(3, TimeSpan.FromMilliseconds(500)));
try 
{
    rule.Attempt(() => int.Parse("abc"));
}
catch(AggregateException ex) 
{
    // Handle all individual exceptions
}
```

### **Utilizing events**

`Rule` class defines a public API that can utilize events. You can subscribe to certain events happening before or after a retry attempt or when a failure occurs during a retry attempt.
```
var rule = Rule.SetupRules(Strategies.Sequential).Config(new Options(3, TimeSpan.FromMilliseconds(500)));

rule.OnBeforeRetry((sender, args) => {
	// Do something before target method is called.
})
.OnAfterRetry((sender, args) => { 
	// Do something after target method is called.
})
.OnFailure((sender, args) => { 
	// Do something when target method has failed.
});
```

### **Cancellation rules**
`Rule` class has available a public API which defines cancellation rules for the selected retry policy, throwing an `AggregateException` with all the exceptions listed up until this point. These come in two flavors:
* Cancels the retry policy after a specific amount of time has passed.
* Cancels the retry policy when a specific exception has occured.
```
// Cancels the retry policy when an ArgumentException occurs
var rule = Rule.SetupRules(Strategies.Sequential)
	.Config(new Options(3, TimeSpan.FromMilliseconds(500)))
	.Cancel(c => c.OnFailure<ArgumentException>());

rule.Attempt(() =>
{
	// This will stop the policy from retrying this method
	// and is going to return an AggregateException including the exception below
	throw new ArgumentException("Custom Exception");
});
```

```
// Cancels the retry policy when an ArgumentException occurs with non-generic method
var rule = Rule.SetupRules(Strategies.Sequential)
	.Config(new Options(3, TimeSpan.FromMilliseconds(500)))
	.Cancel(c => c.OnFailure(typeof(ArgumentException)));

rule.Attempt(() =>
{
	// This will stop the policy from retrying this method
	// and is going to return an AggregateException including the exception below
	throw new ArgumentException("Custom Exception");
});
```

```
// Cancels the retry policy when an ArgumentException or
// ArgumentOutOfRangeException occurs (same result with non-generic method)
var rule = Rule.SetupRules(Strategies.Sequential)
	.Config(new Options(3, TimeSpan.FromMilliseconds(500)))
	.Cancel(c => c.OnFailure<ArgumentException>()
		      .Or<ArgumentOutOfRangeException>()
	);

rule.Attempt(() =>
{
	// This will stop the policy from retrying this method
	// and is going to return an AggregateException including the exception below
	throw new ArgumentOutOfRangeException("Custom Exception");
});
```

```
// Cancels the retry policy after 300 milliseconds passed
var rule = Rule.SetupRules(Strategies.Sequential)
	.Config(new Options(10, TimeSpan.FromMilliseconds(100)))
	.Cancel(c => c.After(TimeSpan.FromMilliseconds(300)));

rule.Attempt(() =>
{
	// This will cause a new attempt due to failure.
	// The retry policy will cancel after 300 milliseconds have passed.
	throw new ArgumentOutOfRangeException("Custom Exception");
});
```

```
// Can mix and match cancellation rules
var rule = Rule.SetupRules(Strategies.Sequential)
	.Config(new Options(10, TimeSpan.FromMilliseconds(100)))
	.Cancel(c => c.After(TimeSpan.FromMilliseconds(300))
	              .OnFailure<ArgumentException>()
	);

rule.Attempt(...);
```

## Tests
Tests under `DotNetRetry.Unit.Tests` project, using the `XUnit` test framework.

### Running from command line
If you have dotnet cli installed, you can build the project from the solution folder, and then run the tests
```
$ > dotnet build

$ > "packages/xunit.runner.console.2.3.1/tools/xunit.console.exe" DotNetRetryTests/bin/Release/DotNetRetry.Unit.Tests.dll
```
## License
Library and code is free to use in commercial applications and/or libraries without restrictions.