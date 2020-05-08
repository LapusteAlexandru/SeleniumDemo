using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class SignUpTyPage
    {
        public SignUpTyPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'content')]")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'thank-you-block')]")]
        public IWebElement tyMessage { get; set; }

    }
}
