using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.POMs.Account_POM
{
    public class Account_POM
    {

        private IWebDriver _driver;

        public Account_POM(IWebDriver driver)
        {
            _driver = driver;
        }

        //functions to find and use fields for log in
        public IWebElement Username => _driver.FindElement(By.Id("username"));
        public void EnterUsername(string username) { Username.SendKeys(username); }

        public IWebElement Password => _driver.FindElement(By.Id("password"));
        public void EnterPassword(string password) { Password.SendKeys(password); }

        public IWebElement Submit => _driver.FindElement(By.CssSelector("p.form-row:nth-child(3) > button:nth-child(3)"));
        public void ClickSubmit() { Submit.Click(); }

        //function to simplify login by calling all required functions using file as input
        public void Login(string filepath)
        {
            string[] inputs = System.IO.File.ReadAllLines(filepath);

            EnterUsername(inputs[0]);
            EnterPassword(inputs[1]);
            ClickSubmit();
        }

        //function to scroll to the given element on the page
        public void ScrollTo(IWebElement element)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        //lambdas to get the links on the side bar once logged in
        public IWebElement DashBoard => _driver.FindElement(By.LinkText("Dashboard"));
        public IWebElement Orders => _driver.FindElement(By.LinkText("Orders"));
        public IWebElement Downloads => _driver.FindElement(By.LinkText("Downloads"));
        public IWebElement Addresses => _driver.FindElement(By.LinkText("Addresses"));
        public IWebElement AccountDetails => _driver.FindElement(By.LinkText("Account details"));
        public IWebElement LogOut => _driver.FindElement(By.LinkText("Logout"));
        public void ClickLogout()
        {
            ScrollTo(LogOut);
            LogOut.Click();
        }

        //function to look for order using the order number found when placing the order
        public void FindOrder(string orderNum)
        {

            bool found = false;

            try
            {
                _driver.FindElement(By.LinkText("#" + orderNum));
                //Assert.Pass("The order has been found");
                found = true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine("The order has not been found");
                //Console.WriteLine(ex.Message);
                //Assert.Fail("The order was not found");
                found = false;
            }

            Assert.That(found, Is.True, "The order was not found");

        }

    }
}
