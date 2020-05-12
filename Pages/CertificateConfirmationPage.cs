using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class CertificateConfirmationPage
    {
        public CertificateConfirmationPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//app-certificates//form")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'required')]")]
        public IList<IWebElement> requiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-checkbox")]
        public IList<IWebElement> checkboxes { get; set; }

        [FindsBy(How = How.XPath, Using = "//h4[text()='Submition']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic breast surgery')]/ancestor::mat-checkbox")]
        public IWebElement breastSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic nasal surgery')]/ancestor::mat-checkbox")]
        public IWebElement nasalSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic Surgery of the periorbital region')]/ancestor::mat-checkbox")]
        public IWebElement periorbitalSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic surgery of the ear')]/ancestor::mat-checkbox")]
        public IWebElement earSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic facial contouring surgery')]/ancestor::mat-checkbox")]
        public IWebElement facialSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic surgery of the face')]/ancestor::mat-checkbox")]
        public IWebElement faceSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic surgery of the face/nose/periorbital region/ears')]/ancestor::mat-checkbox")]
        public IWebElement earsSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic body contouring surgery')]/ancestor::mat-checkbox")]
        public IWebElement bodyCountouringCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Supplementary certificate in body contouring')]/ancestor::mat-checkbox")]
        public IWebElement massiveWeightLossCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic Surgery (all areas covered by the scheme)')]/ancestor::mat-checkbox")]
        public IWebElement cosmeticSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement submitBtn { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();
        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(breastSurgeryCheckbox);
            mainElements.Add(nasalSurgeryCheckbox);
            mainElements.Add(periorbitalSurgeryCheckbox);
            mainElements.Add(earSurgeryCheckbox);
            mainElements.Add(facialSurgeryCheckbox);
            mainElements.Add(faceSurgeryCheckbox);
            mainElements.Add(earsSurgeryCheckbox);
            mainElements.Add(bodyCountouringCheckbox);
            mainElements.Add(massiveWeightLossCheckbox);
            mainElements.Add(cosmeticSurgeryCheckbox);
            mainElements.Add(submitBtn);
            return mainElements;
        }

        public ApplicationThankYouPage Submit()
        {
            submitBtn.Click();
            return new ApplicationThankYouPage(TestBase.driver);
        }
    }
}
