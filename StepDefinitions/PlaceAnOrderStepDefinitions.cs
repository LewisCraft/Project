using System;
using TechTalk.SpecFlow;

using static Project.Utilities.TestBase;

//turns out can't use wildcard to use multiple namespaces like in java so here's all the POMs
using Project.POMs.TopNav;
using Project.POMs.Account_POM;
using Project.POMs.Cart_POM;
using Project.POMs.Checkout_POM;
using Project.POMs.Shop_POM;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Project.StepDefinitions
{
    [Binding]
    public class PlaceAnOrderStepDefinitions
    {

        //set up scenario context for test class to enable sharing of data between steps
        private ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        //constructer to get the ScenarioContext
        //also gets the driver from the base class
        public PlaceAnOrderStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = (IWebDriver)_scenarioContext["driver"];
        }

        [Given(@"I am logged in")]
        public void GivenIAmLoggedInUsingTheDetailsInFile()
        {

            topNav.Account.Click();

            //string loginDetails = @"../../../project login deets.txt";
            account = new Account_POM(_driver);

            account.Login();
        }

        [Given(@"I have item '([0-9]{1,2})' in the cart")]
        public void GivenIHaveItemInTheCart(int itemNum)
        {
            topNav.Shop.Click();

            shop = new Shop_POM(_driver);

            shop.ClickBuyItem(1);
        }

        [Given(@"I am on the cart page")]
        public void GivenIAmOnTheCartPage()
        {
            topNav.ScrollToTopNav();
            topNav.Cart.Click();
            cart = new Cart_POM(_driver);
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
            Assert.That(cart.ExpectedDiscount(), Is.EqualTo(cart.ActualDiscount), $"Incorrect discount applied: expected {cart.GetDiscount()}%, was actually {(int)((cart.ActualDiscount / cart.GetOriginalPrice) * 100)}%");
            //cart.CheckDiscountIsCorrect();
            

        }

        [Then(@"the correct price is displayed")]
        public void ThenTheCorrectPriceIsDisplayed()
        {
            Assert.That(cart.ExpectedTotal(), Is.EqualTo(cart.GetActualTotal), $"Total Price is incorrect: expected {cart.ExpectedTotal()}, but was actually {cart.GetActualTotal}");
            //cart.CheckTotalPriceIsCorrect();
        }

        [When(@"I place an order")]
        public void WhenIPlaceAnOrder()
        {
            topNav.Checkout.Click();
            //string checkoutDetails = @"../../../project checkout deets.txt";
            checkout = new Checkout_POM(_driver);

            checkout.EnterDetails();
            checkout.PlaceOrder();
        }

        [Then(@"the order can be found on the account")]
        public void ThenTheOrderCanBeFoundOnTheAccount()
        {
            string orderNum = checkout.GetOrderNumber();
            topNav.Account.Click();
            account.Orders.Click();
            Assert.That(account.OrderFound(orderNum), Is.True, $"Order {orderNum} was not found on the account");
        }

    }
}