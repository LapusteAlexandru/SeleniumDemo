﻿using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pages
{
    class ProfessionalBehavioursPage
    {
        public ProfessionalBehavioursPage(IWebDriver driver)
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
        [FindsBy(How = How.XPath, Using = "//h4[text()='Professional Behaviours']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-card-content//div[@class='panel']")]
        public IWebElement infoPanel { get; set; }

        [FindsBy(How = How.Id, Using = "mat-checkbox-1")]
        public IWebElement professionalResponsibilitiesCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='file']")]
        public IWebElement uploadInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement saveBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'required') or contains(text(),'At least one file')]")]
        public IList<IWebElement> requiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Professional Behaviours')]//i[contains(@class,'far')]")]
        public IWebElement statusIndicator { get; set; }
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Professional behaviours was successfully')]")]
        public IWebElement professionalBehavioursSubmintedMsg { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(infoPanel);
            mainElements.Add(title);
            mainElements.Add(professionalResponsibilitiesCheckbox);
            mainElements.Add(uploadInput);
            mainElements.Add(saveBtn);
            return mainElements;
        }

        public void CompleteForm(string filename)
        {
            if (!professionalResponsibilitiesCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"))
                professionalResponsibilitiesCheckbox.Click();

            string fileExtension = "png";
            TestBase.uploadField(filename, fileExtension);
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mat-input-1")));
            saveBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalBehavioursSubmintedMsg));
        }
    }
}