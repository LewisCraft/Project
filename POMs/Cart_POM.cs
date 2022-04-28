using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.POMs.Cart_POM
{
    public class Cart_POM
    {

        //set up driver and wait
        //discount value is used in calculating values related to the expected discount
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private int _discount;

        //constructor
        //discount is set to 0 by default
        public Cart_POM(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _discount = 0;
        }

        //getter and setter for discount value
        public void SetDiscount(int discount)
        {
            _discount = discount;
        }

        public int GetDiscount()
        {
            return _discount;
        }

        //function to scroll to the given element on the page
        public void ScrollTo(IWebElement element)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        //functions to remove item from the cart
        //probably only works when only one item type is in the cart
        public IWebElement RemoveButton => _driver.FindElement(By.CssSelector(".remove"));
        public void RemoveItem()
        {
            try
            {
                //Thread.Sleep(500);
                ScrollTo(RemoveButton);
                RemoveButton.Click();
                _wait.Until(drv => drv.FindElement(By.CssSelector(".cart-empty")).Displayed);

            }
            catch (Exception ex)
            {
                Console.WriteLine("cart is probably empty already");
            }
        }

        //functions to enter the coupon into the coupon code field
        //takes the coupon code as a string as input
        public IWebElement GetCoupon => _driver.FindElement(By.Id("coupon_code"));
        public IWebElement GetCouponButton => _driver.FindElement(By.CssSelector(@"button.button:nth-child(3)"));
        public void EnterCoupon(string couponCode)
        {
            GetCoupon.Clear();
            GetCoupon.SendKeys(couponCode);
            ScrollTo(GetCouponButton);
            GetCouponButton.Click();
        }

        //functions to get price values from the page for comparison and some calculations

        //get price of the item as a string
        private string GetOriginalPriceString => _driver.FindElement(By.CssSelector("td.product-subtotal > span:nth-child(1)")).Text;
        //parse the price into a decimal, also removes currency symbol
        public decimal GetOriginalPrice => Decimal.Parse(GetOriginalPriceString.Substring(1));

        //same process with shipping as with the price
        private string GetShippingString => _driver.FindElement(By.CssSelector(".shipping > td:nth-child(2) > span:nth-child(1)")).Text;
        public decimal GetShipping => Decimal.Parse(GetShippingString.Substring(1));

        //calculate expected discount of 15% from the price
        public decimal ExpectedDiscount() => (GetOriginalPrice / 100) * _discount;

        //get the discount as calculated by the website
        private string GetActualDiscount => _driver.FindElement(By.CssSelector(".cart-discount > td:nth-child(2) > span:nth-child(1)")).Text;
        public decimal ActualDiscount => Decimal.Parse(GetActualDiscount.Substring(1));

        //calculate the expected total price using the item price, the shipping cost and the expected discount
        public decimal ExpectedTotal() => (GetOriginalPrice - ExpectedDiscount()) + GetShipping;

        //get the final total as calculated by the website
        private string GetActualTotalString => _driver.FindElement(By.CssSelector(".order-total > td:nth-child(2) > strong:nth-child(1) > span:nth-child(1)")).Text;
        public decimal GetActualTotal => Decimal.Parse(GetActualTotalString.Substring(1));

        //function to assert that the coupon value is correctly assigned
        //error message gives actual and expected discounts as percentages
        public void CheckDiscountIsCorrect()
        {
            Assert.That(ExpectedDiscount(), Is.EqualTo(ActualDiscount), $"Incorrect discount applied: expected {_discount}%, was actually {(int)((ActualDiscount / GetOriginalPrice) * 100)}%");
        }

        //function to assert that the end price with discount and shipping is correct
        public void CheckTotalPriceIsCorrect()
        {
            Assert.That(ExpectedTotal(), Is.EqualTo(GetActualTotal), "Total Price is incorrect");
        }

    }
}
