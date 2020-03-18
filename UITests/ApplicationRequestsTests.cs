using Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static Helpers.MailSubjectEnum;

namespace ApplicationRequestsTests
{
    [TestFixture]
    [Category("ApplicationRequests")]
    class ApplicationRequestsTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[Applications]", TestBase.appUsername, "Status", 2);
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
            foreach (var e in applicationRequestsPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestApproveRequest()
        {

            string expectedText = "Congratulations";
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            applicationRequestsPage.filterInput.Clear();
            applicationRequestsPage.filterInput.SendKeys(TestBase.appUsername);
            string rowValue;
            rowValue = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.statusCell, TestBase.appUsername))).Text;
            Assert.That(rowValue.Equals("Submitted"));
            applicationRequestsPage.AcceptRequest(TestBase.appUsername);
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Filter']/ancestor::div[@class='mat-form-field-infix']//input")));
            applicationRequestsPage.filterInput.Clear();
            applicationRequestsPage.filterInput.SendKeys(TestBase.appUsername);
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(string.Format(applicationRequestsPage.emailCell, TestBase.appUsername))));
            rowValue = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.statusCell, TestBase.appUsername))).Text;
            Assert.That(rowValue.Equals("Approved"));
            //Assert.That(TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.editBtn, TestBase.appUsername))).GetAttribute("disabled").Equals("true"));
            string attribute = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.approveBtn, TestBase.appUsername))).GetAttribute("disabled");
            Assert.That(attribute.Equals("true"));
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.appUsername, TestBase.password);
            string allEmails = mailRepository.GetUnreadMails(Subject.ApplicationApproval).ToString();
            Assert.That(allEmails.Contains(expectedText));

        }
        

        [Test, Order(2)]
        public void TestAddColumn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            applicationRequestsPage.addColumnBtn.Click();
            Assert.That(applicationRequestsPage.careerGradeTableHeader.Displayed);
            applicationRequestsPage.addColumnBtn.Click();
            Assert.That(applicationRequestsPage.telephoneTableHeader.Displayed);
            applicationRequestsPage.addColumnBtn.Click();
            Assert.That(applicationRequestsPage.createdAtTableHeader.Displayed);
        }
        [Test, Order(2)]
        public void TestRemoveColumn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            applicationRequestsPage.addColumnBtn.Click();
            applicationRequestsPage.addColumnBtn.Click();
            applicationRequestsPage.addColumnBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(applicationRequestsPage.createdAtTableHeader));
            applicationRequestsPage.removeColumnBtn.Click();
            Assert.IsFalse(TestBase.ElementIsPresent(applicationRequestsPage.createdAtTableHeader));
            applicationRequestsPage.removeColumnBtn.Click();
            Assert.IsFalse(TestBase.ElementIsPresent(applicationRequestsPage.telephoneTableHeader));
            applicationRequestsPage.removeColumnBtn.Click();
            Assert.IsFalse(TestBase.ElementIsPresent(applicationRequestsPage.careerGradeTableHeader));
        }
        [Test, Order(2)]
        public void TestCancelAccept()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            applicationRequestsPage.filterInput.Clear();
            applicationRequestsPage.filterInput.SendKeys(TestBase.appUsername);
            IWebElement accept = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.approveBtn, TestBase.appUsername)));
            string status = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.statusCell, TestBase.appUsername))).Text;
            accept.Click();
            applicationRequestsPage.cancelBtn.Click();
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-application-requests//table")));
            applicationRequestsPage.filterInput.Clear();
            applicationRequestsPage.filterInput.SendKeys(TestBase.appUsername);
            accept = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.approveBtn, TestBase.appUsername)));
            Assert.That(accept.Enabled);
            Assert.That(!status.Equals("Approved"));
        }
        
        [Test, Order(1)]
        public void TestDataIsLoaded()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            applicationRequestsPage.filterInput.Clear();
            applicationRequestsPage.filterInput.SendKeys(TestBase.appUsername);
            string rowValue;
            rowValue = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.emailCell,TestBase.appUsername))).Text;
            Assert.That(rowValue.Equals(TestBase.applicantData[0]));
            rowValue = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.GMCNumberCell,TestBase.appUsername))).Text;
            Assert.That(rowValue.Equals(TestBase.applicantData[1]));
            rowValue = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.GMCSpecialtyCell,TestBase.appUsername))).Text;
            Assert.That(rowValue.Equals(TestBase.applicantData[2]));
            
        }
    }
}
