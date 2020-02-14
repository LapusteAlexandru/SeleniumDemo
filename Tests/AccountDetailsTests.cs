
using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AccountDetailsTests
{
    [TestFixture]
    [Category("AccountDetails")]
    class AccountDetailsTests
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
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage adp = dp.getAccountDetails();
            foreach (var e in adp.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test, Order(2)]
        public void TestRequiredMsgs()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage adp = dp.getAccountDetails();
            adp.submitBtn.Click();
            Thread.Sleep(300);
            foreach (var e in adp.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(adp.requiredMsgs.Count.Equals(10));
            Assert.That(adp.certificationsRequiredMsgs.Displayed);
        }

        [Test, Order(2)]
        public void TestGMCNumberValidation()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage adp = dp.getAccountDetails();
            adp.gmcNumberInput.SendKeys("123");
            adp.submitBtn.Click();
            Assert.That(adp.gmcNumberMinCharValidation.Displayed);
        }

        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage adp = dp.getAccountDetails();
            Assert.That(adp.statusIndicator.GetAttribute("class").Contains("not-started"));
            adp.CompleteForm(TestBase.userTitle, TestBase.userFirstName, TestBase.userLastName, TestBase.userAddress, TestBase.userPhone,TestBase.userGender, TestBase.userGmcNumber.ToString(), TestBase.userGmcSpecialty, TestBase.userCareerGrade);
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(adp.accountSubmittedMsg));
            foreach (var e in adp.GetMainElements())
                Assert.That(!e.Enabled || e.GetAttribute("class").Contains("mat-checkbox-disabled") || e.GetAttribute("class").Contains("mat-select-disabled"));
            Assert.That(adp.statusIndicator.GetAttribute("class").Contains("completed"));

        }

        [Test, Order(2)]
        public void TestNoOfCharLimit()
        {
            string testText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum";
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage adp = dp.getAccountDetails();
            adp.CompleteForm(testText, testText, testText, testText, testText, TestBase.userGender, TestBase.userGmcNumber.ToString(), TestBase.userGmcSpecialty, TestBase.userCareerGrade);
            foreach (var e in adp.limitReachedMsgs)
                Assert.That(e.Displayed);
            Assert.That(adp.limitReachedMsgs.Count.Equals(5));

        }

        [Test]
        public void TestInformationPanelCloses()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage adp = dp.getAccountDetails();
            adp.inforPanel.Click();
            Thread.Sleep(300);
            Assert.That(adp.inforPanel.GetAttribute("aria-expanded").Equals("false") && !adp.inforPanelContent.Displayed);
        }

        [Test]
        public void TestInformationPanelOpens()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage adp = dp.getAccountDetails();
            adp.inforPanel.Click();
            adp.inforPanel.Click();
            Thread.Sleep(300);
            Assert.That(adp.inforPanel.GetAttribute("aria-expanded").Equals("true") && adp.inforPanelContent.Displayed);
        }
    }
}
