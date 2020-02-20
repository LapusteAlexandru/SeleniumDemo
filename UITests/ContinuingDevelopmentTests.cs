using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ContinuingDevelopmentTests
{
    [TestFixture]
    [Category("ContinuingDevelopment")]
    class ContinuingDevelopmentTests
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
            ContinuingDevelopmentPage cdp = dp.getContinuingDevelopment();
            foreach (var e in cdp.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ContinuingDevelopmentPage cdp = dp.getContinuingDevelopment();
            cdp.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(cdp.title));
            dp.openSideMenuIfClosed();
            Assert.That(cdp.statusIndicator.GetAttribute("class").Contains("completed"));
        }
    }
}
