using Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ForgotPasswordTests
{
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
            TestBase.driver.Quit();
        }

        [Test]
        public void TestPageLoads()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            foreach (var e in fpp.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestRequiredMsg()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            fpp.submitBtn.Click();
            Assert.That(fpp.emailRequiredMsg.Displayed);
        }
        [Test]
        public void TestCancelBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            fpp.getLogin(); 
            Assert.That(lp.title.Displayed);
        }
        [Test]
        public void TestHomeBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            fpp.ClickHome(); 
            Assert.That(hp.title.Displayed);
        }
        [Test]
        public void TestNonExistingUser()
        {
            var username = "testnonregistereduser@test.com";
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            fpp.SendResetEmail(username);
            TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@class='content']")));
            var message = fpp.userNotRegistered.Text;
            Assert.That(message.Contains(username));
        }

        [Test]

        public void TestPresenceOfResetPassLink()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            fpp.SendResetEmail(TestBase.username);
            Thread.Sleep(2000);
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.username, TestBase.password);
            string allEmails = mailRepository.GetUnreadMails("account password reset");
            var linkParser = new Regex(@"(?:https?://rcs-cosmetics-identity-dev.azurewebsites.net/Account/ResetPassword)\S+\b");
            Assert.That(linkParser.Matches(allEmails).Count == 1);
        }
    }
}
