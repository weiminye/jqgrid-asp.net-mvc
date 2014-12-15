Feature: CRUD
	In order to CRUD with the System
	As a user
	I want to CRUD record via jqGrid

Scenario:  CRUD at UI
	Given I am at jqGrid page	
	When I press plus button at jqGrid
	And Create a new test record
	And Click the submit button
	Then the added test record will be shown at jqGrid	
	When Click the update button at the test record
#	And submit the update
#	Then the updated test record  will be shown at jqGrid with updated values
#	When Click the delete button at the test record
#	Then the deleted test record  will be not shown at jqGrid
#	And Edit with new value
