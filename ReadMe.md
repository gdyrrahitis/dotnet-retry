# DotNetRetry
Retry mechanism for C#

[![Build Status](https://circleci.com/gh/gdyrrahitis/angular-101/tree/master.svg?style=shield&circle-token=:circle-token)](https://circleci.com/gh/gdyrrahitis/angular-101/tree/master)
[![Node version](https://img.shields.io/badge/node-4.5.0-brightgreen.svg?style=flat)](http://nodejs.org/download/)
[![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/gdyrrahitis/angular-101)

## Table Of Contents
* Description
* Classes
* Examples
* Tests

### Description
A retry library for C#, which can retry a given operation.
It consists of 3 Classes to use, `RetryWrapper.cs`, `RetryStatic.cs`, `Retry.cs`. The first two end up to use `Retry.cs` in the end. More on Classes section.
`Retry` class uses a recursive way to try to re-run the provided operation. The method retries are stored into a `Stack` and recursivelly, every X time, which is provided to the call of the retry method, the operation is popped out of the `Stack`, in order to run. If, it won't run, after the maximum retries have met, an `AggregateException` will be thrown, along with the exceptions thrown from the method invocation.
It also supports async methods for retrying, which tries to execute the method, waiting for the result.

### Classes
* `RetryWrapper.cs`. A wrapper class that wraps the static 'RetryStatic.cs`. It is intended to be used on test suites.
  *  Methods
    * GetRetryMechanism. Returns the Instance of `Retry` by calling the `RetryStatic.Instance` property.
* `RetryStatic.cs`. A static class that returns an instance of the `Retry.cs` class. It consists of an `Instance` property that returns an instance of the `Retry` class.
* `Retry.cs`. The class that consists all the logic for the retry attempts.
  * Methods
    * void Attempt. Attempts to try and run a void synchronous action
    * void Attempt<T>. Attempts to try and run a void synchronous action with a parameter of type T

### Examples
Just retrying a method
```
const string test = "abc123";
var retry = new Retry();
retry.Attempt(() => int.Parse(test), 2, Timespan.FromSeconds(2));
```

### Tests
Tests under `DotNetRetry.Tests` project, using `NUnit 3` framework.
