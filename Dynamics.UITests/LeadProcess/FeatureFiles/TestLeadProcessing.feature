@Sf6cc4178
Feature: TestLeadProcessing

This feature file covers tests for lead critical and high ev. medium priority features for lead entity

@T9a2768dc
Scenario Outline: As user I want the links for kentico, klenty are loaded properly
	Given user is in the leads page
	When user open lead with Id "<leadID>"
	Then links are loaded properly
	And links are not broken after every from 2 page loads

	Examples:
		|leadID|
		|eb68fe86-3821-ec11-b6e5-000d3addf874|


