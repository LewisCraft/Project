using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project.Utilities.POM_Helper;

namespace Project.POMs.TopNav
{
    public class TopNav
    {

        private IWebDriver _driver;

        public TopNav(IWebDriver driver)
        {
            this._driver = driver;
        }

        //lambdas to get all the links in the navigation bar
        public IWebElement Home => _driver.FindElement(By.LinkText("Home"));
        public IWebElement Shop => _driver.FindElement(By.LinkText("Shop"));
        public IWebElement Cart => _driver.FindElement(By.LinkText("Cart"));
        public IWebElement Checkout => _driver.FindElement(By.LinkText("Checkout"));
        public IWebElement Account => _driver.FindElement(By.LinkText("My account"));
        public IWebElement Blog => _driver.FindElement(By.LinkText("Blog"));

        public void ScrollToTopNav() => ScrollTo(Home, _driver);

    }
}
