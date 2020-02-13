using NUnit.Framework;
using Pages;
using RCoS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ResetPasswordTests
{
    [TestFixture]
    [Category("ResetPassword")]
    class ResetPasswordTests
    {
        private static bool passwordReset = false;
        [SetUp]
        public void Setup()
        {
            TestBase.RootInit();
        }

        [TearDown]
        public void Teardown()
        {
            if(passwordReset)
            {
                TestBase.driver.Url = "https://rcs-cosmetics-client-dev.azurewebsites.net/"; 
                HomePage hp = new HomePage(TestBase.driver);
                LoginPage lp = hp.GetLogin();
                ForgotPasswordPage fpp = lp.GetForgotPassword();
                ResetPasswordPage rpp = fpp.GetResetPassword(TestBase.username);
                rpp.DoReset(TestBase.password , TestBase.password );
            }
            TestBase.TakeScreenShot();
            TestBase.driver.Quit();
        }

        [Test, Order(1)]
        public void TestPageLoads()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            ResetPasswordPage rpp = fpp.GetResetPassword(TestBase.username);
            foreach (var e in rpp.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestRequiredMsgs()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            ResetPasswordPage rpp = fpp.GetResetPassword(TestBase.username);
            rpp.resetBtn.Click();
            Thread.Sleep(500);
            Assert.That(rpp.passwordRequiredMsg.Displayed && rpp.confirmPasswordRequiredMsg.Displayed);
        }
        [Test]
        public void TestPasswordMatch()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            ResetPasswordPage rpp = fpp.GetResetPassword(TestBase.username);
            rpp.DoReset(TestBase.password, TestBase.password + "1");
            Thread.Sleep(300);
            Assert.That(rpp.passwordsDontMatchMsg.Displayed);
        }
        [Test]
        public void TestResetWithOldPass()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            ResetPasswordPage rpp = fpp.GetResetPassword(TestBase.username);
            rpp.DoReset(TestBase.password, TestBase.password);
            Assert.That(rpp.oldPasswordValidationMsg.Displayed);
        }
        [Test]
        public void TestCancelBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            ResetPasswordPage rpp = fpp.GetResetPassword(TestBase.username);
            HomePage hp2 = rpp.ClickCancel();
            Assert.That(hp2.title.Displayed);
        }
        [Test]
        public void TestResetPassword()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            ResetPasswordPage rpp = fpp.GetResetPassword(TestBase.username);
            rpp.DoReset(TestBase.password+'4', TestBase.password+'4');
            TyResetPasswordPage trpp = new TyResetPasswordPage(TestBase.driver);
            foreach(var e in trpp.GetMainElements())
                Assert.That(e.Displayed);
            passwordReset = true;
        }
        [Test]
        public void TestAlreadyHaveAccountBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            ForgotPasswordPage fpp = lp.GetForgotPassword();
            ResetPasswordPage rpp = fpp.GetResetPassword(TestBase.username);
            LoginPage lp2 = rpp.ClickAlreadyHaveAccount();
            Assert.That(lp2.title.Displayed);
        }
    }
}
