@Cancellation
Feature: Cancellable retries
	In order to test cancellation rules
	As using a finite algorithm
	I want to be verify the number of retries and time took when cancellation rules applied

# Actions
# Cancels finite
Scenario: Cancel finite action on ArgumentException
	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on ArgumentException cancellation policy and attempt to run operation
	Then exactly 1 retry should happen
	And took around 100 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

Scenario: Cancel finite action on ArgumentOutOfRangeException
	Given I have entered a failing non-returnable operation which fails at step 3 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on ArgumentOutOfRangeException cancellation policy and attempt to run operation
	Then exactly 2 retry should happen
	And took around 200 milliseconds in total
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And AggregateException will be thrown with exactly 3 inner exceptions

Scenario: Cancel finite action on NullReferenceException
	Given I have entered a failing non-returnable operation which fails at step 4 with NullReferenceException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on NullReferenceException cancellation policy and attempt to run operation
	Then exactly 3 retry should happen
	And took around 300 milliseconds in total
	And OnFailure will be dispatched exactly 4 times
	And OnBeforeRetry will be dispatched exactly 4 times
	And OnAfterRetry will be dispatched exactly 4 times
	And AggregateException will be thrown with exactly 4 inner exceptions

Scenario: Cancel finite action on Exception
	Given I have entered a failing non-returnable operation which fails at step 2 with Exception and all others are ArgumentException
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on Exception cancellation policy and attempt to run operation
	Then exactly 1 retry should happen
	And took around 100 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

# Does not cancel finite
Scenario: Does not cancel finite action on ArgumentException
	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on ArgumentException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 400 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel finite action on ArgumentOutOfRangeException
	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on ArgumentOutOfRangeException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 400 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel finite action on NullReferenceException
	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on NullReferenceException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 400 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel finite action on Exception
	Given I have entered a failing non-returnable operation which fails at step 2 with NullReferenceException and all others are ArgumentException
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on Exception cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 400 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

# Cancels forever
Scenario: Cancel forever action on ArgumentException
	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentException and all others are Exception
	And I have entered 100 milliseconds between
	When I setup up to fail on ArgumentException cancellation policy and attempt to run operation
	Then exactly 1 retry should happen
	And took around 100 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

Scenario: Cancel forever action on ArgumentOutOfRangeException
	Given I have entered a failing non-returnable operation which fails at step 3 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 100 milliseconds between
	When I setup up to fail on ArgumentOutOfRangeException cancellation policy and attempt to run operation
	Then exactly 2 retry should happen
	And took around 200 milliseconds in total
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And AggregateException will be thrown with exactly 3 inner exceptions

Scenario: Cancel forever action on NullReferenceException
	Given I have entered a failing non-returnable operation which fails at step 4 with NullReferenceException and all others are Exception
	And I have entered 100 milliseconds between
	When I setup up to fail on NullReferenceException cancellation policy and attempt to run operation
	Then exactly 3 retry should happen
	And took around 300 milliseconds in total
	And OnFailure will be dispatched exactly 4 times
	And OnBeforeRetry will be dispatched exactly 4 times
	And OnAfterRetry will be dispatched exactly 4 times
	And AggregateException will be thrown with exactly 4 inner exceptions

Scenario: Cancel forever action on Exception
	Given I have entered a failing non-returnable operation which fails at step 2 with Exception and all others are ArgumentException
	And I have entered 100 milliseconds between
	When I setup up to fail on Exception cancellation policy and attempt to run operation
	Then exactly 1 retry should happen
	And took around 100 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

# TODO: Tests
# Does not cancel forever
Scenario: Does not cancel forever action on ArgumentException
	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 100 milliseconds between
	And stops after 500 milliseconds
	When I setup up to fail on ArgumentException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 500 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel forever action on ArgumentOutOfRangeException
	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentException and all others are Exception
	And I have entered 100 milliseconds between
	And stops after 500 milliseconds
	When I setup up to fail on ArgumentOutOfRangeException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 500 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel forever action on NullReferenceException
	Given I have entered a failing non-returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 100 milliseconds between
	And stops after 500 milliseconds
	When I setup up to fail on NullReferenceException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 500 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel forever action on Exception
	Given I have entered a failing non-returnable operation which fails at step 2 with NullReferenceException and all others are ArgumentException
	And I have entered 100 milliseconds between
	And stops after 500 milliseconds
	When I setup up to fail on Exception cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 500 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

# Cancels finite
Scenario: Cancel finite action after 200 milliseconds
	Given I have entered a failing non-returnable operation
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail after 200 milliseconds cancellation policy and attempt to run operation
	Then exactly 2 retry should happen
	And took around 200 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

