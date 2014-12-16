Feature: Search
	In order to search record by properties
	As a user
	I want to be search record by properties

Scenario: Search by FirstName
	When I search by FirstName as 'Wei' via jqGrid invoking API
	Then the returned record should all contains 'Weimin' at FirstName
	And the record that  at FirstName is 'John' should NOT be listed at the result with reading record

Scenario: Search by LastName
	When I search by LastName as 'Y' via jqGrid invoking API
	Then the returned record should all contains 'Y' at LastName
	And the record that  at LastName is 'Smith' should NOT be listed at the result with reading record

Scenario: Search by City
	When I search by City as 'San' via jqGrid invoking API
	Then the returned record should all contains 'San' at City
	And the record that  at City is 'New York' should NOT be listed at the result with reading record

Scenario: Search by Zip
	When I search by Zip as '94' via jqGrid invoking API
	Then the returned record should all contains '94' at Zip
	And the record that  at Zip is '11111' should NOT be listed at the result with reading record

Scenario: Search by FirstName and LastName both
	When I search by FirstName as 'We' and LastName as 'Y' via jqGrid invoking API
	Then the returned record should all contains 'We' at FirstName and 'Y' at LastName
	And the record that  at FistName is 'Weimin' and LastName is 'Jobs' should NOT be listed at the result with reading record

Scenario: Search by FirstName and City both
	When I search by FirstName as 'We' and City as 'San' via jqGrid invoking API
	Then the returned record should all contains 'We' at City and 'San' at LastName
	And the record that  at FistName is 'Weimin' and City is 'Guang Zhou' should NOT be listed at the result with reading record

Scenario: Search by LastName and Zip both
	When I search by LastName as 'Y' and Zip as '94' via jqGrid invoking API
	Then the returned record should all contains 'Y' at LastName and '94' at Zip
	And the record that  at LastName is 'Ye' and Zip is '510000' should NOT be listed at the result with reading record

Scenario: Search by FirstName , LastName , and city both
	When I search by FirstName as 'We' and LastName as 'Y' and City as 'San' via jqGrid invoking API
	Then the returned record should all contains 'We' at FirstName and 'Y' at LastName and 'San' at City
	And the record that  at FistName is 'Weimin' and LastName is 'Jobs' should NOT be listed at the result with reading record

Scenario: Search by FirstName , LastName , city and zip both
	When I search by FirstName as 'We' and LastName as 'Y' and City as 'San' and Zip as '94' via jqGrid invoking API
	Then the returned record should all contains 'We' at FirstName and 'Y' at LastName and 'San' and '94' at Zip at City
	And the record that  at FistName is 'Weimin' and LastName is 'Jobs' should NOT be listed at the result with reading record
