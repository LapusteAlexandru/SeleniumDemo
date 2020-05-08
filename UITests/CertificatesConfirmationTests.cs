using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CertificatesConfirmationTests
{
    [TestFixture]
    [Category("CertificatesConfirmation")]
    class CertificatesConfirmationTests
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

            CertificateConfirmationPage certificateConfirmationPage = getCertificationConfirmPage();
            foreach (var e in certificateConfirmationPage.GetMainElements())
                Assert.That(e.Displayed);
            Assert.That(certificateConfirmationPage.earsSurgeryCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
        }
        [Test, Order(1)]
        public void TestRequiredMsg()
        {

            CertificateConfirmationPage certificateConfirmationPage = getCertificationConfirmPage();
            IList<IWebElement> checkboxes = certificateConfirmationPage.checkboxes;
            foreach(var e in checkboxes)
            {
                if (e.GetAttribute("class").Contains("mat-checkbox-checked"))
                    e.Click();
            }
            certificateConfirmationPage.submitBtn.Click();
            foreach (var e in certificateConfirmationPage.requiredMsgs)
                Assert.That(e.Displayed);
        }

        [Test]
        public void TestSubmit()
        {
            CertificateConfirmationPage certificateConfirmationPage = getCertificationConfirmPage();
            ApplicationThankYouPage appThankYou = certificateConfirmationPage.Submit();
            Assert.That(appThankYou.thankYouMsg.Displayed);
        }

        private CertificateConfirmationPage getCertificationConfirmPage()
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            return certificateConfirmationPage;
        }

    }
}