Scenario: Cancel finite action after 600 milliseconds
	Given I have entered a failing non-returnable operation
	And I have entered 15 attempts for 200 milliseconds between
	When I setup up to fail after 600 milliseconds cancellation policy and attempt to run operation
	Then exactly 3 retry should happen
	And took around 600 milliseconds in total
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And AggregateException will be thrown with exactly 3 inner exceptions

Scenario: Cancel finite action after 1500 milliseconds
	Given I have entered a failing non-returnable operation
	And I have entered 15 attempts for 200 milliseconds between
	When I setup up to fail after 1500 milliseconds cancellation policy and attempt to run operation
	Then exactly 8 retry should happen
	And took around 1600 milliseconds in total
	And OnFailure will be dispatched exactly 8 times
	And OnBeforeRetry will be dispatched exactly 8 times
	And OnAfterRetry will be dispatched exactly 8 times
	And AggregateException will be thrown with exactly 8 inner exceptions

Scenario: Cancel finite action after 3000 milliseconds
	Given I have entered a failing non-returnable operation
	And I have entered 15 attempts for 430 milliseconds between
	When I setup up to fail after 3000 milliseconds cancellation policy and attempt to run operation
	Then exactly 7 retry should happen
	And took around 3010 milliseconds in total
	And OnFailure will be dispatched exactly 7 times
	And OnBeforeRetry will be dispatched exactly 7 times
	And OnAfterRetry will be dispatched exactly 7 times
	And AggregateException will be thrown with exactly 7 inner exceptions

Scenario: Cancel finite action after 5000 milliseconds
	Given I have entered a failing non-returnable operation
	And I have entered 15 attempts for 600 milliseconds between
	When I setup up to fail after 5000 milliseconds cancellation policy and attempt to run operation
	Then exactly 9 retry should happen
	And took around 5400 milliseconds in total
	And OnFailure will be dispatched exactly 9 times
	And OnBeforeRetry will be dispatched exactly 9 times
	And OnAfterRetry will be dispatched exactly 9 times
	And AggregateException will be thrown with exactly 9 inner exceptions

# Cancels forever - after 200,600,1500,3000,5000 milliseconds
Scenario: Cancel forever action after 200 milliseconds
	Given I have entered a failing non-returnable operation
	And I have entered 100 milliseconds between
	When I setup up to fail after 200 milliseconds cancellation policy and attempt to run operation
	Then exactly 2 retry should happen
	And took around 200 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

Scenario: Cancel forever action after 600 milliseconds
	Given I have entered a failing non-returnable operation
	And I have entered 200 milliseconds between
	When I setup up to fail after 600 milliseconds cancellation policy and attempt to run operation
	Then exactly 3 retry should happen
	And took around 600 milliseconds in total
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And AggregateException will be thrown with exactly 3 inner exceptions

Scenario: Cancel forever action after 1500 milliseconds
	Given I have entered a failing non-returnable operation
	And I have entered 200 milliseconds between
	When I setup up to fail after 1500 milliseconds cancellation policy and attempt to run operation
	Then exactly 8 retry should happen
	And took around 1600 milliseconds in total
	And OnFailure will be dispatched exactly 8 times
	And OnBeforeRetry will be dispatched exactly 8 times
	And OnAfterRetry will be dispatched exactly 8 times
	And AggregateException will be thrown with exactly 8 inner exceptions

Scenario: Cancel forever action after 3000 milliseconds
	Given I have entered a failing non-returnable operation
	And I have entered 430 milliseconds between
	When I setup up to fail after 3000 milliseconds cancellation policy and attempt to run operation
	Then exactly 7 retry should happen
	And took around 3010 milliseconds in total
	And OnFailure will be dispatched exactly 7 times
	And OnBeforeRetry will be dispatched exactly 7 times
	And OnAfterRetry will be dispatched exactly 7 times
	And AggregateException will be thrown with exactly 7 inner exceptions

Scenario: Cancel forever action after 5000 milliseconds
	Given I have entered a failing non-returnable operation
	And I have entered 600 milliseconds between
	When I setup up to fail after 5000 milliseconds cancellation policy and attempt to run operation
	Then exactly 9 retry should happen
	And took around 5400 milliseconds in total
	And OnFailure will be dispatched exactly 9 times
	And OnBeforeRetry will be dispatched exactly 9 times
	And OnAfterRetry will be dispatched exactly 9 times
	And AggregateException will be thrown with exactly 9 inner exceptions

# Functions
# Cancels finite
Scenario: Cancel finite function on ArgumentException
	Given I have entered a failing returnable operation which fails at step 2 with ArgumentException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on ArgumentException cancellation policy and attempt to run operation
	Then exactly 1 retry should happen
	And took around 100 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

Scenario: Cancel finite function on ArgumentOutOfRangeException
	Given I have entered a failing returnable operation which fails at step 3 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on ArgumentOutOfRangeException cancellation policy and attempt to run operation
	Then exactly 2 retry should happen
	And took around 200 milliseconds in total
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And AggregateException will be thrown with exactly 3 inner exceptions

