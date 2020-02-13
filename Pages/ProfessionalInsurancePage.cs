﻿using OpenQA.Selenium;
using RCoS;
using Helpers;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Helpers.RadioButtonEnum;
using System.IO;

namespace Pages
{
    class ProfessionalInsurancePage
    {
        public ProfessionalInsurancePage(IWebDriver driver)
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
        [FindsBy(How = How.XPath, Using = "//h4[text()='Professional Indemnity Insurance']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-card-content//div[@class='panel']")]
        public IWebElement infoPanel { get; set; }

        [FindsBy(How = How.Id, Using = "mat-checkbox-1")]
        public IWebElement indemnityArrangementsCheckbox { get; set; }

        [FindsBy(How = How.Id, Using = "mat-checkbox-2")]
        public IWebElement disclosedNatureCheckbox { get; set; }

        [FindsBy(How = How.Id, Using = "mat-radio-2")]
        public IWebElement yesPracticeRadio { get; set; }

        [FindsBy(How = How.Id, Using = "mat-radio-3")]
        public IWebElement noPracticeRadio { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='file']")]
        public IWebElement uploadInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement saveBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'required') or contains(text(),'At least one file')]")]
        public IList<IWebElement> requiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Professional Indemnity Insurance')]//i[contains(@class,'far')]")]
        public IWebElement statusIndicator { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Professional indemnity insurance was successfully')]")]
        public IWebElement professionalInsuranceSubmitedMsg { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(infoPanel);
            mainElements.Add(title);
            mainElements.Add(indemnityArrangementsCheckbox);
            mainElements.Add(disclosedNatureCheckbox);
            mainElements.Add(yesPracticeRadio);
            mainElements.Add(noPracticeRadio);
            mainElements.Add(uploadInput);
            mainElements.Add(saveBtn);
            return mainElements;
        }

        public void CompleteForm(ProfessionalInsuranceRadio radio, string filename)
        {
            string fileExtension = "png";
            if (!indemnityArrangementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"))
                indemnityArrangementsCheckbox.Click();
            if (!disclosedNatureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"))
                disclosedNatureCheckbox.Click();
            if (radio.Equals(ProfessionalInsuranceRadio.Yes))
                yesPracticeRadio.Click();
            else
                noPracticeRadio.Click();
            if (filename.Length > 0)
            { 
                TestBase.uploadField(filename, fileExtension);
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mat-input-1")));
            }
            saveBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalInsuranceSubmitedMsg));
        }
        public void CompleteForm(ProfessionalInsuranceRadio radio)
        {
            CompleteForm(radio, "");
        }
    }
}