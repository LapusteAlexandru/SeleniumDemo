using Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System.Text.RegularExpressions;
using System.Threading;
using static Helpers.MailSubjectEnum;

namespace ForgotPasswordTests
{
    [TestFixture]
    [Category("ForgotPassword")]
    class ForgotPasswordTests
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
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            foreach (var e in forgotPasswordPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestRequiredMsg()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            forgotPasswordPage.submitBtn.Click();
            Assert.That(forgotPasswordPage.emailRequiredMsg.Displayed);
        }
        [Test]
        public void TestCancelBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            forgotPasswordPage.getLogin(); 
            Assert.That(loginPage.title.Displayed);
        }
        [Test]
        public void TestHomeBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            forgotPasswordPage.ClickHome(); 
            Assert.That(homePage.title.Displayed);
        }
        [Test]
        public void TestNonExistingUser()
        {
            var username = "testnonregistereduser@test.com";
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            forgotPasswordPage.SendResetEmail(username);
            TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='content']")));
            var message = forgotPasswordPage.userNotRegistered.Text;
            Assert.That(message.Contains(username));
        }

        [Test]

        public void TestPresenceOfResetPassLink()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            forgotPasswordPage.SendResetEmail(TestBase.username);
            Thread.Sleep(2000);
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.username, TestBase.password);
            string allEmails = mailRepository.GetUnreadMails(Subject.ForgotPassword);
            var linkParser = new Regex(@"(?:https?://rcs-cosmetics-identity-dev.azurewebsites.net/Account/ResetPassword)\S+\b");
            Assert.That(linkParser.Matches(allEmails).Count == 1);
        }
    }
}
