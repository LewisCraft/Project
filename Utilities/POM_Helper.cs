using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Utilities
{
    //helper class for the pom classes
    static class POM_Helper
    {

        //function to scroll to the given element on the page
        public static void ScrollTo(IWebElement element, IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        //function to use explicit waits
        public static void WaitFor(IWebElement element, IWebDriver driver)
        {
            //have to make a new wait in case different driver is used as class is static
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => element.Displayed);

        }
        public static void WaitFor(By element, IWebDriver driver)
        {
            //have to make a new wait in case different driver is used as class is static
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => driver.FindElement(element).Displayed);

        }

        public static void TakeScreenshot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot ssDriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssDriver.GetScreenshot();
            screenshot.SaveAsFile(@"../../../screenshots/"+screenshotName, ScreenshotImageFormat.Png);
        }



    }
}
