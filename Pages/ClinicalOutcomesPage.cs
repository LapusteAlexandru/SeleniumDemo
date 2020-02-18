using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class ClinicalOutcomesPage
    {
        public ClinicalOutcomesPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//h4[text()='Clinical Outcomes']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-card-content//div[@class='panel']")]
        public IWebElement infoPanel { get; set; }

        [FindsBy(How = How.Id, Using = "mat-checkbox-1")]
        public IWebElement CMARequirementsCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='file']/ancestor::button")]
        public IWebElement uploadInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement saveBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'required') or contains(text(),'At least one file')]")]
        public IList<IWebElement> requiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Clinical Outcomes')]//i[contains(@class,'far')]")]
        public IWebElement statusIndicator { get; set; }
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Clinical outcomes were successfully')]")]
        public IWebElement pageSubmitedMsg { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(infoPanel);
            mainElements.Add(title);
            mainElements.Add(CMARequirementsCheckbox);
            mainElements.Add(uploadInput);
            mainElements.Add(saveBtn);
            return mainElements;
        }

        public void CompleteForm(string filename)
        {
            if (!CMARequirementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"))
                CMARequirementsCheckbox.Click();

            string fileExtension = "png";
            TestBase.uploadField(filename, fileExtension);
            saveBtn.Click();
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(pageSubmitedMsg));
            }
            catch (NoSuchElementException e) { Console.WriteLine(e); }
        }
    }
}
