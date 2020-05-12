using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class TyResetPasswordPage
    {
        public TyResetPasswordPage(IWebDriver driver)
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
        [FindsBy(How = How.XPath, Using = "//h2[text()='Password reset successfully. You can sign-in using new password. ']")]
        public IWebElement tyMsg { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'Login')]")]
        public IWebElement loginBtn { get; set; }
        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(tyMsg);
            mainElements.Add(loginBtn);
            return mainElements;
        }

    }
}
