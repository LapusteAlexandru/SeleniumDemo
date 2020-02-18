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

            ProfessionalBehavioursPage obj = ClickOn("OperativeExposurePage");
            Assert.That(obj.title.Displayed);
        }
        [Test]
        public void TestClickClinicalOutcomes()
        {

            ProfessionalBehavioursPage obj = ClickOn("ClinicalOutcomesPage");
            Assert.That(obj.title.Displayed);
        }
        [Test]
        public void TestClickContinuingDevelopmentP()
        {

            ProfessionalBehavioursPage obj = ClickOn("ContinuingDevelopmentPage");
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
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            if (TestBase.ElementIsPresent(dp.sidebar))
                dp.sidebarMenuBtn.Click();
            else { 
                dp.sidebarMenuBtn.Click();
                Thread.Sleep(500);
                dp.sidebarMenuBtn.Click();
                
            }
            Thread.Sleep(500);
            Assert.That(!dp.sidebar.Displayed);
        }
        [Test]
        public void TestShowSidebar()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            if (!TestBase.ElementIsPresent(dp.sidebar))
                dp.sidebarMenuBtn.Click();
            else
            {
                dp.sidebarMenuBtn.Click();
                Thread.Sleep(500);
                dp.sidebarMenuBtn.Click();
            }
            Thread.Sleep(500);
            Assert.That(dp.sidebar.Displayed);
        }

        public dynamic ClickOn(string page)
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            switch (page)
            {
                case "AccountDetailsPage":
                    return dp.getAccountDetails();
                case "ProbityStatementsPage":
                    return dp.getProbityStatements();
                case "ProfessionalInsurancePage":
                    return dp.getProfessionalInsurance();
                case "ProfessionalBehavioursPage":
                    return dp.getProfessionalBehaviours();
                case "RevalidationPage":
                    return dp.getRevalidation();
                case "OperativeExposurePage":
                    return dp.getOperativeExposure();
                case "ClinicalOutcomesPage":
                    return dp.getClinicalOutcomes();
                case "ContinuingDevelopmentPage":
                    return dp.getContinuingDevelopment();
                case "ReferencesPage":
                    return dp.getReferences();

                default:
                    return null;
            }
        }
    }
}
