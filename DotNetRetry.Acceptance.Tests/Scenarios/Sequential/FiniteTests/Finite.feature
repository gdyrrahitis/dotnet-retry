Feature: Finite retries
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Execute operation errorless without retrying
	Given I have entered a successfull non-returnable operation
	And I have entered 3 attempts for 100 milliseconds between
	When I attempt to run it
	Then no retry attempts should be made
