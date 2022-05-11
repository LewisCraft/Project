using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Project.Utilities.POM_Helper;

namespace Project.POMs.Account_POM
{
    public class Account_POM
    {

        private IWebDriver _driver;

        //locators for account dashboard
        public IWebElement DashBoard => _driver.FindElement(By.LinkText("Dashboard"));
        public IWebElement Orders => _driver.FindElement(By.LinkText("Orders"));
        public IWebElement Downloads => _driver.FindElement(By.LinkText("Downloads"));
        public IWebElement Addresses => _driver.FindElement(By.LinkText("Addresses"));
        public IWebElement AccountDetails => _driver.FindElement(By.LinkText("Account details"));
        public IWebElement LogOut => _driver.FindElement(By.LinkText("Logout"));

        //locators for login fields
        public IWebElement Username => _driver.FindElement(By.Id("username"));
        public IWebElement Password => _driver.FindElement(By.Id("password"));
        public IWebElement Submit => _driver.FindElement(By.CssSelector("p.form-row:nth-child(3) > button:nth-child(3)"));

        public Account_POM(IWebDriver driver)
        {
            _driver = driver;
        }

        //functions to use fields for log in
        //functions use string input
        
        public void EnterUsername(string username) { Username.SendKeys(username); }

        public void EnterPassword(string password) { Password.SendKeys(password); }

        public void ClickSubmit() { Submit.Click(); }

        //functions to simplify login by calling all required functions
        //can use filepath for an input file or just use environment variables instead
        public void Login(string filepath)
        {
            string[] inputs = System.IO.File.ReadAllLines(filepath);

            EnterUsername(inputs[0]);
            EnterPassword(inputs[1]);
            ClickSubmit();
        }
        public void Login()
        {
            EnterUsername(Environment.GetEnvironmentVariable("username"));
            EnterPassword(Environment.GetEnvironmentVariable("password"));
            ClickSubmit();
        }

        

        
        
        //function to log out
        public void ClickLogout()
        {
            ScrollTo(LogOut, _driver);
            LogOut.Click();
        }

        //function to look for order using the order number found when placing the order
        public bool OrderFound(string orderNum)
        {

            bool found = false;

            try
            {
                _driver.FindElement(By.LinkText("#" + orderNum));
                //Assert.Pass("The order has been found");
                found = true;
            }
            catch
            {
                //Console.WriteLine("The order has not been found");
                //Console.WriteLine(ex.Message);
                //Assert.Fail("The order was not found");
                found = false;
            }

            return found;

        }

    }
}
