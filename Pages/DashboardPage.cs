﻿using OpenQA.Selenium;
using RCoS;
using Pages;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class DashboardPage
    {
        public DashboardPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//mat-nav-list")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.XPath, Using = "//title[text()='RCS.Cosmetics.Web']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Account Details')]")]
        public IWebElement accountDetailsBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Probity Statements')]")]
        public IWebElement probityStatementsBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Professional Indemnity Insurance')]")]
        public IWebElement professionalInsuranceBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Professional Behaviours')]")]
        public IWebElement professionalBehavioursBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Revalidation')]")]
        public IWebElement revalidationBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Operative Exposure')]")]
        public IWebElement operativeExposureBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Clinical Outcomes')]")]
        public IWebElement clinicalOutcomesBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Continuing Professional Development')]")]
        public IWebElement professionalDevelopmentBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Reflection on Practice')]")]
        public IWebElement practiceBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'References')]")]
        public IWebElement referencesBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Submit Application')]")]
        public IWebElement submitAppBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Registration Requests')]")]
        public IWebElement registrationRequestsBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='user-details-block']//h5")]
        public IWebElement username { get; set; }

        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'menu-icon')]")]
        public IWebElement sidebarMenuBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'sign-out')]")]
        public IWebElement signOutBtn { get; set; }

        [FindsBy(How = How.LinkText, Using = "Home")]
        public IWebElement homeBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-sidenav")]
        public IWebElement sidebar { get; set; }

        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'sign-out')]")]
        public IWebElement logoutBtn { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(sidebarMenuBtn);
            mainElements.Add(username);
            mainElements.Add(signOutBtn);
            mainElements.Add(accountDetailsBtn);
            mainElements.Add(homeBtn);
            return mainElements;
        }
        public IList<IWebElement> GetAllElements()
        {
            mainElements.Add(sidebarMenuBtn);
            mainElements.Add(username);
            mainElements.Add(signOutBtn);
            mainElements.Add(accountDetailsBtn);
            mainElements.Add(homeBtn);
            mainElements.Add(probityStatementsBtn);
            mainElements.Add(professionalInsuranceBtn);
            mainElements.Add(professionalBehavioursBtn);
            mainElements.Add(revalidationBtn);
            mainElements.Add(operativeExposureBtn);
            mainElements.Add(clinicalOutcomesBtn);
            mainElements.Add(professionalDevelopmentBtn);
            mainElements.Add(practiceBtn);
            mainElements.Add(referencesBtn);
            mainElements.Add(submitAppBtn);

            return mainElements;
        }

        public AccountDetailsPage getAccountDetails()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(accountDetailsBtn));
            accountDetailsBtn.Click();
            return new AccountDetailsPage(TestBase.driver);
        }
        public ProbityStatementsPage getProbityStatements()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(probityStatementsBtn));
            probityStatementsBtn.Click();
            return new ProbityStatementsPage(TestBase.driver);
        }
        public ProfessionalInsurancePage getProfessionalInsurance()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalInsuranceBtn));
            professionalInsuranceBtn.Click();
            return new ProfessionalInsurancePage(TestBase.driver);
        } 
        public ProfessionalBehavioursPage getProfessionalBehaviours()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalBehavioursBtn));
            professionalBehavioursBtn.Click();
            return new ProfessionalBehavioursPage(TestBase.driver);
        }
        public RevalidationPage getRevalidation()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(revalidationBtn));
            revalidationBtn.Click();
            return new RevalidationPage(TestBase.driver);
        }
        public OperativeExposurePage getOperativeExposure()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(operativeExposureBtn));
            operativeExposureBtn.Click();
            return new OperativeExposurePage(TestBase.driver);
        }
        public ClinicalOutcomesPage getClinicalOutcomes()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(clinicalOutcomesBtn));
            clinicalOutcomesBtn.Click();
            return new ClinicalOutcomesPage(TestBase.driver);
        }
        public ContinuingDevelopmentPage getContinuingDevelopment()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalDevelopmentBtn));
            professionalDevelopmentBtn.Click();
            return new ContinuingDevelopmentPage(TestBase.driver);
        }
        public ReferencesPage getReferences()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(referencesBtn));
            referencesBtn.Click();
            return new ReferencesPage(TestBase.driver);
        }
        public RegistrationRequestsPage getRegistrationRequests()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(registrationRequestsBtn));
            registrationRequestsBtn.Click();
            return new RegistrationRequestsPage(TestBase.driver);
        }
        public HomePage logout()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(logoutBtn));
            logoutBtn.Click();
            return new HomePage(TestBase.driver);
        }

        public void openSideMenuIfClosed()
        {
            if (TestBase.driver.Manage().Window.Size.Width < 1200 && !TestBase.ElementIsPresent(sidebar)){
                sidebarMenuBtn.Click();
            }
        }
    }
}