Scenario: Cancel finite function on NullReferenceException
	Given I have entered a failing returnable operation which fails at step 4 with NullReferenceException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on NullReferenceException cancellation policy and attempt to run operation
	Then exactly 3 retry should happen
	And took around 300 milliseconds in total
	And OnFailure will be dispatched exactly 4 times
	And OnBeforeRetry will be dispatched exactly 4 times
	And OnAfterRetry will be dispatched exactly 4 times
	And AggregateException will be thrown with exactly 4 inner exceptions

Scenario: Cancel finite function on Exception
	Given I have entered a failing returnable operation which fails at step 2 with Exception and all others are ArgumentException
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on Exception cancellation policy and attempt to run operation
	Then exactly 1 retry should happen
	And took around 100 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

# Does not cancel finite
Scenario: Does not cancel finite function on ArgumentException
	Given I have entered a failing returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on ArgumentException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 400 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel finite function on ArgumentOutOfRangeException
	Given I have entered a failing returnable operation which fails at step 2 with ArgumentException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on ArgumentOutOfRangeException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 400 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel finite function on NullReferenceException
	Given I have entered a failing returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on NullReferenceException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 400 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel finite function on Exception
	Given I have entered a failing returnable operation which fails at step 2 with NullReferenceException and all others are ArgumentException
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail on Exception cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 400 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

# Cancels forever
Scenario: Cancel forever function on ArgumentException
	Given I have entered a failing returnable operation which fails at step 2 with ArgumentException and all others are Exception
	And I have entered 100 milliseconds between
	When I setup up to fail on ArgumentException cancellation policy and attempt to run operation
	Then exactly 1 retry should happen
	And took around 100 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

Scenario: Cancel forever function on ArgumentOutOfRangeException
	Given I have entered a failing returnable operation which fails at step 3 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 100 milliseconds between
	When I setup up to fail on ArgumentOutOfRangeException cancellation policy and attempt to run operation
	Then exactly 2 retry should happen
	And took around 200 milliseconds in total
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And AggregateException will be thrown with exactly 3 inner exceptions

Scenario: Cancel forever function on NullReferenceException
	Given I have entered a failing returnable operation which fails at step 4 with NullReferenceException and all others are Exception
	And I have entered 100 milliseconds between
	When I setup up to fail on NullReferenceException cancellation policy and attempt to run operation
	Then exactly 3 retry should happen
	And took around 300 milliseconds in total
	And OnFailure will be dispatched exactly 4 times
	And OnBeforeRetry will be dispatched exactly 4 times
	And OnAfterRetry will be dispatched exactly 4 times
	And AggregateException will be thrown with exactly 4 inner exceptions

Scenario: Cancel forever function on Exception
	Given I have entered a failing returnable operation which fails at step 2 with Exception and all others are ArgumentException
	And I have entered 100 milliseconds between
	When I setup up to fail on Exception cancellation policy and attempt to run operation
	Then exactly 1 retry should happen
	And took around 100 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

# TODO: Tests
# Does not cancel forever
Scenario: Does not cancel forever function on ArgumentException
	Given I have entered a failing returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 100 milliseconds between
	And stops after 500 milliseconds
	When I setup up to fail on ArgumentException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 500 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel forever function on ArgumentOutOfRangeException
	Given I have entered a failing returnable operation which fails at step 2 with ArgumentException and all others are Exception
	And I have entered 100 milliseconds between
	And stops after 500 milliseconds
	When I setup up to fail on ArgumentOutOfRangeException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 500 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel forever function on NullReferenceException
	Given I have entered a failing returnable operation which fails at step 2 with ArgumentOutOfRangeException and all others are Exception
	And I have entered 100 milliseconds between
	And stops after 500 milliseconds
	When I setup up to fail on NullReferenceException cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 500 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

Scenario: Does not cancel forever function on Exception
	Given I have entered a failing returnable operation which fails at step 2 with NullReferenceException and all others are ArgumentException
	And I have entered 100 milliseconds between
	And stops after 500 milliseconds
	When I setup up to fail on Exception cancellation policy and attempt to run operation
	Then exactly 5 retry should happen
	And took around 500 milliseconds in total
	And OnFailure will be dispatched exactly 5 times
	And OnBeforeRetry will be dispatched exactly 5 times
	And OnAfterRetry will be dispatched exactly 5 times
	And AggregateException will be thrown with exactly 5 inner exceptions

# Cancels finite - after 200,600,1500,3000,5000 milliseconds
Scenario: Cancel finite function after 200 milliseconds
	Given I have entered a failing returnable operation
	And I have entered 5 attempts for 100 milliseconds between
	When I setup up to fail after 200 milliseconds cancellation policy and attempt to run operation
	Then exactly 2 retry should happen
	And took around 200 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

