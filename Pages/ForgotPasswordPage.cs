using Helpers;
using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static Helpers.MailSubjectEnum;

namespace Pages
{
    class ForgotPasswordPage
    {
        public ForgotPasswordPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//form[@name='forgotPasswordForm']")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//h1[text()='Forgot your password?']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'is not registered or email is not confirmed')]")]
        public IWebElement userNotRegistered { get; set; }

        [FindsBy(How = How.Id, Using = "Email")]
        public IWebElement emailInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-messages='forgotPasswordForm.email.$error']//div[@ng-message='required']")]
        public IWebElement emailRequiredMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@value='forgotPassword']")]
        public IWebElement submitBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@value='cancel']")]
        public IWebElement cancelBtn { get; set; }

        [FindsBy(How = How.LinkText, Using = "Already have an account?")]
        public IWebElement loginBtn { get; set; }

        [FindsBy(How = How.LinkText, Using = "Home")]
        public IWebElement homeBtn { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(title);
            mainElements.Add(emailInput);
            mainElements.Add(submitBtn);
            mainElements.Add(cancelBtn);
            mainElements.Add(loginBtn);
            return mainElements;
        }

        public LoginPage getLogin()
        {
            loginBtn.Click();
            return new LoginPage(TestBase.driver);
        }
        public HomePage ClickHome()
        {
            homeBtn.Click();
            return new HomePage(TestBase.driver);
        }

        public void SendResetEmail(string username)
        {
            emailInput.Clear();
            emailInput.SendKeys(username);
            submitBtn.Click();
        }

        public ResetPasswordPage GetResetPassword(string username)
        {
            emailInput.Clear();
            emailInput.SendKeys(username);
            submitBtn.Click();
            Thread.Sleep(2000);
            if (TestBase.ElementIsPresent(userNotRegistered))
                return null;
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.username, TestBase.password);
            string allEmails = mailRepository.GetUnreadMails(Subject.ForgotPassword);
            var linkParser = new Regex(@"(?:https?://rcs-cosmetics-identity-dev.azurewebsites.net/Account/ResetPassword)\S+\b");
            var link = linkParser.Matches(allEmails) ;
            TestBase.driver.Url = link.SingleOrDefault().ToString().Replace("&amp;", "&");
            return new ResetPasswordPage(TestBase.driver);
        }
    }
}
