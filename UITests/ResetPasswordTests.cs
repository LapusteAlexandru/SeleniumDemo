using NUnit.Framework;
using Pages;
using RCoS;
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
                HomePage homePage = new HomePage(TestBase.driver);
                LoginPage loginPage = homePage.GetLogin();
                ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
                ResetPasswordPage resetPasswordPage = forgotPasswordPage.GetResetPassword(TestBase.username);
                resetPasswordPage.DoReset(TestBase.password , TestBase.password );
                passwordReset = false;
            }
            TestBase.TakeScreenShot();
            TestBase.driver.Quit();
        }

        [Test, Order(1)]
        public void TestPageLoads()
        {
            ResetPasswordPage resetPasswordPage = getResetPassword();
            foreach (var e in resetPasswordPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestRequiredMsgs()
        {

            ResetPasswordPage resetPasswordPage = getResetPassword();
            resetPasswordPage.resetBtn.Click();
            Thread.Sleep(500);
            Assert.That(resetPasswordPage.passwordRequiredMsg.Displayed && resetPasswordPage.confirmPasswordRequiredMsg.Displayed);
        }
        [Test]
        public void TestPasswordMatch()
        {

            ResetPasswordPage resetPasswordPage = getResetPassword();
            resetPasswordPage.DoReset(TestBase.password, TestBase.password + "1");
            Thread.Sleep(300);
            Assert.That(resetPasswordPage.passwordsDontMatchMsg.Displayed);
        }
        [Test]
        public void TestResetWithOldPass()
        {

            ResetPasswordPage resetPasswordPage = getResetPassword();
            resetPasswordPage.DoReset(TestBase.password, TestBase.password);
            Assert.That(resetPasswordPage.oldPasswordValidationMsg.Displayed);
        }
        [Test]
        public void TestCancelBtn()
        {

            HomePage home = new HomePage(TestBase.driver);
            ResetPasswordPage resetPasswordPage = getResetPassword();
            resetPasswordPage.ClickCancel();
            Assert.That(home.title.Displayed);
        }
        [Test]
        public void TestResetPassword()
        {
            passwordReset = true;

            ResetPasswordPage resetPasswordPage = getResetPassword();
            resetPasswordPage.DoReset(TestBase.password+'4', TestBase.password+'4');
            TyResetPasswordPage tresetPasswordPage = new TyResetPasswordPage(TestBase.driver);
            foreach(var e in tresetPasswordPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestAlreadyHaveAccountBtn()
        {
            LoginPage login = new LoginPage(TestBase.driver);
            ResetPasswordPage resetPasswordPage = getResetPassword();
            resetPasswordPage.ClickAlreadyHaveAccount();
            Assert.That(login.title.Displayed);
        }

        private ResetPasswordPage getResetPassword()
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            ResetPasswordPage resetPasswordPage = forgotPasswordPage.GetResetPassword(TestBase.username);
            return resetPasswordPage;
        }
    }
}
