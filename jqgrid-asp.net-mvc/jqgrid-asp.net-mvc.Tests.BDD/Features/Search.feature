Feature: Search
	In order to search records at the System
	As a user
	I want to search records via jqGrid

@mytag
Scenario: Add two numbers
	Given I have entered 50 into the calculator
	And I have entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen
