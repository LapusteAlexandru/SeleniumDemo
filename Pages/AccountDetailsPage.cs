using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class AccountDetailsPage
    {
        public AccountDetailsPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//app-account-details//form")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'required')]")]
        public IList<IWebElement> requiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Account Details')]//i[contains(@class,'far')]")]
        public IWebElement statusIndicator { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'The length must not exceed')]")]
        public IList<IWebElement> limitReachedMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='certifications']//div[contains(@class,'error')]")]
        public IWebElement certificationsRequiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),' GMC Number must contain 7 digits')]")]
        public IWebElement gmcNumberMinCharValidation { get; set; }

        [FindsBy(How = How.XPath, Using = "//h4[text()='Account Details']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.Id, Using = "mat-input-0")]
        public IWebElement titleInput { get; set; }

        [FindsBy(How = How.Id, Using = "mat-input-1")]
        public IWebElement firstNameInput { get; set; }

        [FindsBy(How = How.Id, Using = "mat-input-2")]
        public IWebElement lastNameInput { get; set; }

        [FindsBy(How = How.Id, Using = "mat-input-3")]
        public IWebElement dateInput { get; set; }

        [FindsBy(How = How.Id, Using = "mat-input-4")]
        public IWebElement addressInput { get; set; }

        [FindsBy(How = How.Id, Using = "mat-input-5")]
        public IWebElement emailInput { get; set; }

        [FindsBy(How = How.Id, Using = "mat-input-6")]
        public IWebElement phoneInput { get; set; }

        [FindsBy(How = How.Id, Using = "mat-select-0")]
        public IWebElement genderSelect { get; set; }

        [FindsBy(How = How.Id, Using = "mat-select-1")]
        public IWebElement gmcSpecialitySelect { get; set; }

        [FindsBy(How = How.Id, Using = "mat-input-7")]
        public IWebElement gmcNumberInput { get; set; }

        [FindsBy(How = How.Id, Using = "mat-select-2")]
        public IWebElement careerGradeSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic breast surgery')]/ancestor::mat-checkbox")]
        public IWebElement breastSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic nasal surgery')]/ancestor::mat-checkbox")]
        public IWebElement nasalSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic Surgery of periorbital region')]/ancestor::mat-checkbox")]
        public IWebElement periorbitalSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic surgery of ear')]/ancestor::mat-checkbox")]
        public IWebElement earSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic facial contouring surgery')]/ancestor::mat-checkbox")]
        public IWebElement facialSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic surgery of the face')]/ancestor::mat-checkbox")]
        public IWebElement faceSurgeryCheckbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cosmetic surgery of the face/nose/periorbital region/ears')]/ancestor::mat-checkbox")]
        public IWebElement earsSurgeryCheckbox { get; set; }

        [FindsBy(How = How.Id, Using = "mat-expansion-panel-header-0")]
        public IWebElement inforPanel { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='mat-expansion-panel-body']")]
        public IWebElement inforPanelContent { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement submitBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'account-submitted')]")]
        public IWebElement accountSubmittedMsg { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(titleInput);
            mainElements.Add(firstNameInput);
            mainElements.Add(lastNameInput);
            mainElements.Add(dateInput);
            mainElements.Add(addressInput);
            mainElements.Add(emailInput);
            mainElements.Add(phoneInput);
            mainElements.Add(genderSelect);
            mainElements.Add(gmcNumberInput);
            mainElements.Add(gmcSpecialitySelect);
            mainElements.Add(careerGradeSelect);
            mainElements.Add(breastSurgeryCheckbox);
            mainElements.Add(nasalSurgeryCheckbox);
            mainElements.Add(periorbitalSurgeryCheckbox);
            mainElements.Add(earSurgeryCheckbox);
            mainElements.Add(facialSurgeryCheckbox);
            mainElements.Add(faceSurgeryCheckbox);
            mainElements.Add(earsSurgeryCheckbox);
            mainElements.Add(submitBtn);
            return mainElements;
        }

        public void CompleteForm(string title,string firstName,string lastName,string address,string phone,string gender,string gmcNumber,string gmcSpeciality,string careerGrade)
        {
            titleInput.Clear();
            titleInput.SendKeys(title);
            firstNameInput.Clear();
            firstNameInput.SendKeys(firstName);
            lastNameInput.Clear();
            lastNameInput.SendKeys(lastName);
            TestBase.driver.FindElement(By.XPath("//mat-datepicker-toggle")).Click();
            TestBase.driver.FindElement(By.XPath("//div[text()='1']")).Click();
            addressInput.Clear();
            addressInput.SendKeys(address);
            phoneInput.Clear();
            phoneInput.SendKeys(phone);
            TestBase.SelectOption("mat-select-0", gender);
            gmcNumberInput.Clear();
            gmcNumberInput.SendKeys(gmcNumber);
            TestBase.SelectOption("mat-select-1", gmcSpeciality);
            TestBase.SelectOption("mat-select-2", careerGrade);
            earsSurgeryCheckbox.Click();
            submitBtn.Click();
        }
    }
}
