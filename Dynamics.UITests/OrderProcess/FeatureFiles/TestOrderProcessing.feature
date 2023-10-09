@S15a9d3d7
Feature: TestOrderProcessing
/// https://kentico.atlassian.net/browse/IN-936

@Td1dac3e4
Scenario Outline: As user I want an order containing new subscription product has properly set parameters for its product and for its license after processing
	Given a user is on the orders page
	And a user creates a new order
	And the order data is set
		| Name        | BillingOffice   | Currency   | Pricelevel   | Customer   | DeliveryContact   | Purchaser   |
		| <OrderName> | <BillingOffice> | <Currency> | <Pricelevel> | <Customer> | <DeliveryContact> | <Purchaser> |
	And the user saves the order
	And the order has an order product with <ExistingProduct>
	And the product data is set
		| ContractLength   | Description |
		| <ContractLength> | test        |
	And user closes entity going back to order with saving
	And the order data is set
		| PriceIndexationType        |
		| <OrderPriceIndexationType> |
	And the user saves the order
	When the user processes the order
	And user is going to order product 1
	Then product fields are properly set <ContractLengthExp>
	Given user closes entity going back to order with saving
	And a license should be bound to the order
	When the user opens the license lookup
	Then license parameters are properly set
		| Name             | Account             | Currency             | Product             | WithSourceCode             | LicenseType      |
		| <LicenseNameExp> | <LicenseAccountExp> | <LicenseCurrencyExp> | <LicenseProductExp> | <LicenseWithSourceCodeExp> | <LicenseTypeExp> |
	Given user closes entity going back to order with saving
	And user goes to related activities and select email 0
	Then email exists and it is a draft

	Examples:
		| OrderName                   | BillingOffice                                     | Currency | Pricelevel               | Customer   | DeliveryContact | Purchaser  | ExistingProduct                                          | OrderPriceIndexationType | ContractLength | ContractLengthExp | LicenseNameExp                                           | LicenseAccountExp | LicenseCurrencyExp | LicenseProductExp                                        | LicenseWithSourceCodeExp | LicenseTypeExp      |
		| NewSubscriptionLicense_TC01 | Kentico software CZ s.r.o. - International Office | CZK      | Kentico CZK - 2023/07/01 | test-order | JayV            | test-order | Business - 1 Website Auto-Scalable Subscription (Yearly) | According to pricelist   | 12.00          | 12                | Business - 1 Website Auto-Scalable Subscription (Yearly) | test-order        | CZK                | Business - 1 Website Auto-Scalable Subscription (Yearly) | False                    | SubscriptionLicense |
		| NewSubscriptionLicense_TC02 | Kentico software CZ s.r.o. - International Office | CZK      | Kentico CZK - 2023/07/01 | test-order | JayV            | test-order | Business - 1 Website Auto-Scalable Subscription (Yearly) | According to pricelist   | 36.00          | 36                | Business - 1 Website Auto-Scalable Subscription (Yearly) | test-order        | CZK                | Business - 1 Website Auto-Scalable Subscription (Yearly) | False                    | SubscriptionLicense |


@T99707a5f
Scenario Outline: As user I want the renewal is splitted to renewal and reinstatement when license expiration is before order processing, order processing is performed within the grace period
	Given a user is on the orders page
	And a user creates a new order
	And the order data is set
		| Name                        | BillingOffice   | Currency   | Pricelevel   | Customer                                | DeliveryContact	  | Purchaser                               |
		| TestReinstatement_T99707a5f | <BillingOffice> | <Currency> | <Pricelevel> | UITest_Corkery, Feeney and Bernier.knjz | Hoyt UITest_Stokes| UITest_Corkery, Feeney and Bernier.knjz |
	And the user saves the order
	And the order has an order product with <ExistingProduct>
	And the product data is set
		| ContractLength |
		| 12			 |
	And user closes entity going back to order with saving
	And the order data is set
		| PriceIndexationType   |
		| According to pricelist|
	And the user saves the order
	And the user processes the order
	And a license should be bound to the order
	And the user opens the license lookup
	And a form is switched to "License Office Form_UCI"
	And the license expires in <NumberOfdays> days
	And the user saves the license
	And the user creates a new order from the license
	And the order data is set
		| Name					 | BillingOffice   | Currency   | Pricelevel   | Customer								 | DeliveryContact   | Purchaser							  |
		| RenewalOrder_T99707a5f | <BillingOffice> | <Currency> | <Pricelevel> | UITest_Corkery, Feeney and Bernier.knjz | Hoyt UITest_Stokes| UITest_Corkery, Feeney and Bernier.knjz|
	And the user saves the order
	And the order has not saved an order product called <RenewalProduct>
	And user changes pricing to "true"
	And the product data is set
		| PricePerUnit          | ContractLength          |
		| <RenewalPricePerUnit> | <RenewalContractLength> |
	And user saves product
	And "extended amount" value for field "ExtendedAmount" is memorized
	And user closes entity going back to order with saving
	And the order data is set
		| PriceIndexationType   |
		| According to pricelist|
	And the user saves the order
	When the user processes the order
	Then renewal product is splitted to renewal and reinstatement
	And order detail amount is equal to original renewal "extended amount"

	Examples:
		| BillingOffice                                     | Currency | Pricelevel               | ExistingProduct                                          |RenewalContractLength | RenewalProduct                 | RenewalPricePerUnit | NumberOfdays|
		| Kentico software CZ s.r.o. - International Office | CZK      | Kentico CZK - 2023/07/01 | Business - 1 Website Auto-Scalable Subscription (Yearly) |12			        | Xperience Subscription Renewal | 10000               | -10		 |
		| Kentico software CZ s.r.o. - International Office | CZK      | Kentico CZK - 2023/07/01 | Business - 1 Website Auto-Scalable Subscription (Yearly) |7.19			        | Xperience Subscription Renewal | 7600                | -14		 |
		| Kentico software CZ s.r.o. - International Office | CZK      | Kentico CZK - 2023/07/01 | Business - 1 Website Auto-Scalable Subscription (Yearly) |24			        | Xperience Subscription Renewal | 22600               | -2			 |


