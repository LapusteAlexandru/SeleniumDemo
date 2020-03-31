using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class PaymentPage
    {
        public PaymentPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//app-payment//form")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//app-payment//form")]
        public IWebElement paymentForm { get; set; }
        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='cardNumber']")]
        private IWebElement cardNumberInput { get; set; }
        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='firstName']")]
        private IWebElement firstNameInput { get; set; }
        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='lastName']")]
        private IWebElement lastNameInput { get; set; }
        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='cardExpiry']")]
        private IWebElement dateInput { get; set; }
        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='cardCodeVerification']")]
        private IWebElement cvvInput { get; set; }
        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement submitBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'required')]")]
        public IList<IWebElement> requiredMsgs { get; set; }
        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'Card Number is too short')]")]
        public IWebElement shortCardNumberMsg { get; set; }
        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'CVV is too short')]")]
        public IWebElement shortCVVMsg { get; set; }
        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'The length must not exceed')]")]
        public IList<IWebElement> limitReachedMsgs { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();
        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(cardNumberInput);
            mainElements.Add(firstNameInput);
            mainElements.Add(lastNameInput);
            mainElements.Add(dateInput);
            mainElements.Add(cvvInput);
            mainElements.Add(submitBtn);
            
            return mainElements;
        }
        public ApplicationThankYouPage Submit(string cardNumber,string firstName,string lastName,string expiryMonthdate,string expiryYearDate,string cvv)
        {
            cardNumberInput.Clear();
            cardNumberInput.SendKeys(cardNumber);
            firstNameInput.Clear();
            firstNameInput.SendKeys(firstName);
            lastNameInput.Clear();
            lastNameInput.SendKeys(lastName);
            dateInput.Clear();
            dateInput.SendKeys(expiryMonthdate);
            dateInput.SendKeys(expiryYearDate);
            cvvInput.Clear();
            cvvInput.SendKeys(cvv);
            submitBtn.Click();
            return new ApplicationThankYouPage(TestBase.driver);
        }
    }
}
