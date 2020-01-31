using OpenQA.Selenium;
using RCoS;
using Pages;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class DashboardPage
    {
        public DashboardPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//mat-sidenav")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.XPath, Using = "//title[text()='RCS.Cosmetics.Web']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Account Details')]")]
        public IWebElement accountDetailsBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Registration Requests')]")]
        public IWebElement registrationRequestsBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='user-details-block']//h5")]
        public IWebElement username { get; set; }

        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'menu-icon')]")]
        public IWebElement sidebarMenuBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'sign-out')]")]
        public IWebElement signOutBtn { get; set; }

        [FindsBy(How = How.LinkText, Using = "Home")]
        public IWebElement homeBtn { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(sidebarMenuBtn);
            mainElements.Add(username);
            mainElements.Add(signOutBtn);
            mainElements.Add(accountDetailsBtn);
            mainElements.Add(homeBtn);
            return mainElements;
        }

        public AccountDetailsPage getAccountDetails()
        {
            accountDetailsBtn.Click();
            return new AccountDetailsPage(TestBase.driver);
        }
    }
}
