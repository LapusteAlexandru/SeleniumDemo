using Pages;
using NUnit.Framework;
using RCoS;
using Helpers;
using System.Threading;
using System.Text.RegularExpressions;
using System.Linq;
using static Helpers.MailSubjectEnum;

namespace EvaluatorSignUpTests
{
    [TestFixture]
    [Category("Signup")]
    class EvaluatorSignUpTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteUserData("[dbo].[ApplicationReviews]", TestBase.evaluatorUsername);
            TestBase.deleteUserData("[dbo].[AssignedApplications]", TestBase.evaluatorUsername);
            TestBase.deleteUserData("[dbo].[Users]", TestBase.evaluatorUsername);
            TestBase.deleteUserData("[dbo].[AspNetUsers]", TestBase.evaluatorUsername);
        }
        [SetUp]
        public void Setup()
        {
            TestBase.RootInit();

            TestBase.driver.Url = "https://rcs-cosmetics-identity-dev.azurewebsites.net/Account/RegisterEvaluator";
        }

        [TearDown]
        public void Teardown()
        {
            TestBase.TakeScreenShot();
            TestBase.driver.Quit();
        }

       

        [Test, Order(1)]
        public void TestEvaluatorPageLoads()
        {
            SignUpPage signupPage = new SignUpPage(TestBase.driver);
            DashboardPage dashboardPage = new DashboardPage(TestBase.driver);
            foreach (var e in signupPage.GetEvaluatorMainElements())
                Assert.That(e.Displayed);
            Assert.False(TestBase.ElementIsPresent(signupPage.cancelBtn));
            Assert.False(TestBase.ElementIsPresent(signupPage.loginBtn));
            Assert.False(TestBase.ElementIsPresent(dashboardPage.homeBtn));
        }

       
        [Test, Order(1)]
        public void TestEvaluatorRequired()
        {
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.registerBtn.Click();
            Thread.Sleep(500);
            foreach (var e in signUpPage.GetRequiredElements())
                Assert.That(e.Displayed);
        }

      
        [Test, Order(1)]
        public void TestEvaluatorInvalidEmail()
        {
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister("InvalidevaluatorEmail", "", "");
            Assert.That(signUpPage.emailInvalidMsg.Displayed);
        }

       
        [Test, Order(1)]
        public void TestEvaluatorPassDontMatch()
        {
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.evaluatorUsername, "123", "321");
            Assert.That(signUpPage.passwordsDontMatchMsg.Displayed);
        }

       
        [Test, Order(1)]
        public void TestEvaluatorPasswordMinCharValidation()
        {
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.evaluatorUsername, "1!qQ", "1!qQ");
            Assert.That(signUpPage.passwordMinLengthMsg.Displayed);
        }
        
        [Test, Order(1)]
        public void TestEvaluatorPasswordAlphanumericValidation()
        {
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.evaluatorUsername, "1234qQ", "1234qQ");
            Assert.That(signUpPage.passwordAlphanumericMsg.Displayed);
        }
        
        public void TestEvaluatorPasswordLowercaseValidation()
        {
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.evaluatorUsername, "12!@QQ", "12!@QQ");
            Assert.That(signUpPage.passwordLowercaseMsg.Displayed);
        }
        
        [Test, Order(1)]
        public void TestEvaluatorPasswordUpercaseValidation()
        {
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.evaluatorUsername, "12!@qq", "12!@qq");
            Assert.That(signUpPage.passwordUppercaseMsg.Displayed);
        }
       
        [Test, Order(1)]
        public void TestEvaluatorPasswordNumberValidation()
        {
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            signUpPage.DoRegister(TestBase.evaluatorUsername, "!@qqQQ", "!@qqQQ");
            Assert.That(signUpPage.passwordNumberMsg.Displayed);
        }
        
        [Test, Order(2)]
        public void TestEvaluatorSuccessfulSignup()
        {
            SignUpPage signUpPage = new SignUpPage(TestBase.driver);
            SignUpTyPage signupTyPage = signUpPage.DoRegister(TestBase.evaluatorUsername, TestBase.password, TestBase.password);
            Assert.That(signupTyPage.tyMessage.Displayed);
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, TestBase.evaluatorUsername, TestBase.password);
            string allEmails = mailRepository.GetUnreadMails(Subject.Register);
            var linkParser = new Regex(@"(?:https?://rcs-cosmetics-identity-dev.azurewebsites.net/Account)\S+\b");
            var link = linkParser.Matches(allEmails);
            TestBase.driver.Url = link.SingleOrDefault().ToString().Replace("&amp;", "&");
            TyRegistrationPage trp = new TyRegistrationPage(TestBase.driver);
            Assert.That(trp.title.Displayed);
        }
        
    }
}
