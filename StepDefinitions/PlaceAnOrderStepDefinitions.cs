using System;
using TechTalk.SpecFlow;

using static Project.Utilities.TestBase;

//turns out can't use wildcard to use multiple namespaces like in java so heres all the POMs
using Project.POMs.TopNav;
using Project.POMs.Shop_POM;
using Project.POMs.Checkout_POM;
using Project.POMs.Cart_POM;
using Project.POMs.Account_POM;

//consider using environment variables

namespace Project.StepDefinitions
{
    [Binding]
    public class PlaceAnOrderStepDefinitions
    {

        [Given(@"I am logged in using the details in file '([^']*)'")]
        public void GivenIAmLoggedInUsingTheDetailsInFile(string filePath)
        {

            topNav.Account.Click();

            string loginDetails = @"../../../project login deets.txt";

            account.Login(loginDetails);
        }

        [Given(@"I have item '([0-9]{1,2})' in the cart")]
        public void GivenIHaveItemInTheCart(int itemNum)
        {

            topNav.Shop.Click();
            shop.ScrollTo(shop.GetItem(1));
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
            Console.WriteLine("expected price: " + cart.ExpectedTotal());
            Console.WriteLine("actual total: " + cart.GetActualTotal);
            cart.CheckDiscountIsCorrect();
            
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
            string checkoutDetails = @"../../../project checkout deets.txt";
            checkout.EnterDetails(checkoutDetails);
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