using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PaymentCheckTests
{
    [TestFixture]
    [Category("PaymentCheck")]
    class PaymentCheckTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[Users]", TestBase.appUsername, "Payments",5);
            TestBase.deleteSectionData("[dbo].[Applications]", TestBase.appUsername, "Status");
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

            var paymentCheckPage = getPaymentCheck().Item1;
            foreach (var e in paymentCheckPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(1)]
        public void TestRequiredMsg()
        {

            var paymentCheckPage = getPaymentCheck().Item1;
            paymentCheckPage.submitBtn.Click();
            Thread.Sleep(500);
            foreach (var e in paymentCheckPage.requiredMsgs)
                Assert.That(e.Displayed);
        }

        [Test]
        public void TestAlreadyPaid()
        {

            var (paymentCheckPage, dashboardPage) = getPaymentCheck();
            PaymentThankYouPage tyPage = paymentCheckPage.getThankYou();
            Assert.That(tyPage.thankYouMsgCard.Displayed);
            Thread.Sleep(500);
            Assert.True(dashboardPage.currentAppBtn.Displayed);
        }
        [Test, Order(1)]
        public void TestHaventPaid()
        {
            var pages = getPaymentCheck();
            var paymentCheckPage = pages.Item1;
            PaymentPage paymentPage = paymentCheckPage.getPayment();
            Assert.That(paymentPage.paymentForm.Displayed);

        }

        private (PaymentCheckPage,DashboardPage) getPaymentCheck()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.appUsername, TestBase.password);
            PaymentCheckPage paymentCheckPage = dashboardPage.getPaymentCheck();
            return (paymentCheckPage, dashboardPage);
        }
    }
}
