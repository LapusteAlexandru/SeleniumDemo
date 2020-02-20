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
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            ResetPasswordPage resetPasswordPage = forgotPasswordPage.GetResetPassword(TestBase.username);
            foreach (var e in resetPasswordPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            ResetPasswordPage resetPasswordPage = forgotPasswordPage.GetResetPassword(TestBase.username);
            resetPasswordPage.resetBtn.Click();
            Thread.Sleep(500);
            Assert.That(resetPasswordPage.passwordRequiredMsg.Displayed && resetPasswordPage.confirmPasswordRequiredMsg.Displayed);
        }
        [Test]
        public void TestPasswordMatch()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            ResetPasswordPage resetPasswordPage = forgotPasswordPage.GetResetPassword(TestBase.username);
            resetPasswordPage.DoReset(TestBase.password, TestBase.password + "1");
            Thread.Sleep(300);
            Assert.That(resetPasswordPage.passwordsDontMatchMsg.Displayed);
        }
        [Test]
        public void TestResetWithOldPass()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            ResetPasswordPage resetPasswordPage = forgotPasswordPage.GetResetPassword(TestBase.username);
            resetPasswordPage.DoReset(TestBase.password, TestBase.password);
            Assert.That(resetPasswordPage.oldPasswordValidationMsg.Displayed);
        }
        [Test]
        public void TestCancelBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            ResetPasswordPage resetPasswordPage = forgotPasswordPage.GetResetPassword(TestBase.username);
            resetPasswordPage.ClickCancel();
            Assert.That(homePage.title.Displayed);
        }
        [Test]
        public void TestResetPassword()
        {
            passwordReset = true;
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            ResetPasswordPage resetPasswordPage = forgotPasswordPage.GetResetPassword(TestBase.username);
            resetPasswordPage.DoReset(TestBase.password+'4', TestBase.password+'4');
            TyResetPasswordPage tresetPasswordPage = new TyResetPasswordPage(TestBase.driver);
            foreach(var e in tresetPasswordPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestAlreadyHaveAccountBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            ForgotPasswordPage forgotPasswordPage = loginPage.GetForgotPassword();
            ResetPasswordPage resetPasswordPage = forgotPasswordPage.GetResetPassword(TestBase.username);
            resetPasswordPage.ClickAlreadyHaveAccount();
            Assert.That(loginPage.title.Displayed);
        }
    }
}
