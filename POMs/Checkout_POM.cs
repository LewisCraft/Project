using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Utilities.CheckoutDetails;

namespace Project.POMs.Checkout_POM
{
    public class Checkout_POM
    {

        private IWebDriver _driver;
        private WebDriverWait _wait;

        //locators for the checkout fields
        public IWebElement GetFName => _driver.FindElement(By.Id("billing_first_name"));
        public IWebElement GetLName => _driver.FindElement(By.Id("billing_last_name"));
        public IWebElement GetAddress => _driver.FindElement(By.Id("billing_address_1"));
        public IWebElement GetCity => _driver.FindElement(By.Id("billing_city"));
        public IWebElement GetPostcode => _driver.FindElement(By.Id("billing_postcode"));
        public IWebElement GetPhone => _driver.FindElement(By.Id("billing_phone"));
        public IWebElement GetPlaceOrder => _driver.FindElement(By.Id("place_order"));

        //constructor
        public Checkout_POM(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        //functions to enter the billing information in the appropriate fields
        //can take strings for inputs, otherwise use environment variables
        
        public void EnterFName(string fname)
        {
            GetFName.Clear();
            GetFName.SendKeys(fname);
        }

        public void EnterLName(string lname)
        { 
            GetLName.Clear();
            GetLName.SendKeys(lname);
        }
        
        public void EnterAddress(string address)
        {
            GetAddress.Clear();
            GetAddress.SendKeys(address);
        }
                
        public void EnterCity(string city)
        {
            GetCity.Clear();
            GetCity.SendKeys(city);
        }
        
        public void EnterPostcode(string postcode)
        {
            GetPostcode.Clear();
            GetPostcode.SendKeys(postcode);
        }

        public void EnterPhone(string phone)
        {
            GetPhone.Clear();
            GetPhone.SendKeys(phone);
        }

        //functions to simplify the inputs, can take a filepath as a string for input file
        //otherwise will use the environment variables
        public void EnterDetails(string filepath)
        {
            string[] inputs = System.IO.File.ReadAllLines(filepath);
            inputs = inputs[0].Split(',');

            EnterFName(inputs[0]);
            EnterLName(inputs[1]);
            EnterAddress(inputs[2]);
            EnterCity(inputs[3]);
            EnterPostcode(inputs[4]);
            EnterPhone(inputs[5]);
        }
        public void EnterDetails()
        {

            CheckoutDetails details = new CheckoutDetails();
            EnterDetails(details);
            
        }
        private void EnterDetails(CheckoutDetails details)
        {
            EnterFName(details.FName);
            EnterLName(details.LName);
            EnterAddress(details.Address);
            EnterCity(details.City);
            EnterPostcode(details.Postcode);
            EnterPhone(details.Phone);
        }

        //function to press the place order button
        public void PlaceOrder()
        {
            try
            {
                _wait.Until(drv => GetPlaceOrder.Displayed);
                //has to wait for a moment as the button seems to move immediately after inputs
                Thread.Sleep(1000);
                GetPlaceOrder.Click();

            }
            catch(Exception ex)
            {
                Console.WriteLine("unable to click the place order button");
                Console.WriteLine(ex.Message);
            }
        }

        //function to get the order number from the reciept after placing an order
        //returns order number as a string
        public string GetOrderNumber()
        {
            _wait.Until(drv => drv.FindElement(By.CssSelector(".woocommerce-order-overview__order > strong:nth-child(1)")).Displayed);
            IWebElement orderNumber = _driver.FindElement(By.CssSelector(".woocommerce-order-overview__order > strong:nth-child(1)"));
            return orderNumber.Text;
        }
        
    }
}
