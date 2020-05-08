using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;

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

            var continuingDevelopmentPage = getContinuingDevelopment().Item1;
            foreach (var e in continuingDevelopmentPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            var (continuingDevelopmentPage, dashboardPage) = getContinuingDevelopment();
            continuingDevelopmentPage.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(continuingDevelopmentPage.title));
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            Assert.That(continuingDevelopmentPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }

        private (ContinuingDevelopmentPage,DashboardPage) getContinuingDevelopment()
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            ContinuingDevelopmentPage continuingDevelopmentPage = dashboardPage.getContinuingDevelopment();
            return (continuingDevelopmentPage,dashboardPage);
        }
    }
}
