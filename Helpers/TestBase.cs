
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Pages;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RCoS
{
    class TestBase
    {
        public static string username = "amdaris.rcos@gmail.com";
        public static string password = "123aA@123";
        public static string adminUsername = "mail@mail.com";
        public static string adminPassword = "P@ssword1";

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
            driver.FindElement(By.Id(option)).Click();
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

        public static IWebElement tableData(string value)
        {
            return driver.FindElement(By.XPath(String.Format("//td//div[contains(text(),'%s')]", value)));
        }
    }
}

