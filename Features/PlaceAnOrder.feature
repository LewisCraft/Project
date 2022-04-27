Feature: PlaceAnOrder

A short summary of the feature

Background: 
	Given I am logged in using the details in file '../../../project login deets.txt'
	And I have item '1' in the cart
	And I am on the cart page

@Coupon
Scenario: Ensure coupon applies properly
	When I apply the coupon 'edgewords'
	Then '15'% discount is applied
	And the correct price is displayed

@Order
Scenario: Ensure the order has been placed
	When I place an order
	Then the order can be found on the account