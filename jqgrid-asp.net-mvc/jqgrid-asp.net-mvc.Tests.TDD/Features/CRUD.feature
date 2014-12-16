Feature: CRUD
	In order to CRUD with the System
	As a user
	I want to CRUD record via jqGrid

@CRUD@Read@Positive
Scenario:  read record	
	When I read records	via jqGrid invoking API
	Then then should get init records

@CRUD@Create@Positive
Scenario: create record
	When I create a record via jqGrid invoking API
	Then the record should be listed at the result with reading record

#@CRUD@Update@Positive
#Scenario: update record
#	Given I am at jqGrid page
#	When I press plus button at jqGrid
#	And Create a new test record
#	And submit
#	Then the added test record will be shown at jqGrid
#
#@CRUD@Delete@Positive
#Scenario: delete record
#	Given I am at jqGrid page
#	When I press plus button at jqGrid
#	And Create a new test record
#	And submit
#	Then the added test record will be shown at jqGrid