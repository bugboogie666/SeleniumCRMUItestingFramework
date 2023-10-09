@S84643d75
Feature: TestPandaDoc

Tests for contract management tool involving create of new business opportunity/order scenario and subscription switch

@Td3ac1a63
Scenario Outline: As user I am able to create contract for a new business so that, I create offer letter, let it sign and order with signed document is prepared to be invoiced
	Given user is in the accounts page
	And user creates new account
	And account data is set
		| AccountName   | Country   |
		| <AccountName> | <Country> |
	And user goes to "Details" tab
	And account data is set
		| Industry   |
		| <Industry> |
	And user sets account type
	And user saves account
	And user goes to "Summary" tab
	And user opens quick create contact from primary contact
	And quick create contact data is set
		| LastName   | FirstName   | Email   |
		| <LastName> | <FirstName> | <Email> |
	And user saves quick create
	And user saves account
	And primary contact is memorized as "contract contact"
	And user creates a new opportunity from account
	And opportunity data is set
		| Name               | Type   | Source   |
		| <OpportunityTopic> | <Type> | <Source> |
	And contact "contract contact" is set for opportunity
	And user saves opportunity
	And the opportunity has an order product with <OppExistingProduct>
	And user closes entity going back to opportunity with saving
	And owner for opportunity is changed to "svc-CRM-System@kentico.com"
	And user saves opportunity
	And user activates opportunity BPF stage "New"
	And user leaves stage "New"
	And opportunity data is set
		| OriginatingLeadBPF | EstimateCloseDateBPF |
		| John Underlord     | <NOW>                |
	And user closes stage "Mutual Interest"
	And user saves opportunity
	And user creates a new order from opportunity
	And the order data is set
		| PriceIndexationType    |
		| According to pricelist |
	And the user saves the order
	And user opens opportunity lookup
	And user creates an offer letter


	Examples:
		| AccountName | Country        | Industry              | LastName | Email    | FirstName | OpportunityTopic  | Type         | Source | OppExistingProduct                                       |
		| <RANDOM>    | Czech republic | Agriculture;Education | <RANDOM> | <RANDOM> | <RANDOM>  | Panda example opp | New Business | Direct | Business - 1 Website Auto-Scalable Subscription (Yearly) |

