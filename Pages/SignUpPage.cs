using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Pages
{
    class SignUpPage
    {
        public SignUpPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form[@name='registryForm']")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.Id, Using = "Username")]
        public IWebElement emailInput { get; set; }
        
        [FindsBy(How = How.Name, Using = "password")]
        public IWebElement passwordInput { get; set; }
        
        [FindsBy(How = How.Name, Using = "confirmPassword")]
        public IWebElement confirmPasswordInput { get; set; }
        
        [FindsBy(How = How.Name, Using = "termsAndConditions")]
        public IWebElement tosCheckbox { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@ng-click='openTermsDialog()']")]
        public IWebElement tosDialog { get; set; }
        
        [FindsBy(How = How.Name, Using = "userPrivacyNotice")]
        public IWebElement userPrivacyCheckbox { get; set; }
        
        [FindsBy(How = How.Name, Using = "evaluatorPrivacyNotice")]
        public IWebElement evaluatorPrivacyCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-click='openUserPrivacyNoticeDialog()']")]
        public IWebElement userPrivacyDialog { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-click='openEvaluatorPrivacyNoticeDialog()']")]
        public IWebElement evaluatorPrivacyDialog { get; set; }
        
        [FindsBy(How = How.Id, Using = "agree_button")]
        public IWebElement iAgreeBtn { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//a[text()='Terms and Conditions']")]
        public IWebElement tosBtn { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//a[text()='Privacy Notice']")]
        public IWebElement privacyBtn { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//button[@value='register']")]
        public IWebElement registerBtn { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//a[@value='cancel']")]
        public IWebElement cancelBtn { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//a[text()='Already have an account?']")]
        public IWebElement loginBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//h1[text()='Not yet registered?']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-messages='registryForm.password.$error']//div[@ng-message='required']")]
        public IWebElement passwordRequiredMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-messages='registryForm.confirmPassword.$error']//div[@ng-message='required']")]
        public IWebElement confirmPasswordRequiredMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-messages='registryForm.username.$error']//div[@ng-message='required']")]
        public IWebElement emailRequiredMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-messages='registryForm.username.$error']//div[@ng-message='pattern']")]
        public IWebElement emailInvalidMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-messages='registryForm.termsAndConditions.$error']//div[@ng-message='required']")]
        public IWebElement tosRequiredMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(@ng-messages,'PrivacyNotice.$error')]//div[@ng-message='required']")]
        public IWebElement privacyRequiredMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@ng-message='compareTo']")]
        public IWebElement passwordsDontMatchMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[text()='Passwords must be at least 8 characters.']")]
        public IWebElement passwordMinLengthMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[text()='Passwords must have at least one non alphanumeric character.']")]
        public IWebElement passwordAlphanumericMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[text()=\"Passwords must have at least one lowercase ('a'-'z').\"]")]
        public IWebElement passwordLowercaseMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[text()=\"Passwords must have at least one uppercase ('A'-'Z').\"]")]
        public IWebElement passwordUppercaseMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[text()=\"Passwords must have at least one digit ('0'-'9').\"]")]
        public IWebElement passwordNumberMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//li[contains(text(),\"is already taken\")]")]
        public IWebElement emailTakenMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//h2[contains(text(),\"Which country's laws apply to any disputes?\")]")]
        public IWebElement tosLastSection { get; set; }

        [FindsBy(How = How.XPath, Using = "//b[contains(text(),\"Purpose:\")]")]
        public IWebElement privacyLastSection { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();
        public IList<IWebElement> evaluatorMainElements = new List<IWebElement>();
        public IList<IWebElement> requiredElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(emailInput);
            mainElements.Add(passwordInput);
            mainElements.Add(confirmPasswordInput);
            mainElements.Add(tosCheckbox);
            mainElements.Add(tosBtn);
            mainElements.Add(userPrivacyCheckbox);
            mainElements.Add(privacyBtn);
            mainElements.Add(registerBtn);
            mainElements.Add(cancelBtn);
            mainElements.Add(loginBtn);
            return mainElements;
        }
        
        public IList<IWebElement> GetEvaluatorMainElements()
        {
            evaluatorMainElements.Add(emailInput);
            evaluatorMainElements.Add(passwordInput);
            evaluatorMainElements.Add(confirmPasswordInput);
            evaluatorMainElements.Add(tosCheckbox);
            evaluatorMainElements.Add(tosBtn);
            evaluatorMainElements.Add(evaluatorPrivacyCheckbox);
            evaluatorMainElements.Add(privacyBtn);
            evaluatorMainElements.Add(registerBtn);
            return evaluatorMainElements;
        }
        public IList<IWebElement> GetRequiredElements()
        {
            requiredElements.Add(emailRequiredMsg);
            requiredElements.Add(passwordRequiredMsg);
            requiredElements.Add(confirmPasswordRequiredMsg);
            requiredElements.Add(tosRequiredMsg);
            requiredElements.Add(privacyRequiredMsg);
            return requiredElements;
        }

        public LoginPage AlreadyHaveAccount()
        {
            loginBtn.Click();
            return new LoginPage(TestBase.driver);
        }

        public SignUpTyPage DoRegister(string email,string password,string confirmPassword)
        {
            emailInput.Clear();
            emailInput.SendKeys(email);
            passwordInput.Clear();
            passwordInput.SendKeys(password);
            confirmPasswordInput.Clear();
            confirmPasswordInput.SendKeys(confirmPassword);
            tosDialog.Click();
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.Id("agree_button")));
            TestBase.ScrollToElemnent("//li[contains(text(),'jurisdiction of the courts of England')]"); 
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("agree_button")));
            iAgreeBtn.Click();
            if (email.Contains("evaluator"))
            {
                TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(evaluatorPrivacyDialog));
                evaluatorPrivacyDialog.Click();
            }
            else
            {
                TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(userPrivacyDialog));
                userPrivacyDialog.Click();
            }
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.Id("agree_button")));
            TestBase.ScrollToElemnent("//b[contains(text(),'Purpose:')]");
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("agree_button")));
            iAgreeBtn.Click();
            registerBtn.Click();
            return new SignUpTyPage(TestBase.driver);
        }

        public LoginPage ClickCancel()
        {
            cancelBtn.Click();
            return new LoginPage(TestBase.driver);
        }



    }
}
