using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Pages
{
    class EditApplicationPage
    {
        public EditApplicationPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-edit-application//h4[contains(text(),'Account')]")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[text()='Account Details']/parent::div")]
        public IWebElement accountDetailsTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Probity Statements']/parent::div")]
        public IWebElement probityStatementsTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Professional Indemnity Insurance']/parent::div")]
        public IWebElement professionalInsuranceTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Professional Behaviours']/parent::div")]
        public IWebElement professionalBehavioursTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Revalidation']/parent::div")]
        public IWebElement revalidationTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Operative Exposure']/parent::div")]
        public IWebElement operativeExposureTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Clinical Outcomes']/parent::div")]
        public IWebElement clinicalOutcomesTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Continuing Professional Development']/parent::div")]
        public IWebElement professionalDevelopmentTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Reflection on Practice']/parent::div")]
        public IWebElement practiceTab { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[text()='Reference']/parent::div")]
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
            var selected = accountDetailsTab.GetAttribute("class");
            TestBase.wait.Equals(selected.Contains("mat-tab-label-active"));
            return new AccountDetailsPage(TestBase.driver);
        }
        public ProbityStatementsPage getProbityStatements()
        {
            while (!probityStatementsTab.Displayed)
                navigateRightBtn.Click();
            probityStatementsTab.Click();
            var selected = probityStatementsTab.GetAttribute("class");
            TestBase.wait.Equals(selected.Contains("mat-tab-label-active"));
            return new ProbityStatementsPage(TestBase.driver);
        }
        public ProfessionalInsurancePage getProfessionalInsurance()
        {
            while (!professionalInsuranceTab.Displayed)
                navigateRightBtn.Click();
            professionalInsuranceTab.Click();
            var selected = professionalInsuranceTab.GetAttribute("class");
            TestBase.wait.Equals(selected.Contains("mat-tab-label-active"));
            return new ProfessionalInsurancePage(TestBase.driver);
        }
        public ProfessionalBehavioursPage getProfessionalBehaviours()
        {
            while (!professionalBehavioursTab.Displayed)
                navigateRightBtn.Click();
            professionalBehavioursTab.Click();
            var selected = professionalBehavioursTab.GetAttribute("class");
            TestBase.wait.Equals(selected.Contains("mat-tab-label-active"));
            return new ProfessionalBehavioursPage(TestBase.driver);
        }
        public RevalidationPage getRevalidation()
        {
            while (!revalidationTab.Displayed)
                navigateRightBtn.Click();
            revalidationTab.Click();
            var selected = revalidationTab.GetAttribute("class");
            TestBase.wait.Equals(selected.Contains("mat-tab-label-active"));
            return new RevalidationPage(TestBase.driver);
        }
        public OperationNumbersPage getOperativeExposure()
        {
            while (!operativeExposureTab.Displayed)
                navigateRightBtn.Click();
            operativeExposureTab.Click();
            var selected = operativeExposureTab.GetAttribute("class");
            TestBase.wait.Equals(selected.Contains("mat-tab-label-active"));
            return new OperationNumbersPage(TestBase.driver);
        }
        public ClinicalOutcomesPage getClinicalOutcomes()
        {
            while (!clinicalOutcomesTab.Displayed)
                navigateRightBtn.Click();
            clinicalOutcomesTab.Click();
            var selected = clinicalOutcomesTab.GetAttribute("class");
            TestBase.wait.Equals(selected.Contains("mat-tab-label-active"));
            return new ClinicalOutcomesPage(TestBase.driver);
        }
        public ContinuingDevelopmentPage getProfessionalDevelopment()
        {
            while (!professionalDevelopmentTab.Displayed)
                navigateRightBtn.Click();
            professionalDevelopmentTab.Click();
            var selected = professionalDevelopmentTab.GetAttribute("class");
            TestBase.wait.Equals(selected.Contains("mat-tab-label-active"));
            return new ContinuingDevelopmentPage(TestBase.driver);
        }
        public ReflectionOnPracticePage getReflectionOnPractice()
        {
            while (!practiceTab.Displayed)
                navigateRightBtn.Click();
            practiceTab.Click();
            var selected = practiceTab.GetAttribute("class");
            TestBase.wait.Equals(selected.Contains("mat-tab-label-active"));
            return new ReflectionOnPracticePage(TestBase.driver);
        }
        public ReferencesPage getReference()
        {
            while (!referenceTab.Displayed)
                navigateRightBtn.Click();
            referenceTab.Click();
            var selected = referenceTab.GetAttribute("class");
            TestBase.wait.Equals(selected.Contains("mat-tab-label-active"));
            return new ReferencesPage(TestBase.driver);
        }

    }
}
