using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.POMs.Shop_POM
{
    public class Shop_POM
    {

        private IWebDriver _driver;

        //constructor
        public Shop_POM(IWebDriver driver)
        {
            _driver = driver;
        }

        //functions to find and click on a given item in the shop, works for values 0 - 11
        public IWebElement GetItem(int n) => _driver.FindElement(By.CssSelector($".post-{n+27} > a:nth-child(2)"));
        public void ClickBuyItem(int n) 
        { 
            ScrollTo(GetItem(n));
            GetItem(n).Click();
        }

        //javascript executor to scroll to a given item in the shop to click on
        public void ScrollTo(IWebElement element) { ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element); }

    }
}
