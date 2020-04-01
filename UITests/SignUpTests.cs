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
            TestBase.deleteUserData("[dbo].[AssignedApplications]", TestBase.evaluatorUsername);
            TestBase.deleteUserData("[dbo].[Users]", TestBase.evaluatorUsername);
            TestBase.deleteUserData("[dbo].[AspNetUsers]", TestBase.evaluatorUsername);
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
        
        [Test, Order(1)]
        public void TestEvaluatorPageLoads()
        {
            TestBase.driver.Url = "https://rcs-cosmetics-identity-dev.azurewebsites.net/Account/RegisterEvaluator";
            SignUpPage signupPage = new SignUpPage(TestBase.driver);
            DashboardPage dashboardPage = new DashboardPage(TestBase.driver);
            foreach (var e in signupPage.GetEvaluatorMainElements())
                Assert.That(e.Displayed);
            Assert.False(TestBase.ElementIsPresent(signupPage.cancelBtn));
            Assert.False(TestBase.ElementIsPresent(signupPage.loginBtn));
            Assert.False(TestBase.ElementIsPresent(dashboardPage.homeBtn));
        }

        [Test, Order(1)]
        public void TestRequired()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.registerBtn.Click();
            Assert.That(signUpPage.emailRequiredMsg.Displayed && signUpPage.passwordRequiredMsg.Displayed
                && signUpPage.confirmPasswordRequiredMsg.Displayed && signUpPage.tosRequiredMsg.Displayed);
        }

        [Test, Order(1)]
        public void TestEvaluatorRequired()
        {
            TestBase.driver.Url = "https://rcs-cosmetics-identity-dev.azurewebsites.net/Account/RegisterEvaluator";
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.registerBtn.Click();
            Assert.That(signUpPage.emailRequiredMsg.Displayed && signUpPage.passwordRequiredMsg.Displayed
                && signUpPage.confirmPasswordRequiredMsg.Displayed && signUpPage.tosRequiredMsg.Displayed);
        }

        [Test, Order(1)]
        public void TestInvalidEmail()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister("InvalidEmail", "", "");
            Assert.That(signUpPage.emailInvalidMsg.Displayed);
        }


        [Test, Order(1)]
        public void TestEvaluatorInvalidEmail()
        {
            TestBase.driver.Url = "https://rcs-cosmetics-identity-dev.azurewebsites.net/Account/RegisterEvaluator";
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister("InvalidEmail", "", "");
            Assert.That(signUpPage.emailInvalidMsg.Displayed);
        }

        [Test, Order(1)]
        public void TestPassDontMatch()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username,"123","321");
            Assert.That(signUpPage.passwordsDontMatchMsg.Displayed);
        }

        [Test, Order(1)]
        public void TestEvaluatorPassDontMatch()
        {
            TestBase.driver.Url = "https://rcs-cosmetics-identity-dev.azurewebsites.net/Account/RegisterEvaluator";
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.username,"123","321");
            Assert.That(signUpPage.passwordsDontMatchMsg.Displayed);
        }

        [Test, Order(1)]
        public void TestPasswordMinCharValidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username, "1!qQ", "1!qQ");
            Assert.That(signUpPage.passwordMinLengthMsg.Displayed);
        }

        [Test, Order(1)]
        public void TestEvaluatorPasswordMinCharValidation()
        {
            TestBase.driver.Url = "https://rcs-cosmetics-identity-dev.azurewebsites.net/Account/RegisterEvaluator";
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.username, "1!qQ", "1!qQ");
            Assert.That(signUpPage.passwordMinLengthMsg.Displayed);
        }
        [Test, Order(1)]
        public void TestPasswordAlphanumericValidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username, "1234qQ", "1234qQ");
            Assert.That(signUpPage.passwordAlphanumericMsg.Displayed);
        }
        [Test, Order(1)]
        public void TestEvaluatorPasswordAlphanumericValidation()
        {
            TestBase.driver.Url = "https://rcs-cosmetics-identity-dev.azurewebsites.net/Account/RegisterEvaluator";
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.username, "1234qQ", "1234qQ");
            Assert.That(signUpPage.passwordAlphanumericMsg.Displayed);
        }
        [Test, Order(1)]
        public void TestPasswordLowercaseValidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username, "12!@QQ", "12!@QQ");
            Assert.That(signUpPage.passwordLowercaseMsg.Displayed);
        }
        [Test, Order(1)]
        public void TestEvaluatorPasswordLowercaseValidation()
        {
            TestBase.driver.Url = "https://rcs-cosmetics-identity-dev.azurewebsites.net/Account/RegisterEvaluator";
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.username, "12!@QQ", "12!@QQ");
            Assert.That(signUpPage.passwordLowercaseMsg.Displayed);
        }
        [Test, Order(1)]
        public void TestPasswordUpercaseValidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username, "12!@qq", "12!@qq");
            Assert.That(signUpPage.passwordUppercaseMsg.Displayed);
        }
        [Test, Order(1)]
        public void TestEvaluatorPasswordUpercaseValidation()
        {
            TestBase.driver.Url = "https://rcs-cosmetics-identity-dev.azurewebsites.net/Account/RegisterEvaluator";
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.username, "12!@qq", "12!@qq");
            Assert.That(signUpPage.passwordUppercaseMsg.Displayed);
        }
        [Test, Order(1)]
        public void TestPasswordNumberValidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.username, "!@qqQQ", "!@qqQQ");
            Assert.That(signUpPage.passwordNumberMsg.Displayed);
        }
        [Test, Order(1)]
        public void TestEvaluatorPasswordNumberValidation()
        {
            TestBase.driver.Url = "https://rcs-cosmetics-identity-dev.azurewebsites.net/Account/RegisterEvaluator";
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.username, "!@qqQQ", "!@qqQQ");
            Assert.That(signUpPage.passwordNumberMsg.Displayed);
        }
        [Test, Order(1)]
        public void TestCancelBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin(); 
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.ClickCancel();
            Assert.That(loginPage.title.Displayed);
        }
        [Test, Order(2)]
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
        [Test, Order(2)]
        public void TestEvaluatorSuccessfulSignup()
        {
            TestBase.driver.Url = "https://rcs-cosmetics-identity-dev.azurewebsites.net/Account/RegisterEvaluator";
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            SignUpTyPage signupTyPage =  signUpPage.DoRegister(TestBase.evaluatorUsername, TestBase.password, TestBase.password);
            Assert.That(signupTyPage.tyMessage.Displayed);
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.evaluatorUsername, TestBase.password);
            string allEmails = mailRepository.GetUnreadMails(Subject.Register);
            var linkParser = new Regex(@"(?:https?://rcs-cosmetics-identity-dev.azurewebsites.net/Account)\S+\b");
            var link = linkParser.Matches(allEmails);
            TestBase.driver.Url = link.SingleOrDefault().ToString().Replace("&amp;","&");
            TyRegistrationPage trp = new TyRegistrationPage(TestBase.driver);
            Assert.That(trp.title.Displayed);
        }
        [Test, Order(3)]
        public void TestEmailTaken()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            SignUpPage signUpPage = loginPage.GetSignUp();
            signUpPage.DoRegister(TestBase.uiUsername, TestBase.password, TestBase.password);
            Assert.That(signUpPage.emailTakenMsg.Displayed);
        }
    }
}
