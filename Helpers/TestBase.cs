

using Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Helpers
{
    public class TestBase
    {
        public static string username = "alexandru.lapuste@amdaris.com";
        public static string password = "123qweASD";
        public static DateTime dt = DateTime.Now;
        public static string currentMonth = dt.ToString("MMMM");
        public static int currentDay = dt.Day;
        public static string currentDayNumber = dt.Day.ToString();
        public static string currentYear = dt.Year.ToString();
        public static string currentDate = currentMonth + " " + currentDay.ToString("00") + ", " + currentYear;
        public static string userBirthday = currentMonth + " 01, " + currentYear;
        public static string caseDate = "1/"+ dt.Month + "/" + currentYear;
        public static IWebDriver driver { get; set; }
        public static WebDriverWait wait { get; set; }
        public static WebDriverWait uploadWait { get; set; }
        public static void RootInit()
        {
            
            var cap = new ChromeOptions();
            cap.AddArgument("start-maximized");
            cap.AddArgument("--ignore-certificate-errors");
            cap.AddArgument("--disable-popup-blocking");
            cap.AddArgument("--incognito");
            driver = new ChromeDriver(cap);
            driver.Url = "http://automationpractice.com/index.php";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            uploadWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
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
                string testName = TestContext.CurrentContext.Test.Name;
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss"); 
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string path = Directory.GetCurrentDirectory() + timestamp+testName+ ".png";
                ss.SaveAsFile(path);
                TestContext.AddTestAttachment(path);
            }
        }


        public static void uploadField(string fileName,string fileExtension)
        {
            IList<IWebElement> uploadInputs = driver.FindElements(By.XPath("//input[@type='file']"));
            foreach (IWebElement e in uploadInputs)
            {
                e.SendKeys(TestContext.Parameters["uploadFilesPath"] + fileName + "." + fileExtension);
                try
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//mat-form-field[contains(@class,'uploaded-file')]//input")));
                    
                }
                catch { }
            }
            Thread.Sleep(500);
            IList <IWebElement> deleteButtons = driver.FindElements(By.XPath("//app-file-item//button"));
            foreach (IWebElement e in deleteButtons)
            {
                uploadWait.Until(ExpectedConditions.ElementToBeClickable(e));
            }

        }

        
        public static void ScrollToElemnent(string xpath)
        {
            var element = driver.FindElement(By.XPath(xpath));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }
    }
}

