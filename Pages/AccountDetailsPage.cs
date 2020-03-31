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
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-account-details//form")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'required')]")]
        public IList<IWebElement> requiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Account Details')]/..//i[contains(@class,'far')]")]
        public IWebElement statusIndicator { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'The length must not exceed')]")]
        public IList<IWebElement> limitReachedMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='certifications']//div[contains(@class,'error')]")]
        public IWebElement certificationsRequiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),' GMC Number must contain 7 digits')]")]
        public IWebElement gmcNumberMinCharValidation { get; set; }

        [FindsBy(How = How.XPath, Using = "//h4[text()='Account Details']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='title']")]
        public IWebElement titleInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='firstName']")]
        public IWebElement firstNameInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='lastName']")]
        public IWebElement lastNameInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='birthday']")]
        public IWebElement dateInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='address']")]
        public IWebElement addressInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='email']")]
        public IWebElement emailInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='phoneNumber']")]
        public IWebElement phoneInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-label[text()='Gender']/ancestor::mat-form-field//mat-select")]
        public IWebElement genderSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-label[text()='GMC specialty']/ancestor::mat-form-field//mat-select")]
        public IWebElement gmcSpecialitySelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='gmcNumber']")]
        public IWebElement gmcNumberInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-label[text()='Career Grade']/ancestor::mat-form-field//mat-select")]
        public IWebElement careerGradeSelect { get; set; }

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

        [FindsBy(How = How.XPath, Using = "//span[text()=' Cosmetic Surgery ']/ancestor::mat-checkbox")]
        public IWebElement cosmeticSurgeryCheckbox { get; set; }

        [FindsBy(How = How.Id, Using = "mat-expansion-panel-header-0")]
        public IWebElement inforPanel { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='mat-expansion-panel-body']")]
        public IWebElement inforPanelContent { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement submitBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'account-submitted')]")]
        public IWebElement accountSubmittedMsg { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();
        public IList<IWebElement> evaluatorMainElements = new List<IWebElement>();

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
            mainElements.Add(bodyCountouringCheckbox);
            mainElements.Add(massiveWeightLossCheckbox);
            mainElements.Add(cosmeticSurgeryCheckbox);
            mainElements.Add(submitBtn);
            return mainElements;
        }
        public IList<IWebElement> GetEvaluatorMainElements()
        {
            evaluatorMainElements.Add(titleInput);
            evaluatorMainElements.Add(firstNameInput);
            evaluatorMainElements.Add(lastNameInput);
            evaluatorMainElements.Add(dateInput);
            evaluatorMainElements.Add(addressInput);
            evaluatorMainElements.Add(emailInput);
            evaluatorMainElements.Add(phoneInput);
            evaluatorMainElements.Add(genderSelect);
            evaluatorMainElements.Add(gmcNumberInput);
            evaluatorMainElements.Add(gmcSpecialitySelect);
            evaluatorMainElements.Add(careerGradeSelect);
           
            mainElements.Add(submitBtn);
            return evaluatorMainElements;
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
            if (emailInput.GetAttribute("value").Contains("evaluator"))
                submitBtn.Click();
            else
            {
                earsSurgeryCheckbox.Click();
                submitBtn.Click();
            }
        }
    }
}
