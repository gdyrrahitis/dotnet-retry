@Finite
Feature: Finite retries
	In order to test exponential retry policy
	As using a finite algorithm
	I want to verify the number of attempts and time took to retry an operation

Scenario: Execute exponential operation errorless without retrying
	Given I have entered a successfull non-returnable operation
	And I have entered 3 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then no retry attempts should be made

Scenario: Exponential zero attempts throws exception
	Given I have entered a successfull non-returnable operation
	And I have entered 0 attempts for 100 milliseconds between
	When I setup exponential rule configuration
	Then ArgumentOutOfRangeException is thrown when attempts is less than 1

Scenario: Exponential Zero time throws exception
	Given I have entered a successfull non-returnable operation
	And I have entered 10 attempts for 0 milliseconds between
	When I setup exponential rule configuration
	Then ArgumentOutOfRangeException is thrown when time is less than 1

Scenario: Execute failing exponential operation exhausts 1 attempt
	Given I have entered a failing non-returnable operation
	And I have entered 1 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 1 retry should happen
	And took less than 100 milliseconds in total because of overhead
	And OnFailure will be dispatched exactly 1 times
	And OnBeforeRetry will be dispatched exactly 1 times
	And OnAfterRetry will be dispatched exactly 1 times
	And AggregateException will be thrown with exactly 1 inner exceptions

Scenario: Execute failing exponential operation exhausts 2 attempts
	Given I have entered a failing non-returnable operation
	And I have entered 2 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 2 retry should happen
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

Scenario: Execute failing exponential operation exhausts 3 attempts
	Given I have entered a failing non-returnable operation
	And I have entered 3 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 3 retry should happen
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And AggregateException will be thrown with exactly 3 inner exceptions

Scenario: Execute failing exponential operation exhausts 4 attempts
	Given I have entered a failing non-returnable operation
	And I have entered 4 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 4 retry should happen
	And OnFailure will be dispatched exactly 4 times
	And OnBeforeRetry will be dispatched exactly 4 times
	And OnAfterRetry will be dispatched exactly 4 times
	And AggregateException will be thrown with exactly 4 inner exceptions

Scenario: Execute failing exponential operation exhausts 5 attempts
	Given I have entered a failing non-returnable operation
	And I have entered 5 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 5 retry should happen
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Execute failing exponential operation exhausts 6 attempts
	Given I have entered a failing non-returnable operation
	And I have entered 6 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 6 retry should happen
	And OnFailure will be dispatched exactly 6 times
	And OnBeforeRetry will be dispatched exactly 6 times
	And OnAfterRetry will be dispatched exactly 6 times
	And AggregateException will be thrown with exactly 6 inner exceptions

Scenario: Execute failing exponential operation exhausts 7 attempts
	Given I have entered a failing non-returnable operation
	And I have entered 7 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 7 retry should happen
	And OnFailure will be dispatched exactly 7 times
	And OnBeforeRetry will be dispatched exactly 7 times
	And OnAfterRetry will be dispatched exactly 7 times
	And AggregateException will be thrown with exactly 7 inner exceptions

Scenario: Execute failing exponential operation exhausts 8 attempts
	Given I have entered a failing non-returnable operation
	And I have entered 8 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 8 retry should happen
	And OnFailure will be dispatched exactly 8 times
	And OnBeforeRetry will be dispatched exactly 8 times
	And OnAfterRetry will be dispatched exactly 8 times
	And AggregateException will be thrown with exactly 8 inner exceptions

Scenario: Execute failing exponential operation exhausts 9 attempts
	Given I have entered a failing non-returnable operation
	And I have entered 9 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 9 retry should happen
	And OnFailure will be dispatched exactly 9 times
	And OnBeforeRetry will be dispatched exactly 9 times
	And OnAfterRetry will be dispatched exactly 9 times
	And AggregateException will be thrown with exactly 9 inner exceptions

Scenario: Execute failing exponential operation exhausts 10 attempts
	Given I have entered a failing non-returnable operation
	And I have entered 10 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 10 retry should happen
	And OnFailure will be dispatched exactly 10 times
	And OnBeforeRetry will be dispatched exactly 10 times
	And OnAfterRetry will be dispatched exactly 10 times
	And AggregateException will be thrown with exactly 10 inner exceptions

Scenario: Execute failing exponential operation to succeed at 1 try of overall 1
	Given I have entered a failing non-returnable operation which succeeds at 1 try
	And I have entered 2 attempts for 100 milliseconds between
	When I setup exponential rule configuration
	Then no retry attempts should be made

Scenario: Execute failing exponential operation to succeed at 2 try of overall 2
	Given I have entered a failing non-returnable operation which succeeds at 2 try
	And I have entered 2 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 1 retry should happen
	And OnFailure will be dispatched exactly 1 times
	And OnBeforeRetry will be dispatched exactly 1 times
	And OnAfterRetry will be dispatched exactly 1 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 1 try of overall 3
	Given I have entered a failing non-returnable operation which succeeds at 1 try
	And I have entered 3 attempts for 100 milliseconds between
	When I setup exponential rule configuration
	Then no retry attempts should be made

