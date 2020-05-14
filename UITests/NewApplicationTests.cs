using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewApplicationTests
{
    [TestFixture]
    [Category("NewApplication")]
    class NewApplicationTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[Applications]", TestBase.appUsername, "Status", 4);
        }
        [SetUp]
        public void Setup()
        {
            TestBase.RootInit();
        }

        [TearDown]
        public void Teardown()
        {
            TestBase.deleteSectionData("[dbo].[Documents]", TestBase.appUsername);
            TestBase.deleteUserData("[dbo].[Applications]", TestBase.appUsername);

            TestBase.TakeScreenShot();
            TestBase.driver.Quit();
        }

        [Test]
        public void TestPageLoads()
        {
            var (newApp, dashboardPage) = getNewApp();
            foreach (var e in newApp.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestNewFilledApp()
        {

            var (newApp, dashboardPage) = getNewApp();
            PaymentCheckPage paymentCheck = newApp.CreateFilledApplication();
            PaymentThankYouPage thankYou = paymentCheck.getThankYou();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(thankYou.thankYouMsg));
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            foreach (var e in dashboardPage.GetSections())
                CheckAppState(e.GetAttribute("value"),true);
        }
        [Test]
        public void TestNewEmptyApp()
        {

            var (newApp, dashboardPage) = getNewApp();
            PaymentCheckPage paymentCheck = newApp.CreateEmptyApplication();
            PaymentThankYouPage thankYou = paymentCheck.getThankYou();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(thankYou.thankYouMsg));
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            foreach (var e in dashboardPage.GetSections())
                CheckAppState(e.GetAttribute("value"),false);
        }

        private void CheckAppState(string section,bool filled)
        {
            DashboardPage dashboard = new DashboardPage(TestBase.driver);
            if (filled)
            {
                switch (section)
                {
                    case "Probity Statements":
                        ProbityStatementsPage probityStatements = dashboard.getProbityStatements();
                        Assert.True(probityStatements.professionalObligationsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.True(probityStatements.suspensionCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.True(probityStatements.nothingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"));
                        break;
                    case "Professional Indemnity Insurance":
                        ProfessionalInsurancePage professionalInsurance = dashboard.getProfessionalInsurance();
                        Assert.True(professionalInsurance.indemnityArrangementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.True(professionalInsurance.disclosedNatureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.True(professionalInsurance.yesPracticeRadio.GetAttribute("class").Contains("mat-radio-checked"));
                        break;
                    case "Professional Behaviours":
                        ProfessionalBehavioursPage professionalBehaviours = dashboard.getProfessionalBehaviours();
                        Assert.True(professionalBehaviours.professionalResponsibilitiesCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        break;
                    case "Revalidation":
                        RevalidationPage revalidation = dashboard.getRevalidation();
                        Assert.True(revalidation.declareAppraisalYes.GetAttribute("class").Contains("mat-radio-checked"));
                        break;
                    case "Operation Numbers":
                        OperationNumbersPage operationNumbers = dashboard.getOperationNUmbers();
                        Assert.True(operationNumbers.proceduresCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.True(operationNumbers.operativeExposureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        break;
                    case "Clinical Outcomes":
                        ClinicalOutcomesPage clinicalOutcomes = dashboard.getClinicalOutcomes();
                        Assert.True(clinicalOutcomes.CMARequirementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.True(clinicalOutcomes.phinLink.GetAttribute("value").Equals("PHINLink"));
                        break;
                    default:
                        Console.WriteLine("Invalid section");
                        break;

                }
            }
            else
            {
                switch (section)
                {
                    case "Probity Statements":
                        ProbityStatementsPage probityStatements = dashboard.getProbityStatements();
                        Assert.False(probityStatements.professionalObligationsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.False(probityStatements.suspensionCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.False(probityStatements.nothingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"));
                        break;
                    case "Professional Indemnity Insurance":
                        ProfessionalInsurancePage professionalInsurance = dashboard.getProfessionalInsurance();
                        Assert.False(professionalInsurance.indemnityArrangementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.False(professionalInsurance.disclosedNatureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.False(professionalInsurance.yesPracticeRadio.GetAttribute("class").Contains("mat-radio-checked"));
                        break;
                    case "Professional Behaviours":
                        ProfessionalBehavioursPage professionalBehaviours = dashboard.getProfessionalBehaviours();
                        Assert.False(professionalBehaviours.professionalResponsibilitiesCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        break;
                    case "Revalidation":
                        RevalidationPage revalidation = dashboard.getRevalidation();
                        Assert.False(revalidation.declareAppraisalYes.GetAttribute("class").Contains("mat-radio-checked"));
                        break;
                    case "Operation Numbers":
                        OperationNumbersPage operationNumbers = dashboard.getOperationNUmbers();
                        Assert.False(operationNumbers.proceduresCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.False(operationNumbers.operativeExposureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        break;
                    case "Clinical Outcomes":
                        ClinicalOutcomesPage clinicalOutcomes = dashboard.getClinicalOutcomes();
                        Assert.False(clinicalOutcomes.CMARequirementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
                        Assert.False(clinicalOutcomes.phinLink.GetAttribute("value").Equals("PHINLink"));
                        break;
                    default:
                        Console.WriteLine("Invalid section");
                        break;

                }
            }

        }

        private (NewApplicationPage,DashboardPage) getNewApp()
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            NewApplicationPage newApp = dashboardPage.getNewApp();
            return (newApp, dashboardPage);
        }
    }
}
