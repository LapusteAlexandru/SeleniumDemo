﻿using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Helpers.RadioButtonEnum;

namespace Pages
{
    class ProbityStatementsPage
    {
        public ProbityStatementsPage(IWebDriver driver)
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
        [FindsBy(How = How.XPath, Using = "//h4[text()='Probity Statements']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-card-content//div[@class='mb-3']")]
        public IWebElement infoPanel { get; set; }

        [FindsBy(How = How.Id, Using = "mat-checkbox-1")]
        public IWebElement professionalObligationsCheckbox { get; set; }

        [FindsBy(How = How.Id, Using = "mat-checkbox-2")]
        public IWebElement suspensionCheckbox { get; set; }

        [FindsBy(How = How.Id, Using = "mat-radio-2")]
        public IWebElement nothingToDeclareRadio { get; set; }

        [FindsBy(How = How.Id, Using = "mat-radio-3")]
        public IWebElement somethingToDeclareRadio { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement saveBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'required')]")]
        public IList<IWebElement> requiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Probity Statements')]//i[contains(@class,'far')]")]
        public IWebElement statusIndicator { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(infoPanel);
            mainElements.Add(professionalObligationsCheckbox);
            mainElements.Add(suspensionCheckbox);
            mainElements.Add(nothingToDeclareRadio);
            mainElements.Add(somethingToDeclareRadio);
            mainElements.Add(saveBtn);
            return mainElements;
        }

        public void CompleteForm(Radio radio)
        {
            if(!professionalObligationsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"))
                professionalObligationsCheckbox.Click();
            if(!suspensionCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"))
                suspensionCheckbox.Click();
            if (radio.Equals(Radio.Nothing))
                nothingToDeclareRadio.Click();
            else
                somethingToDeclareRadio.Click();
            saveBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Account details were successfully modified')]")));
        }
    }
}