Scenario: Execute failing exponential operation to succeed at 2 try of overall 3
	Given I have entered a failing non-returnable operation which succeeds at 2 try
	And I have entered 3 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 1 retry should happen
	And OnFailure will be dispatched exactly 1 times
	And OnBeforeRetry will be dispatched exactly 1 times
	And OnAfterRetry will be dispatched exactly 1 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 3 try of overall 3
	Given I have entered a failing non-returnable operation which succeeds at 3 try
	And I have entered 3 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 2 retry should happen
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 1 try of overall 4
	Given I have entered a failing non-returnable operation which succeeds at 1 try
	And I have entered 4 attempts for 100 milliseconds between
	When I setup exponential rule configuration
	Then no retry attempts should be made

Scenario: Execute failing exponential operation to succeed at 2 try of overall 4
	Given I have entered a failing non-returnable operation which succeeds at 2 try
	And I have entered 4 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 1 retry should happen
	And OnFailure will be dispatched exactly 1 times
	And OnBeforeRetry will be dispatched exactly 1 times
	And OnAfterRetry will be dispatched exactly 1 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 3 try of overall 4
	Given I have entered a failing non-returnable operation which succeeds at 3 try
	And I have entered 4 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 2 retry should happen
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 4 try of overall 4
	Given I have entered a failing non-returnable operation which succeeds at 4 try
	And I have entered 4 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 3 retry should happen
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 1 try of overall 5
	Given I have entered a failing non-returnable operation which succeeds at 1 try
	And I have entered 5 attempts for 100 milliseconds between
	When I setup exponential rule configuration
	Then no retry attempts should be made

Scenario: Execute failing exponential operation to succeed at 2 try of overall 5
	Given I have entered a failing non-returnable operation which succeeds at 2 try
	And I have entered 5 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 1 retry should happen
	And OnFailure will be dispatched exactly 1 times
	And OnBeforeRetry will be dispatched exactly 1 times
	And OnAfterRetry will be dispatched exactly 1 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 3 try of overall 5
	Given I have entered a failing non-returnable operation which succeeds at 3 try
	And I have entered 5 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 2 retry should happen
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 4 try of overall 5
	Given I have entered a failing non-returnable operation which succeeds at 4 try
	And I have entered 5 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 3 retry should happen
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 5 try of overall 5
	Given I have entered a failing non-returnable operation which succeeds at 5 try
	And I have entered 5 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 4 retry should happen
	And OnFailure will be dispatched exactly 4 times
	And OnBeforeRetry will be dispatched exactly 4 times
	And OnAfterRetry will be dispatched exactly 4 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 1 try of overall 10
	Given I have entered a failing non-returnable operation which succeeds at 1 try
	And I have entered 10 attempts for 100 milliseconds between
	When I setup exponential rule configuration
	Then no retry attempts should be made

Scenario: Execute failing exponential operation to succeed at 2 try of overall 10
	Given I have entered a failing non-returnable operation which succeeds at 2 try
	And I have entered 10 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 1 retry should happen
	And OnFailure will be dispatched exactly 1 times
	And OnBeforeRetry will be dispatched exactly 1 times
	And OnAfterRetry will be dispatched exactly 1 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 3 try of overall 10
	Given I have entered a failing non-returnable operation which succeeds at 3 try
	And I have entered 10 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 2 retry should happen
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 4 try of overall 10
	Given I have entered a failing non-returnable operation which succeeds at 4 try
	And I have entered 10 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 3 retry should happen
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 5 try of overall 10
	Given I have entered a failing non-returnable operation which succeeds at 5 try
	And I have entered 10 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 4 retry should happen
	And OnFailure will be dispatched exactly 4 times
	And OnBeforeRetry will be dispatched exactly 4 times
	And OnAfterRetry will be dispatched exactly 4 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 6 try of overall 10
	Given I have entered a failing non-returnable operation which succeeds at 6 try
	And I have entered 10 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 5 retry should happen
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 7 try of overall 10
	Given I have entered a failing non-returnable operation which succeeds at 7 try
	And I have entered 10 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 6 retry should happen
	And OnFailure will be dispatched exactly 6 times
	And OnBeforeRetry will be dispatched exactly 6 times
	And OnAfterRetry will be dispatched exactly 6 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 8 try of overall 10
	Given I have entered a failing non-returnable operation which succeeds at 8 try
	And I have entered 10 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 7 retry should happen
	And OnFailure will be dispatched exactly 7 times
	And OnBeforeRetry will be dispatched exactly 7 times
	And OnAfterRetry will be dispatched exactly 7 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 9 try of overall 10
	Given I have entered a failing non-returnable operation which succeeds at 9 try
	And I have entered 10 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 8 retry should happen
	And OnFailure will be dispatched exactly 8 times
	And OnBeforeRetry will be dispatched exactly 8 times
	And OnAfterRetry will be dispatched exactly 8 times
	And no AggregateException is thrown

Scenario: Execute failing exponential operation to succeed at 10 try of overall 10
	Given I have entered a failing non-returnable operation which succeeds at 10 try
	And I have entered 10 attempts for 100 milliseconds between
	When I attempt to run exponential
	Then exactly 9 retry should happen
	And OnFailure will be dispatched exactly 9 times
	And OnBeforeRetry will be dispatched exactly 9 times
	And OnAfterRetry will be dispatched exactly 9 times
	And no AggregateException is thrown