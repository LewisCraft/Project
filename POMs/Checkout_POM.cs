using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.POMs.Checkout_POM
{
    public class Checkout_POM
    {

        private IWebDriver _driver;
        private WebDriverWait _wait;

        public Checkout_POM(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        //functions to enter the billing information in the appropriate fields
        public IWebElement GetFName => _driver.FindElement(By.Id("billing_first_name"));
        public void EnterFName(string fname)
        {
            GetFName.Clear();
            GetFName.SendKeys(fname);
        }

        public IWebElement GetLName => _driver.FindElement(By.Id("billing_last_name"));
        public void EnterLName(string lname)
        { 
            GetLName.Clear();
            GetLName.SendKeys(lname);
        }

        public IWebElement GetAddress => _driver.FindElement(By.Id("billing_address_1"));
        public void Enteraddress(string address)
        {
            GetAddress.Clear();
            GetAddress.SendKeys(address);
        }

        public IWebElement GetCity => _driver.FindElement(By.Id("billing_city"));
        public void EnterCity(string city)
        {
            GetCity.Clear();
            GetCity.SendKeys(city);
        }

        public IWebElement GetPostcode => _driver.FindElement(By.Id("billing_postcode"));
        public void EnterPostcode(string postcode)
        {
            GetPostcode.Clear();
            GetPostcode.SendKeys(postcode);
        }

        public IWebElement GetPhone => _driver.FindElement(By.Id("billing_phone"));
        public void EnterPhone(string phone)
        {
            GetPhone.Clear();
            GetPhone.SendKeys(phone);
        }

        public void EnterDetails(string filepath)
        {
            string[] inputs = System.IO.File.ReadAllLines(filepath);

            EnterFName(inputs[0]);
            EnterLName(inputs[1]);
            Enteraddress(inputs[2]);
            EnterCity(inputs[3]);
            EnterPostcode(inputs[4]);
            EnterPhone(inputs[5]);
        }

        //function to ensure that the pay by check payment method is selected
        public void PayByCheck() {
            try
            {
                _wait.Until(drv => drv.FindElement(By.CssSelector("li.wc_payment_method:nth-child(1) > label:nth-child(2)")).Displayed);
                IWebElement payByCheck = _driver.FindElement(By.CssSelector("li.wc_payment_method:nth-child(1) > label:nth-child(2)"));
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", payByCheck);
                payByCheck.Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine("was unable to click the pay by check option");
                Console.WriteLine(ex.Message);
            }
        }

        public IWebElement GetPlaceOrder => _driver.FindElement(By.Id("place_order"));
        public void PlaceOrder()
        {
            try
            {
                //((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", GetPlaceOrder);
                _wait.Until(drv => GetPlaceOrder.Displayed);
                Thread.Sleep(500);
                GetPlaceOrder.Click();

            }
            catch(Exception ex)
            {
                Console.WriteLine("unable to click the place order button");
                Console.WriteLine(ex.Message);
            }
        }

        public string GetOrderNumber()
        {
            _wait.Until(drv => drv.FindElement(By.CssSelector(".woocommerce-order-overview__order > strong:nth-child(1)")).Displayed);
            IWebElement orderNumber = _driver.FindElement(By.CssSelector(".woocommerce-order-overview__order > strong:nth-child(1)"));
            return orderNumber.Text;
        }
        
    }
}
