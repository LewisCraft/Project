using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.POMs.Cart_POM
{
    public class Cart_POM
    {

        private IWebDriver _driver;
        private int _discount;

        //constructor
        public Cart_POM(IWebDriver driver)
        {
            _driver = driver;
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

        //function to remove item from the cart
        public void RemoveItem()
        {
            try
            {
                _driver.FindElement(By.CssSelector(".remove")).Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine("cart is probably empty already");
            }
        }

        //functions to enter the coupon into the coupon code field
        public IWebElement GetCoupon => _driver.FindElement(By.Id("coupon_code"));
        public IWebElement GetCouponButton => _driver.FindElement(By.CssSelector(@"button.button:nth-child(3)"));
        public void EnterCoupon(string couponCode)
        {
            GetCoupon.Clear();
            GetCoupon.SendKeys(couponCode);
            ScrollTo(GetCouponButton);
            GetCouponButton.Click();
        }


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

        public void CheckDiscountIsCorrect()
        {
            Assert.That(ExpectedDiscount(), Is.EqualTo(ActualDiscount), $"Incorrect discount applied: expected {_discount}%, was actually {(int)((ActualDiscount / GetOriginalPrice) * 100)}%");
        }

        public void CheckTotalPriceIsCorrect()
        {
            Assert.That(ExpectedTotal(), Is.EqualTo(GetActualTotal), "Total Price is incorrect");
        }

    }
}
