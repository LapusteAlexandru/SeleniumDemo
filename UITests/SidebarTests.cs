﻿using NUnit.Framework;
using Pages;
using RCoS;
using System.Threading;

namespace SidebarTests
{
    [TestFixture]
    [Category("Sidebar")]
    class SidebarTests
    {
        [SetUp]
        public void Setup()
        {
            TestBase.RootInit();
        }

        [TearDown]
        public void Teardown()
        {
            TestBase.TakeScreenShot();
            TestBase.driver.Quit();
        }

        [Test]
        public void TestClickAccountDetails()
        {

            AccountDetailsPage obj = ClickOn("AccountDetailsPage");
            Assert.That(obj.title.Displayed);
        }
        [Test]
        public void TestClickProbityStatements()
        {
            ProbityStatementsPage obj = ClickOn("ProbityStatementsPage");
            Assert.That(obj.title.Displayed);
        }
        [Test]
        public void TestClickProfessionalInsurance()
        {
            ProfessionalInsurancePage obj = ClickOn("ProfessionalInsurancePage");
            Assert.That(obj.title.Displayed);
        }
        [Test]
        public void TestClickProfessionalBehaviours()
        {

            ProfessionalBehavioursPage obj = ClickOn("ProfessionalBehavioursPage");
            Assert.That(obj.title.Displayed);
        }
        [Test]
        public void TestClickRevalidation()
        {

            RevalidationPage obj = ClickOn("RevalidationPage");
            Assert.That(obj.title.Displayed);
        }
        [Test]
        public void TestClickOperativeExposure()
        {

            OperativeExposurePage obj = ClickOn("OperativeExposurePage");
            Assert.That(obj.title.Displayed);
        }
        [Test]
        public void TestClickClinicalOutcomes()
        {

            ClinicalOutcomesPage obj = ClickOn("ClinicalOutcomesPage");
            Assert.That(obj.title.Displayed);
        }
        [Test]
        public void TestClickContinuingDevelopmentP()
        {

            ContinuingDevelopmentPage obj = ClickOn("ContinuingDevelopmentPage");
            Assert.That(obj.title.Displayed);
        }
        [Test]
        public void TestClickReferences()
        {
            ReferencesPage obj = ClickOn("ReferencesPage");
            Assert.That(obj.title.Displayed);
        }
        [Test]
        public void TestHideSidebar()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            if (TestBase.ElementIsPresent(dashboardPage.sidebar))
                dashboardPage.sidebarMenuBtn.Click();
            else { 
                dashboardPage.sidebarMenuBtn.Click();
                Thread.Sleep(500);
                dashboardPage.sidebarMenuBtn.Click();
                
            }
            Thread.Sleep(500);
            Assert.That(!dashboardPage.sidebar.Displayed);
        }
        [Test]
        public void TestShowSidebar()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            if (!TestBase.ElementIsPresent(dashboardPage.sidebar))
                dashboardPage.sidebarMenuBtn.Click();
            else
            {
                dashboardPage.sidebarMenuBtn.Click();
                Thread.Sleep(500);
                dashboardPage.sidebarMenuBtn.Click();
            }
            Thread.Sleep(500);
            Assert.That(dashboardPage.sidebar.Displayed);
        }

        public dynamic ClickOn(string page)
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            switch (page)
            {
                case "AccountDetailsPage":
                    return dashboardPage.getAccountDetails();
                case "ProbityStatementsPage":
                    return dashboardPage.getProbityStatements();
                case "ProfessionalInsurancePage":
                    return dashboardPage.getProfessionalInsurance();
                case "ProfessionalBehavioursPage":
                    return dashboardPage.getProfessionalBehaviours();
                case "RevalidationPage":
                    return dashboardPage.getRevalidation();
                case "OperativeExposurePage":
                    return dashboardPage.getOperativeExposure();
                case "ClinicalOutcomesPage":
                    return dashboardPage.getClinicalOutcomes();
                case "ContinuingDevelopmentPage":
                    return dashboardPage.getContinuingDevelopment();
                case "ReferencesPage":
                    return dashboardPage.getReferences();

                default:
                    return null;
            }
        }
    }
}
