using NUnit.Framework;
using RCoS;
using Pages;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;

namespace LoginTests
{
    [TestFixture]
    [Category("Login")]
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
            TestBase.TakeScreenShot();
            TestBase.driver.Quit();
        }

        [Test, Order(1)]
        public void TestPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            foreach (var e in loginPage.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test]
        public void TestWrongUsername()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            loginPage.DoLogin("a@a.com",TestBase.password);
            Assert.That(loginPage.userOrPassValidationMsg.Displayed);
        }
        [Test]
        public void TestWrongPassword()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            loginPage.DoLogin(TestBase.username,"123");
            Assert.That(loginPage.userOrPassValidationMsg.Displayed);
        }
        [Test]
        public void TestRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            loginPage.DoLogin("","");
            Assert.That(loginPage.emailRequiredMsg.Displayed && loginPage.passwordRequiredMsg.Displayed);
        }
        [Test]
        public void TestSuccessfulLogin()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username,TestBase.password);
            dashboardPage.openSideMenuIfClosed();
            Assert.That(dashboardPage.username.Text.Equals(TestBase.username));
        }
        [Test]
        public void TestSignUpBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            Assert.That(signUpPage.title.Displayed);
        }
        [Test]
        public void TestCancelBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            loginPage.ClickCancel();
            Assert.That(homePage.title.Displayed);
        }

    }
}
