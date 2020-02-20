using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ReferencesTests
{
    [TestFixture]
    [Category("References")]
    class ReferencesTests
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
            ReferencesPage rp = dp.getReferences();
            foreach (var e in rp.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ReferencesPage rp = dp.getReferences();
            rp.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(rp.title));
            dp.openSideMenuIfClosed();
            Assert.That(rp.statusIndicator.GetAttribute("class").Contains("completed"));
        }
    }
}
