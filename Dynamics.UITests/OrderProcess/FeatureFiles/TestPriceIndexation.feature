@S276c408f
Feature: TestPriceIndexation
Feature for planning price indexation

@Tdef58d0e
Scenario: As user I want the new renewal period is visible when I click the add renewal period button,
	error message is raised when first renewal is not filled or there is a gap among renewals
	and I am able to save order when contracted values are filled with no gap and first renewal is filled
	Given a user is on the orders page
	And a user creates a new order
	And the order data is set
		| Name        | BillingOffice   | Currency   | Pricelevel   | Customer   | DeliveryContact   | Purchaser   |
		| <OrderName> | <BillingOffice> | <Currency> | <Pricelevel> | <Customer> | <DeliveryContact> | <Purchaser> |
	And the user saves the order
	And the order has an order product with <ExistingProduct>
	And user closes entity going back to order with saving
	And the order data is set
		| PriceIndexationType        |
		| <OrderPriceIndexationType> |
	And Input for first renewal is present
	And new second renewal button is present
	When user clicks on second renewal button
	Then Input for second renewal is present
	And new third renewal button is present
	When the user saves the order
	Then error message for contracted control is raised "Please enter valid data. '1st Renewal' cannot be empty."
	And warning notification for contracted control is raised "Price Indexation Data : Please enter valid data. '1st Renewal' cannot be empty."
	When user clicks on third renewal button
	Then input for third renewal is present
	Given user fills first renewal 1000
	And user fills third renewal 3000
	When the user saves the order
	Then error message for contracted control is raised "Please enter valid data. There cannot be gaps between the renewal prices."
	And warning notification for contracted control is raised "Price Indexation Data : Please enter valid data. There cannot be gaps between the renewal prices."
	Given user fills second renewal 2000
	When user saves and closes the order
	Then order is saved
	When user removes second renewal
	Then only first and second renewal input is displayed
	And price of second renewal is 3000
	When user removes second renewal
	And user removes first renewal
	Then Input for first renewal is present
	Given user fills first renewal 1000
	When the user saves the order
	And the user processes the order
	Then first renewal input is read only

	Examples:
		| OrderName                    | BillingOffice                                     | Currency | Pricelevel               | Customer   | DeliveryContact | Purchaser  | ExistingProduct                                          | OrderPriceIndexationType |
		| ContractedUIControlTest_TC01 | Kentico software CZ s.r.o. - International Office | CZK      | Kentico CZK - 2023/07/01 | test-order | JayV            | test-order | Business - 1 Website Auto-Scalable Subscription (Yearly) | Contracted               |

@Tbaacea5c
Scenario: As user I am not able to put negative values to contracted and percentage rule control
	Given a user is on the orders page
	And a user creates a new order
	And the order data is set
		| Name        | BillingOffice   | Currency   | Pricelevel   | Customer   | DeliveryContact   | Purchaser   |
		| <OrderName> | <BillingOffice> | <Currency> | <Pricelevel> | <Customer> | <DeliveryContact> | <Purchaser> |
	And the user saves the order
	And the order has an order product with <ExistingProduct>
	And user closes entity going back to order with saving
	And the order data is set
		| PriceIndexationType |
		| Contracted          |
	And user fills first renewal -1000
	When the user saves the order
	Then price of first renewal is 1000
	Given the order data is set
		| PriceIndexationType |
		| Percentage rule     |
	And renewal base price is set 100000000
	And maximum percentage is set 125
	Then input overflow validation is displayed in count 2
	Given user clear renewal base price
	And renewal base price is set -1000
	And user clear maximum percentage
	And maximum percentage is set -10
	When the user saves the order
	Then renewal base price is 1000
	And maximum percentage rule is 10

	Examples:
		| OrderName                                 | BillingOffice                                     | Currency | Pricelevel               | Customer   | DeliveryContact | Purchaser  | ExistingProduct                                          |
		| ContractedAndPercentageUIControlTest_TC02 | Kentico software CZ s.r.o. - International Office | CZK      | Kentico CZK - 2023/07/01 | test-order | JayV            | test-order | Business - 1 Website Auto-Scalable Subscription (Yearly) |