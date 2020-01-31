
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
            TestBase.driver.Quit();
        }

        [Test]
        public void TestPageLoads()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage adp = dp.getAccountDetails();
            foreach (var e in adp.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test]
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

        [Test]
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
            adp.CompleteForm("Dr.","Alex","Lapuste","5/17/1992","Timisoara","123123123"," Male ","1231231", " General Surgery ", " Associate Specialist ");
            Thread.Sleep(500);
            Assert.That(adp.accountSubmittedMsg.Displayed);
            foreach (var e in adp.GetMainElements())
                Assert.That(!e.Enabled || e.GetAttribute("class").Contains("mat-checkbox-disabled") || e.GetAttribute("class").Contains("mat-select-disabled"));
            Assert.That(adp.statusIndicator.GetAttribute("class").Contains("completed"));

        }

        [Test]
        public void TestNoOfCharLimit()
        {
            string testText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum";
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage adp = dp.getAccountDetails();
            adp.CompleteForm(testText, testText, testText, "5/17/1992", testText, testText, " Male ","1231231", " General Surgery ", " Associate Specialist ");
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
