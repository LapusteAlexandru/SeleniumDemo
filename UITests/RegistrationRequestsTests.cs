using Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using static Helpers.MailSubjectEnum;

namespace RegistrationRequestsTests
{
    [TestFixture]
    [Category("RegistrationRequests")]
    class RegistrationRequestsTests
    {
        private static bool resubmit = false;
        [SetUp]
        public void Setup()
        {
            TestBase.RootInit();
        }

        [TearDown]
        public void Teardown()
        {
            if (resubmit)
            {
                DashboardPage dashboardPage = new DashboardPage(TestBase.driver);
                HomePage homePage = dashboardPage.logout();
                LoginPage loginPage = homePage.GetLogin();
                loginPage.DoLogin(TestBase.username, TestBase.password);
                AccountDetailsPage adashboardPage = dashboardPage.getAccountDetails();
                adashboardPage.submitBtn.Click();
                TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(adashboardPage.accountSubmittedMsg));
                resubmit = false;
            }
            TestBase.TakeScreenShot();
            TestBase.driver.Quit();
        }

        [Test, Order(1)]
        public void TestPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage registrationRequestsPage = dashboardPage.getRegistrationRequests();
            foreach (var e in registrationRequestsPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestAcceptRequest()
        {

            string expectedText = "Your registration request has been accepted";
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage registrationRequestsPage = dashboardPage.getRegistrationRequests();
            registrationRequestsPage.AcceptRequest(TestBase.username);
            dashboardPage.logout();
            homePage.GetLogin();
            loginPage.DoLogin(TestBase.username, TestBase.password);
            dashboardPage.openSideMenuIfClosed();
            foreach (var e in dashboardPage.GetAllElements())
                Assert.That(e.Displayed); 
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.username, TestBase.password);
            string allEmails = mailRepository.GetUnreadMails(Subject.RegistrationAccepted);
            Assert.That(allEmails.Contains(expectedText));
            
        }
        [Test, Order(3)]
        public void TestRejectRequest()
        {
            resubmit = true;
            string expectedText = "Reason: Testing";
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage registrationRequestsPage = dashboardPage.getRegistrationRequests();
            registrationRequestsPage.RejectRequest(TestBase.username, "Testing");
            dashboardPage.logout();
            homePage.GetLogin();
            loginPage.DoLogin(TestBase.username, TestBase.password);
            dashboardPage.openSideMenuIfClosed();
            var e = dashboardPage.GetAllElements();
            for (int i = 5; i < e.Count; i++)
            {
                Assert.IsFalse(TestBase.ElementIsPresent(e[i]));
            }
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.username, TestBase.password);
            string allEmails = mailRepository.GetUnreadMails(Subject.RegistrationRejected);
            Assert.That(allEmails.Contains(expectedText));
            
        }

        [Test, Order(2)]
        public void TestAddColumn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage registrationRequestsPage = dashboardPage.getRegistrationRequests();
            registrationRequestsPage.addColumnBtn.Click();
            Assert.That(registrationRequestsPage.careerGradeTableHeader.Displayed);
            registrationRequestsPage.addColumnBtn.Click();
            Assert.That(registrationRequestsPage.telephoneTableHeader.Displayed);
            registrationRequestsPage.addColumnBtn.Click();
            Assert.That(registrationRequestsPage.createdAtTableHeader.Displayed);
        }
        [Test, Order(2)]
        public void TestRemoveColumn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage registrationRequestsPage = dashboardPage.getRegistrationRequests();
            registrationRequestsPage.addColumnBtn.Click();
            registrationRequestsPage.addColumnBtn.Click();
            registrationRequestsPage.addColumnBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(registrationRequestsPage.createdAtTableHeader));
            registrationRequestsPage.removeColumnBtn.Click();
            Assert.IsFalse(TestBase.ElementIsPresent(registrationRequestsPage.createdAtTableHeader));
            registrationRequestsPage.removeColumnBtn.Click();
            Assert.IsFalse(TestBase.ElementIsPresent(registrationRequestsPage.telephoneTableHeader));
            registrationRequestsPage.removeColumnBtn.Click();
            Assert.IsFalse(TestBase.ElementIsPresent(registrationRequestsPage.careerGradeTableHeader));
        }
        [Test, Order(2)]
        public void TestCancelAccept()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage registrationRequestsPage = dashboardPage.getRegistrationRequests();
            registrationRequestsPage.openRequestData(TestBase.username);
            Thread.Sleep(300);
            IWebElement accept = TestBase.driver.FindElement(By.XPath(string.Format(registrationRequestsPage.acceptBtn, TestBase.username)));
            accept.Click();
            registrationRequestsPage.cancelBtn.Click();
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(registrationRequestsPage.filterInput));
            IWebElement tableRow = TestBase.driver.FindElement(By.XPath(string.Format(registrationRequestsPage.tableEmailCell, TestBase.username)));
            Assert.That(tableRow.Displayed);
        }
        [Test, Order(2)]
        public void TestCancelReject()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage registrationRequestsPage = dashboardPage.getRegistrationRequests();
            registrationRequestsPage.openRequestData(TestBase.username);
            Thread.Sleep(300);
            IWebElement reject = TestBase.driver.FindElement(By.XPath(string.Format(registrationRequestsPage.rejectBtn, TestBase.username)));
            reject.Click();
            registrationRequestsPage.cancelBtn.Click();
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(registrationRequestsPage.filterInput));
            IWebElement tableRow = TestBase.driver.FindElement(By.XPath(string.Format(registrationRequestsPage.tableEmailCell, TestBase.username)));
            Assert.That(tableRow.Displayed);
        }
        [Test, Order(1)]
        public void TestDataIsLoaded()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage registrationRequestsPage = dashboardPage.getRegistrationRequests();
            registrationRequestsPage.openRequestData(TestBase.username);
            Thread.Sleep(300);
            string rowValue = "";
            for (int i = 0; i < registrationRequestsPage.dataRows.Count; i++)
            {
                rowValue = TestBase.driver.FindElement(By.XPath(string.Format(registrationRequestsPage.registrationTD, TestBase.username, registrationRequestsPage.dataRows[i]))).Text;
                Assert.That(rowValue.Equals(TestBase.userData[i]));
            }
            for (int i = 0; i < registrationRequestsPage.expandableDataRows.Count; i++)
            {
                rowValue = TestBase.driver.FindElement(By.XPath(string.Format(registrationRequestsPage.expandableRegistrationTD, TestBase.username, registrationRequestsPage.expandableDataRows[i]))).Text;
                Assert.That(rowValue.Equals(TestBase.expandableUserData[i]));
            }
        }
    }
}
