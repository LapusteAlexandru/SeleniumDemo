﻿using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
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
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck();
            foreach (var e in paymentCheckPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(1)]
        public void TestRequiredMsg()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck();
            paymentCheckPage.submitBtn.Click();
            Thread.Sleep(500);
            foreach (var e in paymentCheckPage.requiredMsgs)
                Assert.That(e.Displayed);
        }

        [Test]
        public void TestAlreadyPaid()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck();
            ApplicationThankYouPage tyPage = paymentCheckPage.getThankYou();
            Assert.That(tyPage.thankYouMsgCard.Displayed);

        }
        [Test]
        public void TestHaventPaid()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck();
            PaymentPage paymentPage = paymentCheckPage.getPayment();
            Assert.That(paymentPage.paymentForm.Displayed);

        }

        
    }
}
