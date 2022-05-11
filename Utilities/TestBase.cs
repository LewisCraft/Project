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

        //set up scenario context for base test class
        private ScenarioContext _scenarioContext;

        //define poms for use
        public static TopNav topNav;
        public static Shop_POM shop;
        public static Cart_POM cart;
        public static Checkout_POM checkout;
        public static Account_POM account;

        //set up scenario context for sharing data between steps
        public TestBase(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [BeforeScenario]
        public void SetUP()
        {
            //get base URL from environment variables
            string baseUrl = Environment.GetEnvironmentVariable("BaseUrl");
            if(baseUrl == null)
            {
                Console.WriteLine("Base url not found");
                //default value for now if environment variables not found
                baseUrl = "http://edgewordstraining.co.uk/demo-site";
            }

            //set up the driver as a chrome driver
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = baseUrl+"/my-account";

            //store driver in scenario context to share between steps
            _scenarioContext["driver"] = driver;

            //make the topnav POM
            topNav = new TopNav(driver);
            

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
            catch
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
            account.ClickLogout();
            IWebDriver driver = (IWebDriver)_scenarioContext["driver"];
            driver.Quit();

        }

    }
}
