using Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using RCoS;
using System;
using System.Collections.Generic;
using System.Text;
using Helpers;
using System.Threading;
using System.Text.RegularExpressions;
using System.Linq;
using static Helpers.MailSubjectEnum;

namespace SignUpTests
{
    class SignUpTests
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
            SignUpPage sup = lp.GetSignUp();
            foreach (var e in sup.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test]
        public void TestRequired()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            SignUpPage sup = lp.GetSignUp();
            sup.registerBtn.Click();
            Assert.That(sup.emailRequiredMsg.Displayed && sup.passwordRequiredMsg.Displayed
                && sup.confirmPasswordRequiredMsg.Displayed && sup.tosRequiredMsg.Displayed);
        }

        [Test]
        public void TestInvalidEmail()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            SignUpPage sup = lp.GetSignUp();
            sup.DoRegister("InvalidEmail", "", "");
            Assert.That(sup.emailInvalidMsg.Displayed);
        }

        [Test]
        public void TestPassDontMatch()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            SignUpPage sup = lp.GetSignUp();
            sup.DoRegister(TestBase.username,"123","321");
            Assert.That(sup.passwordsDontMatchMsg.Displayed);
        }

        [Test]
        public void TestPasswordMinCharValidation()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            SignUpPage sup = lp.GetSignUp();
            sup.DoRegister(TestBase.username, "1!qQ", "1!qQ");
            Assert.That(sup.passwordMinLengthMsg.Displayed);
        }
        [Test]
        public void TestPasswordAlphanumericValidation()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            SignUpPage sup = lp.GetSignUp();
            sup.DoRegister(TestBase.username, "1234qQ", "1234qQ");
            Assert.That(sup.passwordAlphanumericMsg.Displayed);
        }
        [Test]
        public void TestPasswordLowercaseValidation()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            SignUpPage sup = lp.GetSignUp();
            sup.DoRegister(TestBase.username, "12!@QQ", "12!@QQ");
            Assert.That(sup.passwordLowercaseMsg.Displayed);
        }
        [Test]
        public void TestPasswordUpercaseValidation()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            SignUpPage sup = lp.GetSignUp();
            sup.DoRegister(TestBase.username, "12!@qq", "12!@qq");
            Assert.That(sup.passwordUppercaseMsg.Displayed);
        }
        [Test]
        public void TestPasswordNumberValidation()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            SignUpPage sup = lp.GetSignUp();
            sup.DoRegister(TestBase.username, "!@qqQQ", "!@qqQQ");
            Assert.That(sup.passwordNumberMsg.Displayed);
        }
        [Test]
        public void TestCancelBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin(); 
            SignUpPage sup = lp.GetSignUp();
            LoginPage lp2 = sup.ClickCancel();
            Assert.That(lp2.title.Displayed);
        }
        [Test]
        public void TestSuccessfulSignup()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            SignUpPage sup = lp.GetSignUp();
            sup.DoRegister(TestBase.username, TestBase.password, TestBase.password);
            Thread.Sleep(2000);
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.username, TestBase.password);
            string allEmails = mailRepository.GetUnreadMails(Subject.Register);
            var linkParser = new Regex(@"(?:https?://rcs-cosmetics-identity-dev.azurewebsites.net/Account)\S+\b");
            var link = linkParser.Matches(allEmails);
            TestBase.driver.Url = link.SingleOrDefault().ToString().Replace("&amp;","&");
            TyRegistrationPage trp = new TyRegistrationPage(TestBase.driver);
            Assert.That(trp.title.Displayed);
        }
    }
}
