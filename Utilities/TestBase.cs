using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Project.POMs.TopNav;
using Project.POMs.Shop_POM;
using Project.POMs.Checkout_POM;
using Project.POMs.Cart_POM;
using Project.POMs.Account_POM;

namespace Project.Utilities
{
    [Binding]
    public class TestBase
    {
        //set up webdriver
        public static IWebDriver driver;

        //set up poms for use
        public static TopNav topNav;
        public static Shop_POM shop;
        public static Cart_POM cart;
        public static Checkout_POM checkout;
        public static Account_POM account;

        [BeforeScenario]
        public void SetUP()
        {
            //get base URL from environment variables
            string baseUrl;
            try
            {
                baseUrl = Environment.GetEnvironmentVariable("BaseUrl");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Base url not found");
                throw ex;
            }

            //set up the driver as a chrome driver
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.Manage().Window.Maximize();
            driver.Url = baseUrl+"/my-account";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            //make the POMs
            topNav = new TopNav(driver);
            shop = new Shop_POM(driver);
            cart = new Cart_POM(driver);
            checkout = new Checkout_POM(driver);
            account = new Account_POM(driver);

        }

        [AfterScenario]
        public void TearDown()
        {
            Thread.Sleep(1000);
            topNav.ScrollToTopNav();
            topNav.Cart.Click();
            bool cartEmpty = true;
            try
            {
                cartEmpty = cart.IsEmpty();
            }
            catch(Exception ex)
            {
                cartEmpty = false;
            }
            if(!cartEmpty)
            {
                cart.RemoveItem();
                Thread.Sleep(1000);
            }
            topNav.ScrollToTopNav();
            topNav.Account.Click();
            account.ScrollTo(account.LogOut);
            account.LogOut.Click();
            driver.Quit();

        }

    }
}
