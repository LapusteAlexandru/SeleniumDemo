using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class ResetPasswordPage
    {
        public ResetPasswordPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form[@name='resetPasswordForm']")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement passwordInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//h1[text()='Reset password']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.Id, Using = "ConfirmPassword")]
        public IWebElement confirmPasswordInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@value='resetPassword']")]
        public IWebElement resetBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@value='cancel']")]
        public IWebElement cancelBtn { get; set; }

        [FindsBy(How = How.LinkText, Using = "Already have an account?")]
        public IWebElement loginBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-messages='resetPasswordForm.password.$error']//div[@ng-message='required']")]
        public IWebElement passwordRequiredMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-messages='resetPasswordForm.confirmPassword.$error']//div[@ng-message='required']")]
        public IWebElement confirmPasswordRequiredMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-message='compareTo']")]
        public IWebElement passwordsDontMatchMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[text()=\"You can't use old password\"]")]
        public IWebElement oldPasswordValidationMsg { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(passwordInput);
            mainElements.Add(confirmPasswordInput);
            mainElements.Add(resetBtn);
            mainElements.Add(cancelBtn);
            return mainElements;
        }
        public void DoReset(string password,string confirmpassword)
        {
            passwordInput.Clear();
            passwordInput.SendKeys(password);
            confirmPasswordInput.Clear();
            confirmPasswordInput.SendKeys(confirmpassword);
            resetBtn.Click();
        }

        public HomePage ClickCancel()
        {
            cancelBtn.Click();
            return new HomePage(TestBase.driver);
        }
        public LoginPage ClickAlreadyHaveAccount()
        {
            loginBtn.Click();
            return new LoginPage(TestBase.driver);
        }
    }
}
