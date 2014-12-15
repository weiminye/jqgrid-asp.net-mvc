Feature: CRUD
	In order to CRUD with the System
	As a user
	I want to CRUD record via jqGrid

@CRUD@Read@Positive
Scenario:  record
	Given I am at jqGrid page
	When I press plus button at jqGrid
	And Create a new test record
	And submit
	Then the added test record will be shown at jqGrid	

@CRUD@Create@Positive
Scenario: create record
	Given I am at jqGrid page
	When I press plus button at jqGrid
	And Create a new test record
	And submit
	Then the added test record will be shown at jqGrid	

@CRUD@Update@Positive
Scenario: update record
	Given I am at jqGrid page
	When I press plus button at jqGrid
	And Create a new test record
	And submit
	Then the added test record will be shown at jqGrid

@CRUD@Delete@Positive
Scenario: delete record
	Given I am at jqGrid page
	When I press plus button at jqGrid
	And Create a new test record
	And submit
	Then the added test record will be shown at jqGrid