using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class PaymentCheckPage
    {
        public PaymentCheckPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-payment-check//form")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'required')]")]
        public IList<IWebElement> requiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//h4[text()='Payment Check']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'If you have previously attended')]/ancestor::mat-radio-button")]
        public IWebElement alreadyPaidRadio { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'If you have attended')]/ancestor::mat-radio-button")]
        public IWebElement haventPaidRadio { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement submitBtn { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();
        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(alreadyPaidRadio);
            mainElements.Add(haventPaidRadio);
            mainElements.Add(submitBtn);
            return mainElements;
        }

        public ApplicationThankYouPage getThankYou()
        {
            alreadyPaidRadio.Click();
            submitBtn.Click();
            return new ApplicationThankYouPage(TestBase.driver);
        }
        public PaymentPage getPayment()
        {
            haventPaidRadio.Click();
            submitBtn.Click();
            return new PaymentPage(TestBase.driver);
        }
    }
}
