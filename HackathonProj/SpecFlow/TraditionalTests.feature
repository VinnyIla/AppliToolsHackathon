@Hackathon @Traditional
Feature: TraditionalTests

Scenario: 1. Login Page UI Elements
	Given Home Screen Displayed
	Then Verify Login Page UI elements
	
Scenario: 2. Data Driven Test
	Then Home Screen Displayed
	Then Verify Login functionality

Scenario: 3. Table Sort Test
	Given Home Screen Displayed
	Then Login
	And In recent transaction, Sort Amounts column and verify

Scenario: 4. Canvas Chart Test
	Given Home Screen Displayed
	Then Login
	And Click Compare Expenses and Verify Chart

Scenario: 5. Dynamic Content Test
	Given Home Screen Displayed
	Then Go to the url with ad true
	Then Login
	And Verify 2 adv gifs