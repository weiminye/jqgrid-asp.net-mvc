Feature: CRUD
	In order to CRUD with the System
	As a user
	I want to CRUD record via jqGrid

@CRUD@Create@Positive
Scenario: create record
	Given I am at jqGrid page
	When I press plus button at jqGrid
	And input new record as below
	| City | FirstName | LastName | Zip   |
	| Napa | Weimin     | Ye        | 94112 |
	And submit
	Then the record will be shown at jqGrid
	| City | FirstName | LastName | Zip   |
	| Napa | Weimin     | Ye        | 94112 |