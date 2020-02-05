using Helpers;
using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
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

        [Test]
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
        [Test]
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
        }

        [Test]
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
        [Test]
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
    }
}
