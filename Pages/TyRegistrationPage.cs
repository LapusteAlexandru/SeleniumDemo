using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class TyRegistrationPage
    {
        public TyRegistrationPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//md-card[@class='_md']")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//h2[text()='Thank you for email confirmation']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.LinkText, Using = "Home")]
        public IWebElement homeBtn { get; set; }

    }
}
