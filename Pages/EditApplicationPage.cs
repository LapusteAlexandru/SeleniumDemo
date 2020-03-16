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
    class EditApplicationPage
    {
        public EditApplicationPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//app-edit-application")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[text()='Account Details']")]
        public IWebElement accountDetailsTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Probity Statements']")]
        public IWebElement probityStatementsTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Professional Indemnity Insurance']")]
        public IWebElement professionalInsuranceTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Professional Behaviours']")]
        public IWebElement professionalBehavioursTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Revalidation']")]
        public IWebElement revalidationTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Operative Exposure']")]
        public IWebElement operativeExposureTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Clinical Outcomes']")]
        public IWebElement clinicalOutcomesTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Continuing Professional Development']")]
        public IWebElement professionalDevelopmentTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Reflection on Practice']")]
        public IWebElement practiceTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Reference']")]
        public IWebElement referenceTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'mat-tab-header-pagination-before')]")]
        public IWebElement navigateLeftBtn { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'mat-tab-header-pagination-after')]")]
        public IWebElement navigateRightBtn { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
           
            mainElements.Add(accountDetailsTab);
            mainElements.Add(probityStatementsTab);
            mainElements.Add(professionalInsuranceTab);
            mainElements.Add(professionalBehavioursTab);
            mainElements.Add(revalidationTab);
            mainElements.Add(accountDetailsTab);
            mainElements.Add(operativeExposureTab);
            mainElements.Add(clinicalOutcomesTab);
            mainElements.Add(professionalDevelopmentTab);
            mainElements.Add(practiceTab);
            mainElements.Add(referenceTab);
            return mainElements;
        }

        public AccountDetailsPage getAccountDetails()
        {
            while (!accountDetailsTab.Displayed)
                navigateRightBtn.Click();
            accountDetailsTab.Click();
            return new AccountDetailsPage(TestBase.driver);
        }
        public ProbityStatementsPage getProbityStatements()
        {
            while (!probityStatementsTab.Displayed)
                navigateRightBtn.Click();
            probityStatementsTab.Click();
            return new ProbityStatementsPage(TestBase.driver);
        }
        public ProfessionalInsurancePage getProfessionalInsurance()
        {
            while (!professionalInsuranceTab.Displayed)
                navigateRightBtn.Click();
            professionalInsuranceTab.Click();
            return new ProfessionalInsurancePage(TestBase.driver);
        }
        public ProfessionalBehavioursPage getProfessionalBehaviours()
        {
            while (!professionalBehavioursTab.Displayed)
                navigateRightBtn.Click();
            professionalBehavioursTab.Click();
            return new ProfessionalBehavioursPage(TestBase.driver);
        }
        public RevalidationPage getRevalidation()
        {
            while (!revalidationTab.Displayed)
                navigateRightBtn.Click();
            revalidationTab.Click();
            return new RevalidationPage(TestBase.driver);
        }
        public OperationNumbersPage getOperativeExposure()
        {
            while (!operativeExposureTab.Displayed)
                navigateRightBtn.Click();
            operativeExposureTab.Click();
            return new OperationNumbersPage(TestBase.driver);
        }
        public ClinicalOutcomesPage getClinicalOutcomes()
        {
            while (!clinicalOutcomesTab.Displayed)
                navigateRightBtn.Click();
            clinicalOutcomesTab.Click();
            return new ClinicalOutcomesPage(TestBase.driver);
        }
        public ContinuingDevelopmentPage getProfessionalDevelopment()
        {
            while (!professionalDevelopmentTab.Displayed)
                navigateRightBtn.Click();
            professionalDevelopmentTab.Click();
            return new ContinuingDevelopmentPage(TestBase.driver);
        }
        public ReflectionOnPracticePage getReflectionOnPractice()
        {
            while (!practiceTab.Displayed)
                navigateRightBtn.Click();
            practiceTab.Click();
            return new ReflectionOnPracticePage(TestBase.driver);
        }
        public ReferencesPage getReference()
        {
            while (!referenceTab.Displayed)
                navigateRightBtn.Click();
            referenceTab.Click();
            return new ReferencesPage(TestBase.driver);
        }

    }
}
