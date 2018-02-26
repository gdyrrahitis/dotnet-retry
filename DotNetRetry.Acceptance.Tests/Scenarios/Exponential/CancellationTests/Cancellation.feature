@Cancellation
Feature: Cancellable retries
	In order to test cancellation rules
	As using an exponential algorithm
	I want to be verify the number of retries and time took when cancellation rules applied

# Actions
# Cancels finite
Scenario: Cancel finite exponential action on ArgumentException
	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on ArgumentException for exponential cancellation policy and attempt to run operation
	Then exactly 1 retry should happen
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

#Scenario: Cancel finite exponential action on ArgumentOutOfRangeException
#	Given I have entered a failing non-returnable operation which fails at step 3 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on ArgumentOutOfRangeException for exponential cancellation policy and attempt to run operation
#	Then exactly 2 retry should happen
#	And took around 200 milliseconds in total
#	And OnFailure will be dispatched exactly 3 times
#	And OnBeforeRetry will be dispatched exactly 3 times
#	And OnAfterRetry will be dispatched exactly 3 times
#	And AggregateException will be thrown with exactly 3 inner exceptions
#
#Scenario: Cancel finite exponential action on NullReferenceException
#	Given I have entered a failing non-returnable operation which fails at step 4 with NullReferenceException and all others are Exception
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on NullReferenceException for exponential cancellation policy and attempt to run operation
#	Then exactly 3 retry should happen
#	And took around 300 milliseconds in total
#	And OnFailure will be dispatched exactly 4 times
#	And OnBeforeRetry will be dispatched exactly 4 times
#	And OnAfterRetry will be dispatched exactly 4 times
#	And AggregateException will be thrown with exactly 4 inner exceptions
#
#Scenario: Cancel exponential action on Exception
#	Given I have entered a failing non-returnable operation which fails at step 2 with Exception and all others are ArgumentException
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on Exception for exponential cancellation policy and attempt to run operation
#	Then exactly 1 retry should happen
#	And took around 100 milliseconds in total
#	And OnFailure will be dispatched exactly 2 times
#	And OnBeforeRetry will be dispatched exactly 2 times
#	And OnAfterRetry will be dispatched exactly 2 times
#	And AggregateException will be thrown with exactly 2 inner exceptions
#
## Does not cancel finite exponential
#Scenario: Does not cancel finite exponential action on ArgumentException
#	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on ArgumentException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 400 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel finite exponential action on ArgumentOutOfRangeException
#	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentException and all others are Exception
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on ArgumentOutOfRangeException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 400 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel finite exponential action on NullReferenceException
#	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on NullReferenceException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 400 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel finite exponential action on Exception
#	Given I have entered a failing non-returnable operation which fails at step 2 with NullReferenceException and all others are ArgumentException
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on Exception for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 400 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
## Cancels forever exponential
#Scenario: Cancel forever exponential action on ArgumentException
#	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentException and all others are Exception
#	And I have entered 100 milliseconds between
#	When I setup up to fail on ArgumentException for exponential cancellation policy and attempt to run operation
#	Then exactly 1 retry should happen
#	And took around 100 milliseconds in total
#	And OnFailure will be dispatched exactly 2 times
#	And OnBeforeRetry will be dispatched exactly 2 times
#	And OnAfterRetry will be dispatched exactly 2 times
#	And AggregateException will be thrown with exactly 2 inner exceptions
#
#Scenario: Cancel forever exponential action on ArgumentOutOfRangeException
#	Given I have entered a failing non-returnable operation which fails at step 3 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 100 milliseconds between
#	When I setup up to fail on ArgumentOutOfRangeException for exponential cancellation policy and attempt to run operation
#	Then exactly 2 retry should happen
#	And took around 200 milliseconds in total
#	And OnFailure will be dispatched exactly 3 times
#	And OnBeforeRetry will be dispatched exactly 3 times
#	And OnAfterRetry will be dispatched exactly 3 times
#	And AggregateException will be thrown with exactly 3 inner exceptions
#
#Scenario: Cancel forever exponential action on NullReferenceException
#	Given I have entered a failing non-returnable operation which fails at step 4 with NullReferenceException and all others are Exception
#	And I have entered 100 milliseconds between
#	When I setup up to fail on NullReferenceException for exponential cancellation policy and attempt to run operation
#	Then exactly 3 retry should happen
#	And took around 300 milliseconds in total
#	And OnFailure will be dispatched exactly 4 times
#	And OnBeforeRetry will be dispatched exactly 4 times
#	And OnAfterRetry will be dispatched exactly 4 times
#	And AggregateException will be thrown with exactly 4 inner exceptions
#
#Scenario: Cancel forever exponential action on Exception
#	Given I have entered a failing non-returnable operation which fails at step 2 with Exception and all others are ArgumentException
#	And I have entered 100 milliseconds between
#	When I setup up to fail on Exception for exponential cancellation policy and attempt to run operation
#	Then exactly 1 retry should happen
#	And took around 100 milliseconds in total
#	And OnFailure will be dispatched exactly 2 times
#	And OnBeforeRetry will be dispatched exactly 2 times
#	And OnAfterRetry will be dispatched exactly 2 times
#	And AggregateException will be thrown with exactly 2 inner exceptions
#
## TODO: Tests
## Does not cancel forever exponential
#Scenario: Does not cancel forever exponential action on ArgumentException
#	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 100 milliseconds between
#	And stops after 500 milliseconds
#	When I setup up to fail on ArgumentException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 500 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel forever exponential action on ArgumentOutOfRangeException
#	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentException and all others are Exception
#	And I have entered 100 milliseconds between
#	And stops after 500 milliseconds
#	When I setup up to fail on ArgumentOutOfRangeException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 500 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel forever exponential action on NullReferenceException
#	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 100 milliseconds between
#	And stops after 500 milliseconds
#	When I setup up to fail on NullReferenceException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 500 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel forever exponential action on Exception
#	Given I have entered a failing non-returnable operation which fails at step 2 with NullReferenceException and all others are ArgumentException
#	And I have entered 100 milliseconds between
#	And stops after 500 milliseconds
#	When I setup up to fail on Exception for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 500 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
## Cancels finite exponential
#Scenario: Cancel finite exponential action after 200 milliseconds
#	Given I have entered a failing non-returnable operation
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail after 200 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 2 retry should happen
#	And took around 200 milliseconds in total
#	And OnFailure will be dispatched exactly 2 times
#	And OnBeforeRetry will be dispatched exactly 2 times
#	And OnAfterRetry will be dispatched exactly 2 times
#	And AggregateException will be thrown with exactly 2 inner exceptions
#
#Scenario: Cancel finite exponential action after 600 milliseconds
#	Given I have entered a failing non-returnable operation
#	And I have entered 15 attempts for 200 milliseconds between
#	When I setup up to fail after 600 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 3 retry should happen
#	And took around 600 milliseconds in total
#	And OnFailure will be dispatched exactly 3 times
#	And OnBeforeRetry will be dispatched exactly 3 times
#	And OnAfterRetry will be dispatched exactly 3 times
#	And AggregateException will be thrown with exactly 3 inner exceptions
#
#Scenario: Cancel finite exponential action after 1500 milliseconds
#	Given I have entered a failing non-returnable operation
#	And I have entered 15 attempts for 200 milliseconds between
#	When I setup up to fail after 1500 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 8 retry should happen
#	And took around 1600 milliseconds in total
#	And OnFailure will be dispatched exactly 8 times
#	And OnBeforeRetry will be dispatched exactly 8 times
#	And OnAfterRetry will be dispatched exactly 8 times
#	And AggregateException will be thrown with exactly 8 inner exceptions
#
#Scenario: Cancel finite exponential action after 3000 milliseconds
#	Given I have entered a failing non-returnable operation
#	And I have entered 15 attempts for 430 milliseconds between
#	When I setup up to fail after 3000 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 7 retry should happen
#	And took around 3010 milliseconds in total
#	And OnFailure will be dispatched exactly 7 times
#	And OnBeforeRetry will be dispatched exactly 7 times
#	And OnAfterRetry will be dispatched exactly 7 times
#	And AggregateException will be thrown with exactly 7 inner exceptions
#
#Scenario: Cancel finite exponential action after 5000 milliseconds
#	Given I have entered a failing non-returnable operation
#	And I have entered 15 attempts for 600 milliseconds between
#	When I setup up to fail after 5000 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 9 retry should happen
#	And took around 5400 milliseconds in total
#	And OnFailure will be dispatched exactly 9 times
#	And OnBeforeRetry will be dispatched exactly 9 times
#	And OnAfterRetry will be dispatched exactly 9 times
#	And AggregateException will be thrown with exactly 9 inner exceptions
#
## Cancels forever exponential - after 200,600,1500,3000,5000 milliseconds
#Scenario: Cancel forever exponential action after 200 milliseconds
#	Given I have entered a failing non-returnable operation
#	And I have entered 100 milliseconds between
#	When I setup up to fail after 200 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 2 retry should happen
#	And took around 200 milliseconds in total
#	And OnFailure will be dispatched exactly 2 times
#	And OnBeforeRetry will be dispatched exactly 2 times
#	And OnAfterRetry will be dispatched exactly 2 times
#	And AggregateException will be thrown with exactly 2 inner exceptions
#
#Scenario: Cancel forever exponential action after 600 milliseconds
#	Given I have entered a failing non-returnable operation
#	And I have entered 200 milliseconds between
#	When I setup up to fail after 600 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 3 retry should happen
#	And took around 600 milliseconds in total
#	And OnFailure will be dispatched exactly 3 times
#	And OnBeforeRetry will be dispatched exactly 3 times
#	And OnAfterRetry will be dispatched exactly 3 times
#	And AggregateException will be thrown with exactly 3 inner exceptions
#
#Scenario: Cancel forever exponential action after 1500 milliseconds
#	Given I have entered a failing non-returnable operation
#	And I have entered 200 milliseconds between
#	When I setup up to fail after 1500 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 8 retry should happen
#	And took around 1600 milliseconds in total
#	And OnFailure will be dispatched exactly 8 times
#	And OnBeforeRetry will be dispatched exactly 8 times
#	And OnAfterRetry will be dispatched exactly 8 times
#	And AggregateException will be thrown with exactly 8 inner exceptions
#
#Scenario: Cancel forever exponential action after 3000 milliseconds
#	Given I have entered a failing non-returnable operation
#	And I have entered 430 milliseconds between
#	When I setup up to fail after 3000 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 7 retry should happen
#	And took around 3010 milliseconds in total
#	And OnFailure will be dispatched exactly 7 times
#	And OnBeforeRetry will be dispatched exactly 7 times
#	And OnAfterRetry will be dispatched exactly 7 times
#	And AggregateException will be thrown with exactly 7 inner exceptions
#
#Scenario: Cancel forever exponential action after 5000 milliseconds
#	Given I have entered a failing non-returnable operation
#	And I have entered 600 milliseconds between
#	When I setup up to fail after 5000 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 9 retry should happen
#	And took around 5400 milliseconds in total
#	And OnFailure will be dispatched exactly 9 times
#	And OnBeforeRetry will be dispatched exactly 9 times
#	And OnAfterRetry will be dispatched exactly 9 times
#	And AggregateException will be thrown with exactly 9 inner exceptions
#
## Functions
## Cancels finite exponential
#Scenario: Cancel finite exponential function on ArgumentException
#	Given I have entered a failing returnable operation which fails at step 2 with ArgumentException and all others are Exception
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on ArgumentException for exponential cancellation policy and attempt to run operation
#	Then exactly 1 retry should happen
#	And took around 100 milliseconds in total
#	And OnFailure will be dispatched exactly 2 times
#	And OnBeforeRetry will be dispatched exactly 2 times
#	And OnAfterRetry will be dispatched exactly 2 times
#	And AggregateException will be thrown with exactly 2 inner exceptions
#
#Scenario: Cancel finite exponential function on ArgumentOutOfRangeException
#	Given I have entered a failing returnable operation which fails at step 3 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on ArgumentOutOfRangeException for exponential cancellation policy and attempt to run operation
#	Then exactly 2 retry should happen
#	And took around 200 milliseconds in total
#	And OnFailure will be dispatched exactly 3 times
#	And OnBeforeRetry will be dispatched exactly 3 times
#	And OnAfterRetry will be dispatched exactly 3 times
#	And AggregateException will be thrown with exactly 3 inner exceptions
#
#Scenario: Cancel finite exponential function on NullReferenceException
#	Given I have entered a failing returnable operation which fails at step 4 with NullReferenceException and all others are Exception
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on NullReferenceException for exponential cancellation policy and attempt to run operation
#	Then exactly 3 retry should happen
#	And took around 300 milliseconds in total
#	And OnFailure will be dispatched exactly 4 times
#	And OnBeforeRetry will be dispatched exactly 4 times
#	And OnAfterRetry will be dispatched exactly 4 times
#	And AggregateException will be thrown with exactly 4 inner exceptions
#
#Scenario: Cancel finite exponential function on Exception
#	Given I have entered a failing returnable operation which fails at step 2 with Exception and all others are ArgumentException
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on Exception for exponential cancellation policy and attempt to run operation
#	Then exactly 1 retry should happen
#	And took around 100 milliseconds in total
#	And OnFailure will be dispatched exactly 2 times
#	And OnBeforeRetry will be dispatched exactly 2 times
#	And OnAfterRetry will be dispatched exactly 2 times
#	And AggregateException will be thrown with exactly 2 inner exceptions
#
## Does not cancel finite exponential
#Scenario: Does not cancel finite exponential function on ArgumentException
#	Given I have entered a failing returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on ArgumentException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 400 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel finite exponential function on ArgumentOutOfRangeException
#	Given I have entered a failing returnable operation which fails at step 2 with ArgumentException and all others are Exception
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on ArgumentOutOfRangeException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 400 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel finite exponential function on NullReferenceException
#	Given I have entered a failing returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on NullReferenceException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 400 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel finite exponential function on Exception
#	Given I have entered a failing returnable operation which fails at step 2 with NullReferenceException and all others are ArgumentException
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail on Exception for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 400 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
## Cancels forever exponential
#Scenario: Cancel forever exponential function on ArgumentException
#	Given I have entered a failing returnable operation which fails at step 2 with ArgumentException and all others are Exception
#	And I have entered 100 milliseconds between
#	When I setup up to fail on ArgumentException for exponential cancellation policy and attempt to run operation
#	Then exactly 1 retry should happen
#	And took around 100 milliseconds in total
#	And OnFailure will be dispatched exactly 2 times
#	And OnBeforeRetry will be dispatched exactly 2 times
#	And OnAfterRetry will be dispatched exactly 2 times
#	And AggregateException will be thrown with exactly 2 inner exceptions
#
#Scenario: Cancel forever exponential function on ArgumentOutOfRangeException
#	Given I have entered a failing returnable operation which fails at step 3 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 100 milliseconds between
#	When I setup up to fail on ArgumentOutOfRangeException for exponential cancellation policy and attempt to run operation
#	Then exactly 2 retry should happen
#	And took around 200 milliseconds in total
#	And OnFailure will be dispatched exactly 3 times
#	And OnBeforeRetry will be dispatched exactly 3 times
#	And OnAfterRetry will be dispatched exactly 3 times
#	And AggregateException will be thrown with exactly 3 inner exceptions
#
#Scenario: Cancel forever exponential function on NullReferenceException
#	Given I have entered a failing returnable operation which fails at step 4 with NullReferenceException and all others are Exception
#	And I have entered 100 milliseconds between
#	When I setup up to fail on NullReferenceException for exponential cancellation policy and attempt to run operation
#	Then exactly 3 retry should happen
#	And took around 300 milliseconds in total
#	And OnFailure will be dispatched exactly 4 times
#	And OnBeforeRetry will be dispatched exactly 4 times
#	And OnAfterRetry will be dispatched exactly 4 times
#	And AggregateException will be thrown with exactly 4 inner exceptions
#
#Scenario: Cancel forever exponential function on Exception
#	Given I have entered a failing returnable operation which fails at step 2 with Exception and all others are ArgumentException
#	And I have entered 1500 milliseconds between
#	When I setup up to fail on Exception for exponential cancellation policy and attempt to run operation
#	Then exactly 1 retry should happen
#	And took around 1500 milliseconds in total
#	And OnFailure will be dispatched exactly 2 times
#	And OnBeforeRetry will be dispatched exactly 2 times
#	And OnAfterRetry will be dispatched exactly 2 times
#	And AggregateException will be thrown with exactly 2 inner exceptions
#
## TODO: Tests
## Does not cancel forever exponential
#Scenario: Does not cancel forever exponential function on ArgumentException
#	Given I have entered a failing returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 100 milliseconds between
#	And stops after 500 milliseconds
#	When I setup up to fail on ArgumentException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 500 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel forever exponential function on ArgumentOutOfRangeException
#	Given I have entered a failing returnable operation which fails at step 2 with ArgumentException and all others are Exception
#	And I have entered 100 milliseconds between
#	And stops after 500 milliseconds
#	When I setup up to fail on ArgumentOutOfRangeException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 500 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel forever exponential function on NullReferenceException
#	Given I have entered a failing returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
#	And I have entered 100 milliseconds between
#	And stops after 500 milliseconds
#	When I setup up to fail on NullReferenceException for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 500 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
#Scenario: Does not cancel forever exponential function on Exception
#	Given I have entered a failing returnable operation which fails at step 2 with NullReferenceException and all others are ArgumentException
#	And I have entered 100 milliseconds between
#	And stops after 500 milliseconds
#	When I setup up to fail on Exception for exponential cancellation policy and attempt to run operation
#	Then exactly 5 retry should happen
#	And took around 500 milliseconds in total
#	And OnFailure will be dispatched exactly 5 times
#	And OnBeforeRetry will be dispatched exactly 5 times
#	And OnAfterRetry will be dispatched exactly 5 times
#	And AggregateException will be thrown with exactly 5 inner exceptions
#
## Cancels finite exponential - after 200,600,1500,3000,5000 milliseconds
#Scenario: Cancel finite exponential function after 200 milliseconds
#	Given I have entered a failing returnable operation
#	And I have entered 5 attempts for 100 milliseconds between
#	When I setup up to fail after 200 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 2 retry should happen
#	And took around 200 milliseconds in total
#	And OnFailure will be dispatched exactly 2 times
#	And OnBeforeRetry will be dispatched exactly 2 times
#	And OnAfterRetry will be dispatched exactly 2 times
#	And AggregateException will be thrown with exactly 2 inner exceptions
#
#Scenario: Cancel finite exponential function after 600 milliseconds
#	Given I have entered a failing returnable operation
#	And I have entered 15 attempts for 200 milliseconds between
#	When I setup up to fail after 600 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 3 retry should happen
#	And took around 600 milliseconds in total
#	And OnFailure will be dispatched exactly 3 times
#	And OnBeforeRetry will be dispatched exactly 3 times
#	And OnAfterRetry will be dispatched exactly 3 times
#	And AggregateException will be thrown with exactly 3 inner exceptions
#
#Scenario: Cancel finite exponential function after 1500 milliseconds
#	Given I have entered a failing returnable operation
#	And I have entered 15 attempts for 200 milliseconds between
#	When I setup up to fail after 1500 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 8 retry should happen
#	And took around 1600 milliseconds in total
#	And OnFailure will be dispatched exactly 8 times
#	And OnBeforeRetry will be dispatched exactly 8 times
#	And OnAfterRetry will be dispatched exactly 8 times
#	And AggregateException will be thrown with exactly 8 inner exceptions
#
#Scenario: Cancel finite exponential function after 3000 milliseconds
#	Given I have entered a failing returnable operation
#	And I have entered 15 attempts for 430 milliseconds between
#	When I setup up to fail after 3000 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 7 retry should happen
#	And took around 3010 milliseconds in total
#	And OnFailure will be dispatched exactly 7 times
#	And OnBeforeRetry will be dispatched exactly 7 times
#	And OnAfterRetry will be dispatched exactly 7 times
#	And AggregateException will be thrown with exactly 7 inner exceptions
#
#Scenario: Cancel finite exponential function after 5000 milliseconds
#	Given I have entered a failing returnable operation
#	And I have entered 15 attempts for 600 milliseconds between
#	When I setup up to fail after 5000 millisecondsfor exponential  cancellation policy and attempt to run operation
#	Then exactly 9 retry should happen
#	And took around 5400 milliseconds in total
#	And OnFailure will be dispatched exactly 9 times
#	And OnBeforeRetry will be dispatched exactly 9 times
#	And OnAfterRetry will be dispatched exactly 9 times
#	And AggregateException will be thrown with exactly 9 inner exceptions
#
## Cancels forever exponential
#Scenario: Cancel forever exponential function after 200 milliseconds
#	Given I have entered a failing returnable operation
#	And I have entered 100 milliseconds between
#	When I setup up to fail after 200 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 2 retry should happen
#	And took around 200 milliseconds in total
#	And OnFailure will be dispatched exactly 2 times
#	And OnBeforeRetry will be dispatched exactly 2 times
#	And OnAfterRetry will be dispatched exactly 2 times
#	And AggregateException will be thrown with exactly 2 inner exceptions
#
#Scenario: Cancel forever exponential function after 600 milliseconds
#	Given I have entered a failing returnable operation
#	And I have entered 200 milliseconds between
#	When I setup up to fail after 600 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 3 retry should happen
#	And took around 600 milliseconds in total
#	And OnFailure will be dispatched exactly 3 times
#	And OnBeforeRetry will be dispatched exactly 3 times
#	And OnAfterRetry will be dispatched exactly 3 times
#	And AggregateException will be thrown with exactly 3 inner exceptions
#
#Scenario: Cancel forever exponential function after 1500 milliseconds
#	Given I have entered a failing returnable operation
#	And I have entered 200 milliseconds between
#	When I setup up to fail after 1500 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 8 retry should happen
#	And took around 1600 milliseconds in total
#	And OnFailure will be dispatched exactly 8 times
#	And OnBeforeRetry will be dispatched exactly 8 times
#	And OnAfterRetry will be dispatched exactly 8 times
#	And AggregateException will be thrown with exactly 8 inner exceptions
#
#Scenario: Cancel forever exponential function after 3000 milliseconds
#	Given I have entered a failing returnable operation
#	And I have entered 430 milliseconds between
#	When I setup up to fail after 3000 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 7 retry should happen
#	And took around 3010 milliseconds in total
#	And OnFailure will be dispatched exactly 7 times
#	And OnBeforeRetry will be dispatched exactly 7 times
#	And OnAfterRetry will be dispatched exactly 7 times
#	And AggregateException will be thrown with exactly 7 inner exceptions
#
#Scenario: Cancel forever exponential function after 5000 milliseconds
#	Given I have entered a failing returnable operation
#	And I have entered 600 milliseconds between
#	When I setup up to fail after 5000 milliseconds for exponential cancellation policy and attempt to run operation
#	Then exactly 9 retry should happen
#	And took around 5400 milliseconds in total
#	And OnFailure will be dispatched exactly 9 times
#	And OnBeforeRetry will be dispatched exactly 9 times
#	And OnAfterRetry will be dispatched exactly 9 times
#	And AggregateException will be thrown with exactly 9 inner exceptions