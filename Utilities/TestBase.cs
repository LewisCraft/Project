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

        public static IWebDriver driver;

        public static TopNav topNav;
        public static Shop_POM shop;
        public static Cart_POM cart;
        public static Checkout_POM checkout;
        public static Account_POM account;

        [BeforeScenario]
        public void SetUP()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            topNav = new TopNav(driver);
            shop = new Shop_POM(driver);
            cart = new Cart_POM(driver);
            checkout = new Checkout_POM(driver);
            account = new Account_POM(driver);

        }


        [AfterScenario]
        public void TearDown()
        {

            topNav.Account.Click();
            account.ScrollTo(account.LogOut);
            account.LogOut.Click();
            driver.Quit();

        }

    }
}
