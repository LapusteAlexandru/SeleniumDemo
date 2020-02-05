﻿using NUnit.Framework;
using RCoS;
using Pages;

namespace LoginTests
{
    class LoginTests
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
            foreach (var e in lp.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test]
        public void TestWrongUsername()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            lp.DoLogin("a@a.com",TestBase.password);
            Assert.That(lp.userOrPassValidationMsg.Displayed);
        }
        [Test]
        public void TestWrongPassword()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            lp.DoLogin(TestBase.username,"123");
            Assert.That(lp.userOrPassValidationMsg.Displayed);
        }
        [Test]
        public void TestRequiredMsgs()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            lp.DoLogin("","");
            Assert.That(lp.emailRequiredMsg.Displayed && lp.passwordRequiredMsg.Displayed);
        }
        [Test]
        public void TestSuccessfulLogin()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username,TestBase.password);
            Assert.That(dp.username.Text.Equals(TestBase.username));
        }
        [Test]
        public void TestSignUpBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            SignUpPage sup = lp.GetSignUp();
            Assert.That(sup.title.Displayed);
        }
        [Test]
        public void TestCancelBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            HomePage hp2 = lp.ClickCancel();
            Assert.That(hp2.title.Displayed);
        }

    }
}