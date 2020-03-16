using Pages;
using NUnit.Framework;
using RCoS;
using Helpers;
using System.Threading;
using System.Text.RegularExpressions;
using System.Linq;
using static Helpers.MailSubjectEnum;

namespace SignUpTests
{
    [TestFixture]
    [Category("Signup")]
    class SignUpTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteUserData("[dbo].[Users]", TestBase.username);
            TestBase.deleteUserData("[dbo].[AspNetUsers]", TestBase.username);
        }
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
            SignUpPage signUpPage = loginPage.GetSignUp();
            foreach (var e in signUpPage.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test]
        public void TestRequired()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.registerBtn.Click();
            Assert.That(signUpPage.emailRequiredMsg.Displayed && signUpPage.passwordRequiredMsg.Displayed
                && signUpPage.confirmPasswordRequiredMsg.Displayed && signUpPage.tosRequiredMsg.Displayed);
        }

        [Test]
        public void TestInvalidEmail()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister("InvalidEmail", "", "");
            Assert.That(signUpPage.emailInvalidMsg.Displayed);
        }

        [Test]
        public void TestPassDontMatch()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username,"123","321");
            Assert.That(signUpPage.passwordsDontMatchMsg.Displayed);
        }

        [Test]
        public void TestPasswordMinCharValidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username, "1!qQ", "1!qQ");
            Assert.That(signUpPage.passwordMinLengthMsg.Displayed);
        }
        [Test]
        public void TestPasswordAlphanumericValidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username, "1234qQ", "1234qQ");
            Assert.That(signUpPage.passwordAlphanumericMsg.Displayed);
        }
        [Test]
        public void TestPasswordLowercaseValidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username, "12!@QQ", "12!@QQ");
            Assert.That(signUpPage.passwordLowercaseMsg.Displayed);
        }
        [Test]
        public void TestPasswordUpercaseValidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username, "12!@qq", "12!@qq");
            Assert.That(signUpPage.passwordUppercaseMsg.Displayed);
        }
        [Test]
        public void TestPasswordNumberValidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username, "!@qqQQ", "!@qqQQ");
            Assert.That(signUpPage.passwordNumberMsg.Displayed);
        }
        [Test]
        public void TestCancelBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin(); 
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.ClickCancel();
            Assert.That(loginPage.title.Displayed);
        }
        [Test]
        public void TestSuccessfulSignup()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            SignUpTyPage signupTyPage =  signUpPage.DoRegister(TestBase.username, TestBase.password, TestBase.password);
            Assert.That(signupTyPage.tyMessage.Displayed);
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
