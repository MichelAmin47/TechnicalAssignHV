Feature: RegressionHomePage
	In order to book a flight with or without childeren
	As a user
	I want to have the option to select how many infants or babies while booking a flight

@mytag
Scenario: Book a flight for only an adult
	Given I am on the Transavia homepage
	When I select "Amsterdam" as my departure, I select "Nice" as my departure
	And I select a departure date "20" days in the future, I select the return date plus "3" days
	And I fly alone
	Then I should be taken to the "Boek een vlucht" page
	And I should be able to select a flight