Scenario: Cancel finite function after 600 milliseconds
	Given I have entered a failing returnable operation
	And I have entered 15 attempts for 200 milliseconds between
	When I setup up to fail after 600 milliseconds cancellation policy and attempt to run operation
	Then exactly 3 retry should happen
	And took around 600 milliseconds in total
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And AggregateException will be thrown with exactly 3 inner exceptions

Scenario: Cancel finite function after 1500 milliseconds
	Given I have entered a failing returnable operation
	And I have entered 15 attempts for 200 milliseconds between
	When I setup up to fail after 1500 milliseconds cancellation policy and attempt to run operation
	Then exactly 8 retry should happen
	And took around 1600 milliseconds in total
	And OnFailure will be dispatched exactly 8 times
	And OnBeforeRetry will be dispatched exactly 8 times
	And OnAfterRetry will be dispatched exactly 8 times
	And AggregateException will be thrown with exactly 8 inner exceptions

Scenario: Cancel finite function after 3000 milliseconds
	Given I have entered a failing returnable operation
	And I have entered 15 attempts for 430 milliseconds between
	When I setup up to fail after 3000 milliseconds cancellation policy and attempt to run operation
	Then exactly 7 retry should happen
	And took around 3010 milliseconds in total
	And OnFailure will be dispatched exactly 7 times
	And OnBeforeRetry will be dispatched exactly 7 times
	And OnAfterRetry will be dispatched exactly 7 times
	And AggregateException will be thrown with exactly 7 inner exceptions

Scenario: Cancel finite function after 5000 milliseconds
	Given I have entered a failing returnable operation
	And I have entered 15 attempts for 600 milliseconds between
	When I setup up to fail after 5000 milliseconds cancellation policy and attempt to run operation
	Then exactly 9 retry should happen
	And took around 5400 milliseconds in total
	And OnFailure will be dispatched exactly 9 times
	And OnBeforeRetry will be dispatched exactly 9 times
	And OnAfterRetry will be dispatched exactly 9 times
	And AggregateException will be thrown with exactly 9 inner exceptions

# Cancels forever
Scenario: Cancel forever function after 200 milliseconds
	Given I have entered a failing returnable operation
	And I have entered 100 milliseconds between
	When I setup up to fail after 200 milliseconds cancellation policy and attempt to run operation
	Then exactly 2 retry should happen
	And took around 200 milliseconds in total
	And OnFailure will be dispatched exactly 2 times
	And OnBeforeRetry will be dispatched exactly 2 times
	And OnAfterRetry will be dispatched exactly 2 times
	And AggregateException will be thrown with exactly 2 inner exceptions

Scenario: Cancel forever function after 600 milliseconds
	Given I have entered a failing returnable operation
	And I have entered 200 milliseconds between
	When I setup up to fail after 600 milliseconds cancellation policy and attempt to run operation
	Then exactly 3 retry should happen
	And took around 600 milliseconds in total
	And OnFailure will be dispatched exactly 3 times
	And OnBeforeRetry will be dispatched exactly 3 times
	And OnAfterRetry will be dispatched exactly 3 times
	And AggregateException will be thrown with exactly 3 inner exceptions

Scenario: Cancel forever function after 1500 milliseconds
	Given I have entered a failing returnable operation
	And I have entered 200 milliseconds between
	When I setup up to fail after 1500 milliseconds cancellation policy and attempt to run operation
	Then exactly 8 retry should happen
	And took around 1600 milliseconds in total
	And OnFailure will be dispatched exactly 8 times
	And OnBeforeRetry will be dispatched exactly 8 times
	And OnAfterRetry will be dispatched exactly 8 times
	And AggregateException will be thrown with exactly 8 inner exceptions

Scenario: Cancel forever function after 3000 milliseconds
	Given I have entered a failing returnable operation
	And I have entered 430 milliseconds between
	When I setup up to fail after 3000 milliseconds cancellation policy and attempt to run operation
	Then exactly 7 retry should happen
	And took around 3010 milliseconds in total
	And OnFailure will be dispatched exactly 7 times
	And OnBeforeRetry will be dispatched exactly 7 times
	And OnAfterRetry will be dispatched exactly 7 times
	And AggregateException will be thrown with exactly 7 inner exceptions

Scenario: Cancel forever function after 5000 milliseconds
	Given I have entered a failing returnable operation
	And I have entered 600 milliseconds between
	When I setup up to fail after 5000 milliseconds cancellation policy and attempt to run operation
	Then exactly 9 retry should happen
	And took around 5400 milliseconds in total
	And OnFailure will be dispatched exactly 9 times
	And OnBeforeRetry will be dispatched exactly 9 times
	And OnAfterRetry will be dispatched exactly 9 times
	And AggregateException will be thrown with exactly 9 inner exceptions