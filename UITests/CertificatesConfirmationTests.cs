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
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            foreach (var e in certificateConfirmationPage.GetMainElements())
                Assert.That(e.Displayed);
            Assert.That(certificateConfirmationPage.earsSurgeryCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
        }
        [Test]
        public void TestRequiredMsg()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
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

        [Test, Order(2)]
        public void TestSubmit()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            PaymentCheckPage paymentCheckPage = certificateConfirmationPage.getPaymentCheck();
            Assert.That(paymentCheckPage.title.Displayed);
        }

        [Test, Order(3)]
        public void TestEdit()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            CertificateConfirmationPage certificateConfirmationPage = dashboardPage.getSubmitApplication();
            certificateConfirmationPage.breastSurgeryCheckbox.Click();
            certificateConfirmationPage.getPaymentCheck();
            TestBase.driver.Navigate().Refresh();
            dashboardPage.getSubmitApplication();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(certificateConfirmationPage.submitBtn));
            Assert.That(certificateConfirmationPage.breastSurgeryCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            dashboardPage.openSideMenuIfClosed();
            AccountDetailsPage accountDetailspage = dashboardPage.getAccountDetails();
            Assert.That(accountDetailspage.breastSurgeryCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            dashboardPage.getSubmitApplication();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(certificateConfirmationPage.submitBtn));
            certificateConfirmationPage.breastSurgeryCheckbox.Click();
            certificateConfirmationPage.getPaymentCheck();
            
        }
    }
}
