using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project.Utilities.POM_Helper;

namespace Project.POMs.Shop_POM
{
    public class Shop_POM
    {

        private IWebDriver _driver;

        //locator for a given item number, currently works for values from 0 - 11
        public IWebElement GetItem(int n) => _driver.FindElement(By.CssSelector($".post-{n + 27} > a:nth-child(2)"));

        //constructor
        public Shop_POM(IWebDriver driver)
        {
            _driver = driver;
        }

        //functions to find and click on a given item in the shop, works for values 0 - 11
        public void ClickBuyItem(int n) 
        { 
            ScrollTo(GetItem(n), _driver);
            GetItem(n).Click();
        }

    }
}
