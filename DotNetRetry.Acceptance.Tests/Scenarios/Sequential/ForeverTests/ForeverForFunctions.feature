@Forever, @Functions
Feature: Forever for function retries
	In order to test sequential retry policy
	As using a forever-style algorithm
	I want to verify the number of attempts and time took to retry an operation

Scenario: Execute operation errorless without retrying
	Given I have entered a successfull returnable operation
	And I have entered 100 milliseconds between
	When I attempt to run it
	Then no retry attempts should be made
	And result is Hello world!

Scenario: Zero time throws exception
	Given I have entered a successfull returnable operation
	And I have entered 0 milliseconds between
	When I setup rule configuration
	Then ArgumentOutOfRangeException is thrown when time is less than 1

Scenario: Execute failing operation to succeed at 1 try
	Given I have entered a failing returnable operation which succeeds at 1 try
	And I have entered 100 milliseconds between
	When I attempt to run it
	Then no retry attempts should be made

Scenario: Execute failing operation to succeed at 2 try
	Given I have entered a failing returnable operation which succeeds at 2 try
	And I have entered 100 milliseconds between
	When I attempt to run it
	Then exactly 1 retry should happen
	And took around 100 milliseconds in total
	And OnFailure will be dispatched exactly 1 times
	And OnBeforeRetry will be dispatched exactly 1 times
	And OnAfterRetry will be dispatched exactly 1 times
	And no AggregateException is thrown

Scenario: Execute failing operation to succeed at 3 try
	Given I have entered a failing returnable operation which succeeds at 3 try
	And I have entered 100 milliseconds between
	When I attempt to run it
	Then exactly 2 retry should happen
	And took around 200 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And no AggregateException is thrown

Scenario: Execute failing operation to succeed at 4 try
	Given I have entered a failing returnable operation which succeeds at 4 try
	And I have entered 100 milliseconds between
	When I attempt to run it
	Then exactly 3 retry should happen
	And took around 300 milliseconds in total
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And no AggregateException is thrown

Scenario: Execute failing operation to succeed at 5 try
	Given I have entered a failing returnable operation which succeeds at 5 try
	And I have entered 100 milliseconds between
	When I attempt to run it
	Then exactly 4 retry should happen
	And took around 400 milliseconds in total
	And OnFailure will be dispatched exactly 4 times
	And OnBeforeRetry will be dispatched exactly 4 times
	And OnAfterRetry will be dispatched exactly 4 times
	And no AggregateException is thrown

Scenario: Execute failing operation to succeed at 6 try
	Given I have entered a failing returnable operation which succeeds at 6 try
	And I have entered 100 milliseconds between
	When I attempt to run it
	Then exactly 5 retry should happen
	And took around 500 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And no AggregateException is thrown

Scenario: Execute failing operation to succeed at 7 try
	Given I have entered a failing returnable operation which succeeds at 7 try
	And I have entered 100 milliseconds between
	When I attempt to run it
	Then exactly 6 retry should happen
	And took around 600 milliseconds in total
	And OnFailure will be dispatched exactly 6 times
	And OnBeforeRetry will be dispatched exactly 6 times
	And OnAfterRetry will be dispatched exactly 6 times
	And no AggregateException is thrown

Scenario: Execute failing operation to succeed at 8 try
	Given I have entered a failing returnable operation which succeeds at 8 try
	And I have entered 100 milliseconds between
	When I attempt to run it
	Then exactly 7 retry should happen
	And took around 700 milliseconds in total
	And OnFailure will be dispatched exactly 7 times
	And OnBeforeRetry will be dispatched exactly 7 times
	And OnAfterRetry will be dispatched exactly 7 times
	And no AggregateException is thrown

Scenario: Execute failing operation to succeed at 9 try
	Given I have entered a failing returnable operation which succeeds at 9 try
	And I have entered 100 milliseconds between
	When I attempt to run it
	Then exactly 8 retry should happen
	And took around 800 milliseconds in total
	And OnFailure will be dispatched exactly 8 times
	And OnBeforeRetry will be dispatched exactly 8 times
	And OnAfterRetry will be dispatched exactly 8 times
	And no AggregateException is thrown

Scenario: Execute failing operation to succeed at 10 try
	Given I have entered a failing returnable operation which succeeds at 10 try
	And I have entered 100 milliseconds between
	When I attempt to run it
	Then exactly 9 retry should happen
	And took around 900 milliseconds in total
	And OnFailure will be dispatched exactly 9 times
	And OnBeforeRetry will be dispatched exactly 9 times
	And OnAfterRetry will be dispatched exactly 9 times
	And no AggregateException is thrown