@T7086cc1f
Scenario: As user I want the renewal product and license fields and email to be set correctly when a renewal order is processed before expiration
	Given a user is on the orders page
	And a user creates a new order
	And the order data is set
		| Name				| BillingOffice										| Currency   | Pricelevel				| Customer   | DeliveryContact   | Purchaser  |
		| Test_Renewal_Order| Kentico software CZ s.r.o. - International Office | CZK		 | Kentico CZK - 2023/07/01 | test-order | JayV				 | test-order |
	And the user saves the order
	And the order has an order product with Business - 1 Website Auto-Scalable Subscription (Yearly)
	And user closes entity going back to order with saving
	And the order data is set
		| PriceIndexationType    |
		| According to pricelist |
	And the user saves the order
	And the user processes the order
	And a license should be bound to the order
	And the user opens the license lookup
	And the original license expiration is memorized as key "original license expiration"
	And the user creates a new order from the license
	And the order data is set
		| Name				| BillingOffice										| Currency   | Pricelevel				| Customer   | DeliveryContact   | Purchaser  |
		| Test_Renewal_Order| Kentico software CZ s.r.o. - International Office | CZK		 | Kentico CZK - 2023/07/01 | test-order | JayV				 | test-order |
	And the user saves the order
	And the order has an order product with Xperience Subscription Renewal
	And the product data is set
		| ContractLength |
		| 24             |
	And user closes entity going back to order with saving
	And the order data is set
		| PriceIndexationType    |
		| According to pricelist |
	And the user saves the order
	And the user processes the order
	And user is going to order product 1
	And starts on is set correctly based on date of "original license expiration"
	And expires on is set correctly based on CL "24" and date of "original license expiration"
	And the renewal product expiration is memorized as "renewal product expiration"
	And user closes entity going back to order with saving
	And a license should be bound to the order
	And the user opens the license lookup
	And the license expiration is set correctly to the renewal product expiration
	And user closes entity going back to order with saving
	When user goes to related activities and select email 0
	Then email exists and it is sent


@Tb823f2b7
Scenario: As user I want the renewal product and license fields to be set correctly when a renewal order is processed after grace period
	Given a user is on the orders page
	And a user creates a new order
	And the order data is set
		| Name						| BillingOffice										| Currency   | Pricelevel				| Customer   | DeliveryContact   | Purchaser  |
		| Test_Renewal_Order_AfterGP| Kentico software CZ s.r.o. - International Office | CZK		 | Kentico CZK - 2023/07/01 | test-order | JayV				 | test-order |
	And the user saves the order
	And the order has an order product with Business - 1 Website Auto-Scalable Subscription (Yearly)
	And user closes entity going back to order with saving
	And the order data is set
		| PriceIndexationType    |
		| According to pricelist |
	And the user saves the order
	And the user processes the order
	And a license should be bound to the order
	And the user opens the license lookup
	And the license expires in -45 days
	And the user saves the license
	And the user creates a new order from the license
	And the order data is set
		| Name						| BillingOffice										| Currency   | Pricelevel				| Customer   | DeliveryContact   | Purchaser  |
		| Test_Renewal_Order_AfterGP| Kentico software CZ s.r.o. - International Office | CZK		 | Kentico CZK - 2023/07/01 | test-order | JayV				 | test-order |
	And the user saves the order
	And the order has an order product with Xperience Subscription Renewal
	And the product data is set
		| ContractLength |
		| 12             |
	And starts on and expires on fields are empty and not editable
	And user closes entity going back to order with saving
	And the order data is set
		| PriceIndexationType    |
		| According to pricelist |
	And the user saves the order
	And the user processes the order
	And the date of processing is memorized as "date fulfilled"
	When user is going to order product 1
	Then the order product starts on is set as date of fulfilled
	And user closes entity going back to order with saving