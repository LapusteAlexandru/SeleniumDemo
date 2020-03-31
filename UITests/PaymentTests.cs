using NUnit.Framework;
using Pages;
using RCoS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PaymentTests
{
    [TestFixture]
    [Category("PaymentCheck")]
    class PaymentTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[Applications]", TestBase.appUsername, "Status", 1);
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
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck();
            PaymentPage paymentPage = paymentCheckPage.getPayment();
            foreach (var e in paymentPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(1)]
        public void TestRequiredMsg()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck(); 
            PaymentPage paymentPage = paymentCheckPage.getPayment();
            paymentPage.submitBtn.Click();
            Thread.Sleep(500);
            foreach (var e in paymentPage.requiredMsgs)
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestMakePayment()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck(); 
            PaymentPage paymentPage = paymentCheckPage.getPayment();
            ApplicationThankYouPage tyPage = paymentPage.Submit(TestBase.cardNumber,TestBase.userFirstName,TestBase.userLastName,TestBase.expiryMonthDate,TestBase.expiryYearDate,TestBase.cvv);
            Assert.That(tyPage.thankYouMsg.Displayed);
        }
        [Test, Order(2)]
        public void TestShortCardNumber()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck(); 
            PaymentPage paymentPage = paymentCheckPage.getPayment();
            paymentPage.Submit("411111111111",TestBase.userFirstName,TestBase.userLastName,TestBase.expiryMonthDate,TestBase.expiryYearDate,TestBase.cvv);
            Assert.That(paymentPage.shortCardNumberMsg.Displayed);
        }
        [Test, Order(2)]
        public void TestShortCvv()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck(); 
            PaymentPage paymentPage = paymentCheckPage.getPayment();
            paymentPage.Submit(TestBase.cardNumber,TestBase.userFirstName,TestBase.userLastName,TestBase.expiryMonthDate,TestBase.expiryYearDate,"12");
            Assert.That(paymentPage.shortCardNumberMsg.Displayed);
        }
        [Test, Order(2)]
        public void TestCharLimit()
        {
            string testText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum";

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck(); 
            PaymentPage paymentPage = paymentCheckPage.getPayment();
            paymentPage.Submit(TestBase.cardNumber, testText, testText, TestBase.expiryMonthDate,TestBase.expiryYearDate,TestBase.cvv);
            foreach (var e in paymentPage.limitReachedMsgs)
                Assert.That(e.Displayed);
        }
        /*[Test, Order(2)]
        public void TestExpiryDate()
        {
            string testText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum";

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck(); 
            PaymentPage paymentPage = paymentCheckPage.getPayment();
            paymentPage.Submit(TestBase.cardNumber, testText, testText, TestBase.expiryMonthDate,TestBase.expiryYearDate,TestBase.cvv);
            foreach (var e in paymentPage.limitReachedMsgs)
                Assert.That(e.Displayed);
        }*/
    }
}
