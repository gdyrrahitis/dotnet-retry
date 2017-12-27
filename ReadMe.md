# DotNetRetry
Retry mechanism for C#

[![Build Status](https://travis-ci.org/gdyrrahitis/dotnet-retry.svg?branch=features)](https://travis-ci.org/gdyrrahitis/dotnet-retry)
[![NuGet](https://img.shields.io/nuget/v/gdyrra.dotnet.retry.svg)](https://www.nuget.org/packages/gdyrra.dotnet.retry/1.0.0)
[![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/gdyrrahitis/dotnet-retry)

## Table of contents
* Description
* Classes
* Examples
* Tests

### Description
A retry library for C#, which can retry a given operation, for void or methods that return some value.
After reaching the maximum number of retries, a flatten `AggregateException` will be thrown, which contains all the exceptions occured for the method invocation.

### Retries
For retries a linear technique is implemented in this current `1.0.0` version.
Linear or else sequential retry policy, is basically a technique to try call a certain method X amount of times with a delay specified between retries. This technique will try to get a successful result as many times as it has been to try and will always have the same delay between these attempts. After maximum retry limit is reached, it throws a flatten AggregateException back to the caller.

### Classes
`RetryRule`. The class that consists all the logic for the retry attempts.
	* Methods
		* void Attempt. Attempts to try and run a void synchronous action
		* T Attempt<T>. Attempts to try and run a synchronous Func<T>
It also exposes events for callers to subscribe to. Events are:
	* `OnBeforeRetry`. This event is dispatched before this library attempts to call the target method.
	* `OnAfterRetry`. This event is dispatched after a retry, which means after a failed but also after a successful call.
	* `OnFailure`. This event is dispatched after an exception has been thrown from the target method.

### Examples
**Just a method invocation**

Just retrying a method, twice, waiting for two seconds between retries.
```
var rule = RetryRule.SetupRules();
rule.Attempt(() => TryThisOperation(), 2, Timespan.FromSeconds(2));
// or just rule.Attempt(TryThisOperation, 2, Timespan.FromSeconds(2));
```

**Or passing variables, it doesn't matter**
```
// x = 1, y = "abc"
var rule = RetryRule.SetupRules();
rule.Attempt(() => TryThisOperation(x, y), 2, Timespan.FromSeconds(2));
```

**Using methods that return a value**

They will be treated as `Func<T>`
```
// Greet() method returns "Hello there!"
var rule = RetryRule.SetupRules();
var greeting = rule.Attempt(Greet, 2, Timespan.FromSeconds(2));
// Hello there!
```

**Handling exceptions**

The following will fail in all tries. If that happens, `Attempt` method will throw an `AggregateException` with all exceptions listed.
```
var rule = RetryRule.SetupRules();
try 
{
    rule.Attempt(() => int.Parse("abc"), 3, Timespan.FromSeconds(1));
}
catch(AggregateException ex) 
{
    // Handle all individual exceptions
}
```

**Utilizing events**

In `DotNetRetry.Static` there is another `Retry` class, which returns a singleton instance
```
var rule = RetryRule.SetupRules()
	.OnBeforeRetry((sender, args) => {
		// Do something before target method is called.
	})
	.OnAfterRetry((sender, args) => { 
		// Do something after target method is called.
	})
	.OnFailure((sender, args) => { 
		// Do something when target method has failed.
	});
```

### Tests
Tests under `DotNetRetry.Tests` project, using the `XUnit` test framework.

#### Running from command line
If you have dotnet cli installed, you can build the project from the solution folder, and then run the tests
```
$ > dotnet build

$ > "packages/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe" DotNetRetryTests/bin/Release/DotNetRetry.Tests.dll
```
## License
Library and code is free to use in commercial applications and/or libraries without restrictions.