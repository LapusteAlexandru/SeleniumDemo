using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static Helpers.RadioButtonEnum;

namespace EditApplicationTests
{
    [TestFixture]
    [Category("EditApplication")]
    class EditApplicationTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[Applications]", TestBase.uiUsername, "Status", 1);
        }
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

        [Test, Order(1)]
        public void TestPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.uiUsername);
            foreach (var e in editPage.GetMainElements())
            {
                bool display = e.Displayed;
                if (display == false)
                {
                    editPage.navigateRightBtn.Click();
                    Thread.Sleep(300);
                    Assert.That(e.Displayed);
                }
                else
                    Assert.That(e.Displayed);

            }
        }
        [Test, Order(1)]
        public void TestAccountDetailsPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.uiUsername);
            AccountDetailsPage accountDetailsPage = editPage.getAccountDetails();
            foreach (var e in accountDetailsPage.GetMainElements())
            {
                    Assert.That(e.Displayed);

            }
        }
        [Test, Order(1)]
        public void TestProbityStatementsPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.uiUsername);
            ProbityStatementsPage probityStatementsPage = editPage.getProbityStatements();
            foreach (var e in probityStatementsPage.GetMainElements())
            {
                    Assert.That(e.Displayed);

            }
        }
        [Test, Order(1)]
        public void TestInsurancePageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.uiUsername);
            ProfessionalInsurancePage professionalInsurancePage = editPage.getProfessionalInsurance();
            foreach (var e in professionalInsurancePage.GetMainElements())
            {
                    Assert.That(e.Displayed);

            }
        }
        [Test, Order(1)]
        public void TestProfessionalBehavioursPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.uiUsername);
            ProfessionalBehavioursPage professionalBehavioursPage = editPage.getProfessionalBehaviours();
            foreach (var e in professionalBehavioursPage.GetMainElements())
            {
                    Assert.That(e.Displayed);

            }
        }
        [Test, Order(1)]
        public void TestRevalidationPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.uiUsername);
            RevalidationPage revalidationPage = editPage.getRevalidation();
            foreach (var e in revalidationPage.GetMainElements())
            {
                    Assert.That(e.Displayed);

            }
        }
        [Test, Order(1)]
        public void TestOperationNumbersPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.uiUsername);
            OperationNumbersPage OperationNumbersPage = editPage.getOperativeExposure();
            foreach (var e in OperationNumbersPage.GetMainElements())
            {
                    Assert.That(e.Displayed);

            }
        }
        [Test, Order(1)]
        public void TestClinicalOutcomesPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.uiUsername);
            ClinicalOutcomesPage clinicalOutcomesPage = editPage.getClinicalOutcomes();
            foreach (var e in clinicalOutcomesPage.GetMainElements())
            {
                    Assert.That(e.Displayed);

            }
        }
        [Test, Order(1)]
        public void TestProfessionalDevelopmentPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.uiUsername);
            ContinuingDevelopmentPage continuingDevelopmentPage = editPage.getProfessionalDevelopment();
            foreach (var e in continuingDevelopmentPage.GetMainElements())
            {
                    Assert.That(e.Displayed);

            }
        }
        [Test, Order(1)]
        public void TestPracticePageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.uiUsername);
            ReflectionOnPracticePage reflectionOnPracticePage = editPage.getReflectionOnPractice();
            foreach (var e in reflectionOnPracticePage.GetMainElements())
            {
                    Assert.That(e.Displayed);

            }
        }
        [Test, Order(1)]
        public void TestReferencePageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.uiUsername);
            ReferencesPage referencesPage = editPage.getReference();
            foreach (var e in referencesPage.GetMainElements())
            {
                    Assert.That(e.Displayed);

            }
        }
        [Test, Order(2)]
        public void TestProbityStatementsRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests(); 
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.username);
            ProbityStatementsPage probityStatementsPage = editPage.getProbityStatements();
            probityStatementsPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in probityStatementsPage.requiredMsgs)
                Assert.That(e.Displayed);

            Assert.That(probityStatementsPage.requiredMsgs.Count.Equals(3));
        }
        [Test, Order(2)]
        public void TestProfessionalInsuranceRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests(); 
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.username);
            ProfessionalInsurancePage professionalInsurancePage = editPage.getProfessionalInsurance();
            professionalInsurancePage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in professionalInsurancePage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(professionalInsurancePage.requiredMsgs.Count.Equals(4));
        }
        [Test, Order(2)]
        public void TestProfessionalBehaviourRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests(); 
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.username);
            ProfessionalBehavioursPage professionalBehavioursPage = editPage.getProfessionalBehaviours();
            professionalBehavioursPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in professionalBehavioursPage.requiredMsgs)
                Assert.That(e.Displayed);

            Assert.That(professionalBehavioursPage.requiredMsgs.Count.Equals(2));
        }
        [Test, Order(2)]
        public void TestRevalidationRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests(); 
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.username);
            RevalidationPage revalidationPage = editPage.getRevalidation();
            revalidationPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in revalidationPage.requiredMsgs)
                Assert.That(e.Displayed);

            Assert.That(revalidationPage.requiredMsgs.Count.Equals(5));
        }
        [Test, Order(2)]
        public void TestOperativeExposureRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests(); 
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.username);
            OperationNumbersPage OperationNumbersPage = editPage.getOperativeExposure();
            OperationNumbersPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in OperationNumbersPage.requiredMsgs)
                Assert.That(e.Displayed);

            Assert.That(OperationNumbersPage.requiredMsgs.Count.Equals(3));
        }
        [Test, Order(2)]
        public void TestClinicalOutcomesRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests(); 
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.username);
            ClinicalOutcomesPage clinicalOutcomesPage = editPage.getClinicalOutcomes();
            clinicalOutcomesPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in clinicalOutcomesPage.requiredMsgs)
                Assert.That(e.Displayed);

            Assert.That(clinicalOutcomesPage.requiredMsgs.Count.Equals(2));
        }
        [Test, Order(2)]
        public void TestReflectionOnPracticeRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests(); 
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.username);
            ReflectionOnPracticePage reflectionOnPracticePage = editPage.getReflectionOnPractice();
            foreach(var e in reflectionOnPracticePage.GetFormTabs())
            {
                e.Click();
                TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(reflectionOnPracticePage.hospitalSiteInput));
                reflectionOnPracticePage.submitBtn.Click();
                e.Click();
                var selected = e.GetAttribute("aria-selected");
                TestBase.wait.Equals(selected.Equals(true));
                reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
                Thread.Sleep(500);
                foreach (var f in reflectionOnPracticePage.requiredMsgs)
                {
                    Assert.That(f.Displayed);
                }
                Assert.That(reflectionOnPracticePage.requiredMsgs.Count.Equals(12));
            }
            
        }
        
        [Test]
        public void TestEditProbityStatements()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword); 
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.appUsername);
            ProbityStatementsPage probityStatementsPage = editPage.getProbityStatements();
            ProbityRadio radioOption = ProbityRadio.Nothing;
            if (probityStatementsPage.nothingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"))
                radioOption = ProbityRadio.Something;
            probityStatementsPage.CompleteForm(radioOption);
            TestBase.driver.Navigate().Refresh();
            dashboardPage.openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(dashboardPage.logoutBtn));
            dashboardPage.logout();
            homePage.GetLogin(); 
            loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            dashboardPage.getProbityStatements();
            Assert.That(probityStatementsPage.professionalObligationsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(probityStatementsPage.suspensionCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            if (radioOption.Equals(ProbityRadio.Nothing))
                Assert.That(probityStatementsPage.nothingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"));
            else
                Assert.That(probityStatementsPage.somethingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"));
        }
        
        [Test]
        public void TestEditProfessionalInsurance()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword); 
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.appUsername);
            ProfessionalInsurancePage professionalInsurancePage = editPage.getProfessionalInsurance();
            YesOrNoRadio radioOption = YesOrNoRadio.No; 
            if (professionalInsurancePage.noPracticeRadio.GetAttribute("class").Contains("mat-radio-checked"))
                radioOption = YesOrNoRadio.Yes;
            professionalInsurancePage.CompleteForm(radioOption);
            TestBase.driver.Navigate().Refresh();
            dashboardPage.openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(dashboardPage.logoutBtn));
            dashboardPage.logout();
            homePage.GetLogin(); 
            loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            dashboardPage.getProfessionalInsurance();
            
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalInsurancePage.title));
            dashboardPage.openSideMenuIfClosed();
            Assert.That(professionalInsurancePage.indemnityArrangementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(professionalInsurancePage.disclosedNatureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            if (radioOption.Equals(YesOrNoRadio.No))
                Assert.That(professionalInsurancePage.noPracticeRadio.GetAttribute("class").Contains("mat-radio-checked"));
            else
                Assert.That(professionalInsurancePage.yesPracticeRadio.GetAttribute("class").Contains("mat-radio-checked"));
            Assert.That(professionalInsurancePage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }
        [Test]
        public void TestEditRevalidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword); 
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            EditApplicationPage editPage = applicationRequestsPage.EditUser(TestBase.appUsername);
            RevalidationPage revalidationPage = editPage.getRevalidation();
            YesOrNoRadio radioOption = YesOrNoRadio.No; 
            if (revalidationPage.declareAppraisalNo.GetAttribute("class").Contains("mat-radio-checked"))
                radioOption = YesOrNoRadio.Yes;
            revalidationPage.CompleteForm(radioOption);
            TestBase.driver.Navigate().Refresh();
            dashboardPage.openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(dashboardPage.logoutBtn));
            dashboardPage.logout();
            homePage.GetLogin(); 
            loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            dashboardPage.getRevalidation();
            
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(revalidationPage.title));
            dashboardPage.openSideMenuIfClosed(); 
            if (radioOption.Equals(YesOrNoRadio.No))
                Assert.That(revalidationPage.declareAppraisalNo.GetAttribute("class").Contains("mat-radio-checked"));
            else
                Assert.That(revalidationPage.declareAppraisalYes.GetAttribute("class").Contains("mat-radio-checked"));

            Assert.That(revalidationPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }
    }
}
