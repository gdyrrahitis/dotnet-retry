﻿# DotNetRetry
Retry mechanism for C#

[![Build Status](https://travis-ci.org/gdyrrahitis/dotnet-retry.svg?branch=master)](https://travis-ci.org/gdyrrahitis/dotnet-retry)
[![NuGet](https://img.shields.io/nuget/v/gdyrra.dotnet.retry.svg)](https://www.nuget.org/packages/gdyrra.dotnet.retry/0.1.0)
[![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/gdyrrahitis/dotnet-retry)

## Table of contents
* Description
* Classes
* Examples
* Tests

### Description
A retry library for C#, which can retry a given operation.
It consists of 3 Classes to use, `RetryWrapper.cs`, `RetryStatic.cs`, `Retry.cs`. The first two end up to use `Retry.cs` in the end. More on Classes section.
`Retry` class contains operations, methods named `Attemp` to re-run a failed action or function (`Func<T>`). Method retries are performed in a loop, and upon failure are delayed for X amount of time, which X is provided to the call of the retry method. After reaching the maximum number of retries, an `AggregateException` will be thrown, which contains all the exceptions occured for the method invocation.

### Retries
For retries a linear technique is implemented in this current `0.1.0` version.

### Classes
* `RetryWrapper.cs`. A wrapper class that wraps the static 'RetryStatic.cs`. It is intended to be used on test suites.
  *  Methods
    * GetRetryMechanism. Returns the Instance of `Retry` by calling the `RetryStatic.Instance` property.
* `RetryStatic.cs`. A static class that returns an instance of the `Retry.cs` class. It consists of an `Instance` property that returns an instance of the `Retry` class.
* `Retry.cs`. The class that consists all the logic for the retry attempts.
  * Methods
    * void Attempt. Attempts to try and run a void synchronous action
    * T Attempt<T>. Attempts to try and run a synchronous Func<T>

### Examples
**Just a method invocation**

Just retrying a method, twice, waiting for two seconds between retries.
```
var retry = new Retry();
retry.Attempt(() => TryThisOperation(), 2, Timespan.FromSeconds(2));
// or just retry.Attempt(TryThisOperation, 2, Timespan.FromSeconds(2));
```

**Or passing variables, it doesn't matter**
```
// x = 1, y = "abc"
var retry = new Retry();
retry.Attempt(() => TryThisOperation(x, y), 2, Timespan.FromSeconds(2));
```

**Using methods that return a value**

They will be treated as `Func<T>`
```
// Greet() method returns "Hello there!"
var retry = new Retry();
var greeting = retry.Attempt(Greet, 2, Timespan.FromSeconds(2));
// Hello there!
```

**Handling exceptions**

The following will fail in all tries. If that happens, `Attempt` method will throw an `AggregateException` with all exceptions listed.
```
var retry = new Retry();
try 
{
    retry.Attempt(() => int.Parse("abc"), 3, Timespan.FromSeconds(1));
}
catch(AggregateException ex) 
{
    // Handle all individual exceptions
}
```

**Using singleton instance**

In `DotNetRetry.Static` there is another `Retry` class, which returns a singleton instance
```
DotNetRetry.Static.Retry.Attempt(TryThisOperation, 2, Timespan.FromSeconds(2));
```

### Tests
Tests under `DotNetRetry.Tests` project, using `NUnit 3` framework.

#### Running from command line
If you have dotnet cli installed, you can build the project from the solution folder, and then run the tests
```
$ > dotnet build

$ > "packages/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe" DotNetRetryTests/bin/Release/DotNetRetry.Tests.dll
```
## License
Library and code is free to use in commercial applications and/or libraries without restrictions.