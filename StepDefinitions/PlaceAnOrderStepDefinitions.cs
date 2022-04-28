using System;
using TechTalk.SpecFlow;

using static Project.Utilities.TestBase;

//turns out can't use wildcard to use multiple namespaces like in java so here's all the POMs
using Project.POMs.TopNav;

namespace Project.StepDefinitions
{
    [Binding]
    public class PlaceAnOrderStepDefinitions
    {

        [Given(@"I am logged in")]
        public void GivenIAmLoggedInUsingTheDetailsInFile()
        {

            topNav.Account.Click();

            //string loginDetails = @"../../../project login deets.txt";

            account.Login();
        }

        [Given(@"I have item '([0-9]{1,2})' in the cart")]
        public void GivenIHaveItemInTheCart(int itemNum)
        {
            topNav.Shop.Click();
            shop.ClickBuyItem(1);
        }

        [Given(@"I am on the cart page")]
        public void GivenIAmOnTheCartPage()
        {
            TopNav topNav = new TopNav(driver);

            topNav.ScrollToTopNav();
            topNav.Cart.Click();
        }

        [When(@"I apply the coupon '([^']*)'")]
        public void WhenIApplyTheCoupon(string couponCode)
        {
            cart.EnterCoupon(couponCode);
        }

        [Then(@"'([0-9]{1,2})'% discount is applied")]
        public void ThenDiscountIsApplied(int discount)
        {
            cart.SetDiscount(discount);
            cart.CheckDiscountIsCorrect();
            topNav.Cart.Click();
            cart.RemoveItem();

        }

        [Then(@"the correct price is displayed")]
        public void ThenTheCorrectPriceIsDisplayed()
        {
            cart.CheckTotalPriceIsCorrect();
        }

        [When(@"I place an order")]
        public void WhenIPlaceAnOrder()
        {
            topNav.Checkout.Click();
            //string checkoutDetails = @"../../../project checkout deets.txt";
            checkout.EnterDetails();
            checkout.PlaceOrder();
        }

        [Then(@"the order can be found on the account")]
        public void ThenTheOrderCanBeFoundOnTheAccount()
        {
            string orderNum = checkout.GetOrderNumber();
            topNav.Account.Click();
            account.Orders.Click();
            account.FindOrder(orderNum);
        }

    }
}