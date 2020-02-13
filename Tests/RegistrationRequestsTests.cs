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
    class RegistrationRequestsTests
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

        [Test, Order(1)]
        public void TestPageLoads()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage rrp = dp.getRegistrationRequests();
            foreach (var e in rrp.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestAcceptRequest()
        {
            string expectedText = "Your registration request has been accepted";
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage rrp = dp.getRegistrationRequests();
            rrp.AcceptRequest(TestBase.username);
            HomePage hp2 = dp.logout();
            LoginPage lp2 = hp2.GetLogin();
            DashboardPage dp2 = lp2.DoLogin(TestBase.username, TestBase.password);
            foreach (var e in dp2.GetAllElements())
                Assert.That(e.Displayed); 
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.username, TestBase.password);
            string allEmails = mailRepository.GetUnreadMails(Subject.RegistrationAccepted);
            Assert.That(allEmails.Contains(expectedText));

        }
        [Test, Order(3)]
        public void TestRejectRequest()
        {
            string expectedText = "Reason: Testing";
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage rrp = dp.getRegistrationRequests();
            rrp.RejectRequest(TestBase.username, "Testing");
            HomePage hp2 = dp.logout();
            LoginPage lp2 = hp2.GetLogin();
            DashboardPage dp2 = lp2.DoLogin(TestBase.username, TestBase.password);
            var e = dp2.GetAllElements();
            for (int i = 5; i < e.Count; i++)
            {
                Assert.IsFalse(TestBase.ElementIsPresent(e[i]));
            }
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.username, TestBase.password);
            string allEmails = mailRepository.GetUnreadMails(Subject.RegistrationRejected);
            Assert.That(allEmails.Contains(expectedText));
            AccountDetailsPage adp = dp2.getAccountDetails();
            adp.submitBtn.Click();
        }

        [Test, Order(2)]
        public void TestAddColumn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage rrp = dp.getRegistrationRequests();
            rrp.addColumnBtn.Click();
            Assert.That(rrp.careerGradeTableHeader.Displayed);
            rrp.addColumnBtn.Click();
            Assert.That(rrp.telephoneTableHeader.Displayed);
            rrp.addColumnBtn.Click();
            Assert.That(rrp.createdAtTableHeader.Displayed);
        }
        [Test, Order(2)]
        public void TestRemoveColumn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage rrp = dp.getRegistrationRequests();
            rrp.addColumnBtn.Click();
            rrp.addColumnBtn.Click();
            rrp.addColumnBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(rrp.createdAtTableHeader));
            rrp.removeColumnBtn.Click();
            Assert.IsFalse(TestBase.ElementIsPresent(rrp.createdAtTableHeader));
            rrp.removeColumnBtn.Click();
            Assert.IsFalse(TestBase.ElementIsPresent(rrp.telephoneTableHeader));
            rrp.removeColumnBtn.Click();
            Assert.IsFalse(TestBase.ElementIsPresent(rrp.careerGradeTableHeader));
        }
        [Test, Order(2)]
        public void TestCancelAccept()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage rrp = dp.getRegistrationRequests();
            rrp.openRequestData(TestBase.username);
            Thread.Sleep(300);
            IWebElement accept = TestBase.driver.FindElement(By.XPath(string.Format(rrp.acceptBtn, TestBase.username)));
            accept.Click();
            rrp.cancelBtn.Click();
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(rrp.filterInput));
            IWebElement tableRow = TestBase.driver.FindElement(By.XPath(string.Format(rrp.tableEmailCell, TestBase.username)));
            Assert.That(tableRow.Displayed);
        }
        [Test, Order(2)]
        public void TestCancelReject()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage rrp = dp.getRegistrationRequests();
            rrp.openRequestData(TestBase.username);
            Thread.Sleep(300);
            IWebElement reject = TestBase.driver.FindElement(By.XPath(string.Format(rrp.rejectBtn, TestBase.username)));
            reject.Click();
            rrp.cancelBtn.Click();
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(rrp.filterInput));
            IWebElement tableRow = TestBase.driver.FindElement(By.XPath(string.Format(rrp.tableEmailCell, TestBase.username)));
            Assert.That(tableRow.Displayed);
        }
        [Test, Order(1)]
        public void TestDataIsLoaded()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage rrp = dp.getRegistrationRequests();
            rrp.openRequestData(TestBase.username);
            Thread.Sleep(300);
            for (int i = 0; i < rrp.dataRows.Count; i++)
            {
                string rowValue = TestBase.driver.FindElement(By.XPath(string.Format(rrp.registrationTD, TestBase.username, rrp.dataRows[i]))).Text;
                Assert.That(rowValue.Equals(TestBase.userData[i]));
            }
        }
    }
}
