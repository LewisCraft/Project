using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Project.Utilities.POM_Helper;

namespace Project.POMs.Cart_POM
{
    public class Cart_POM
    {

        //set up driver and wait
        //discount value is used in calculating values related to the expected discount
        private IWebDriver _driver;
        private int _discount;

        //locator for the remove button for an item in the cart
        public IWebElement RemoveButton => _driver.FindElement(By.CssSelector(".remove"));

        //locators for fields related to applying the coupon code
        public IWebElement GetCoupon => _driver.FindElement(By.Id("coupon_code"));
        public IWebElement GetCouponButton => _driver.FindElement(By.CssSelector(@"button.button:nth-child(3)"));

        //locators for price related elements
        private string GetOriginalPriceString => _driver.FindElement(By.CssSelector("td.product-subtotal > span:nth-child(1)")).Text;
        private string GetShippingString => _driver.FindElement(By.CssSelector(".shipping > td:nth-child(2) > span:nth-child(1)")).Text;
        private string GetActualDiscount => _driver.FindElement(By.CssSelector(".cart-discount > td:nth-child(2) > span:nth-child(1)")).Text;
        private string GetActualTotalString => _driver.FindElement(By.CssSelector(".order-total > td:nth-child(2) > strong:nth-child(1) > span:nth-child(1)")).Text;

        //functions to process price values from the page for comparison and some calculations

        //parse the strings into a decimals, also remove currency symbol
        public decimal GetOriginalPrice => Decimal.Parse(GetOriginalPriceString.Substring(1));
        public decimal GetShipping => Decimal.Parse(GetShippingString.Substring(1));
        public decimal ActualDiscount => Decimal.Parse(GetActualDiscount.Substring(1));
        public decimal GetActualTotal => Decimal.Parse(GetActualTotalString.Substring(1));


        //calculations

        //calculate expected discount of 15% from the price
        public decimal ExpectedDiscount() => (GetOriginalPrice / 100) * _discount;

        //calculate the expected total price using the item price, the shipping cost and the expected discount
        public decimal ExpectedTotal() => (GetOriginalPrice - ExpectedDiscount()) + GetShipping;


        //constructor
        //discount is set to 0 by default
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

        //function to remove item from the cart
        //probably only works when only one item type is in the cart
        public void RemoveItem()
        {
            try
            {
                Thread.Sleep(500);
                ScrollTo(RemoveButton, _driver);
                RemoveButton.Click();
                By emptyCart = By.CssSelector(".cart-empty");
                WaitFor(emptyCart, _driver);

            }
            catch
            {
                Console.WriteLine("cart is probably empty");
            }
        }

        //functions to enter the coupon into the coupon code field
        //takes the coupon code as a string as input
        public void EnterCoupon(string couponCode)
        {
            GetCoupon.Clear();
            GetCoupon.SendKeys(couponCode);
            ScrollTo(GetCouponButton, _driver);
            GetCouponButton.Click();
            Thread.Sleep(1000);
            TakeScreenshot(_driver, "coupon.png");
        }

        //function to check if the cart is empty
        public bool IsEmpty()
        {
            return _driver.FindElement(By.CssSelector(@".cart-empty")).Displayed;
        }
    }
}
