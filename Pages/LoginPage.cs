using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class LoginPage
    {
        
        public LoginPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form[@name='loginForm']")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Id, Using = "Username")]
        public IWebElement emailInput { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement passwordInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@value='login']")]
        public IWebElement loginBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@value='cancel']")]
        public IWebElement cancelBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text()='Forgot your password?']")]
        public IWebElement forgotYourPasswordBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text()='Not register yet?']")]
        public IWebElement signUpBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//h1[text()='Login']")]
        public IWebElement title { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'validation-summary-errors')]//li[contains(text(),'Invalid username or password')]")]
        public IWebElement userOrPassValidationMsg { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@ng-messages='loginForm.username.$error']//div[@ng-message='required']")]
        public IWebElement emailRequiredMsg { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@ng-messages='loginForm.password.$error']//div[@ng-message='required']")]
        public IWebElement passwordRequiredMsg { get; set; }

        [FindsBy(How = How.LinkText, Using = "Home")]
        public IWebElement homeBtn { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(emailInput);
            mainElements.Add(passwordInput);
            mainElements.Add(loginBtn);
            mainElements.Add(cancelBtn);
            mainElements.Add(forgotYourPasswordBtn);
            mainElements.Add(signUpBtn);
            return mainElements;
        }

        public DashboardPage DoLogin(string email,string password)
        {
            emailInput.Clear();
            emailInput.SendKeys(email);
            passwordInput.Clear();
            passwordInput.SendKeys(password);
            loginBtn.Click();
            return new DashboardPage(TestBase.driver);
        }
        public SignUpPage GetSignUp()
        {
            signUpBtn.Click();
            return new SignUpPage(TestBase.driver);
        }

        public ForgotPasswordPage GetForgotPassword()
        {
            forgotYourPasswordBtn.Click();
            return new ForgotPasswordPage(TestBase.driver);
        }
        public HomePage ClickCancel()
        {
            cancelBtn.Click();
            return new HomePage(TestBase.driver);
        }
        public HomePage ClickHome()
        {
            homeBtn.Click();
            return new HomePage(TestBase.driver);
        }
    }
}
