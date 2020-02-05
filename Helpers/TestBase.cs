

using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCoS
{
    public class TestBase
    {
        public static string username = "amdaris.rcos@gmail.com";
        public static string password = "123aA@123";
        public static string adminUsername = "mail@mail.com";
        public static string adminPassword = "P@ssword1";
        public static string userTitle = "Dr.";
        public static string userFirstName = "John";
        public static string userLastName = "Doe";
        public static string userAddress = "UK";
        public static string userPhone = "123123123";
        public static string userGender = "Male";
        public static string userGmcNumber = "1231231";
        public static string userGmcSpecialty = "General Surgery";
        public static string userCareerGrade = "Associate Specialist";

        public static IWebDriver driver { get; set; }
        public static WebDriverWait wait { get; set; }
        public static void RootInit()
        {
            var cap = new ChromeOptions();
            cap.AddArgument("start-maximized");
            cap.AddArgument("--ignore-certificate-errors");
            cap.AddArgument("--disable-popup-blocking");
            cap.AddArgument("--incognito");
            driver = new ChromeDriver(cap);
            driver.Url = "https://rcs-cosmetics-client-dev.azurewebsites.net/";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        public static void SwitchTab()
        {
            var parentTab = driver.CurrentWindowHandle;
            IList<string> tabs = new List<string>(driver.WindowHandles);
            foreach (var handle in tabs)
                if (!parentTab.Equals(handle))
                    driver.SwitchTo().Window(handle);
        }

            
        public static void SelectOption(string select,string option)
        {
            driver.FindElement(By.Id(select)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(string.Format("//span[contains(text(),'{0}')]", option))));
            driver.FindElement(By.XPath(string.Format("//span[contains(text(),'{0}')]",option))).Click();
        }

        public static bool ElementIsPresent(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        

        public static void TakeScreenShot()
        {

            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                string path = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\screens\\";
                string testName = TestContext.CurrentContext.Test.Name;
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile($"{path}{timestamp} {testName}." + ScreenshotImageFormat.Png);
            }
        }
    }
}

