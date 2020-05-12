using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class NewApplicationPage
    {
        public NewApplicationPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//app-new-application-dialog")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }



        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Empty')]/parent::button")]
        public IWebElement emptyBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Pre-filled')]/parent::button")]
        public IWebElement prefilledBtn { get; set; }


        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(emptyBtn);
            mainElements.Add(prefilledBtn);
            return mainElements;
        }

        public PaymentCheckPage CreateFilledApplication()
        {
            string paymentCheckBtn = "//div[contains(text(),'Payment Check')]";
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(prefilledBtn));
            IWebElement earsSurgeryCheckbox = TestBase.driver.FindElement(By.XPath("//span[contains(text(),'Cosmetic surgery of the face/nose/periorbital region/ears')]/ancestor::mat-checkbox"));
            earsSurgeryCheckbox.Click();
            prefilledBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(paymentCheckBtn)));
            TestBase.driver.FindElement(By.XPath(paymentCheckBtn)).Click();
            return new PaymentCheckPage(TestBase.driver);
        }
        public PaymentCheckPage CreateEmptyApplication()
        {
            string paymentCheckBtn = "//div[contains(text(),'Payment Check')]";
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(prefilledBtn));
            IWebElement earsSurgeryCheckbox = TestBase.driver.FindElement(By.XPath("//span[contains(text(),'Cosmetic surgery of the face/nose/periorbital region/ears')]/ancestor::mat-checkbox"));
            earsSurgeryCheckbox.Click();
            emptyBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(paymentCheckBtn)));
            TestBase.driver.FindElement(By.XPath(paymentCheckBtn)).Click();
            return new PaymentCheckPage(TestBase.driver);
        }
    }
}
