using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluatorRequestsTests
{
    [TestFixture]
    [Category("EvaluatorRequests")]
    class EvaluatorRequestsTests
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
        public void TestApproveRequest()
        {
            string rowValue;
            int day = TestBase.currentDay;
            string expectedText = "Test accept " + day;
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.evaluatorUsername, TestBase.password);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            applicationRequestsPage.AcceptRequest(TestBase.appUsername);
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Filter']/ancestor::div[@class='mat-form-field-infix']//input")));
            applicationRequestsPage.filterInput.Clear();
            applicationRequestsPage.filterInput.SendKeys(TestBase.appUsername);
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(string.Format(applicationRequestsPage.emailCell, TestBase.appUsername))));
            rowValue = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.statusCell, TestBase.appUsername))).Text;
            Assert.That(rowValue.Equals("Reviewed by Evaluator"));
            
            Assert.That(applicationRequestsPage.ReadComment(TestBase.appUsername, TestBase.userLastName).Equals(expectedText));
            string status = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.feedbackStatus, TestBase.userLastName))).Text;
            Assert.That(status.Contains("Approved"));
//            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.appUsername, TestBase.password);
 //           string allEmails = mailRepository.GetUnreadMails(Subject.ApplicationApproval).ToString();
//            Assert.That(allEmails.Contains(expectedText));

        }
        
        [Test]
        public void TestRejectRequest()
        {
            string rowValue;
            int day = TestBase.currentDay;
            string expectedText = "Test reject " + day;
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.evaluatorUsername, TestBase.password);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            applicationRequestsPage.RejectRequest(TestBase.appUsername);
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Filter']/ancestor::div[@class='mat-form-field-infix']//input")));
            applicationRequestsPage.filterInput.Clear();
            applicationRequestsPage.filterInput.SendKeys(TestBase.appUsername);
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(string.Format(applicationRequestsPage.emailCell, TestBase.appUsername))));
            rowValue = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.statusCell, TestBase.appUsername))).Text;
            Assert.That(rowValue.Equals("Reviewed by Evaluator"));
            
            Assert.That(applicationRequestsPage.ReadComment(TestBase.appUsername, TestBase.userLastName).Equals(expectedText));
            string status = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.feedbackStatus, TestBase.userLastName))).Text;
            Assert.That(status.Contains("Rejected"));
            //            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.appUsername, TestBase.password);
            //           string allEmails = mailRepository.GetUnreadMails(Subject.ApplicationApproval).ToString();
            //            Assert.That(allEmails.Contains(expectedText));

        }
    }
}
