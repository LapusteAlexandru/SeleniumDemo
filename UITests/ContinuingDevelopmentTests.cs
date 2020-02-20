using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;

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
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            ContinuingDevelopmentPage continuingDevelopmentPage = dashboardPage.getContinuingDevelopment();
            foreach (var e in continuingDevelopmentPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            ContinuingDevelopmentPage continuingDevelopmentPage = dashboardPage.getContinuingDevelopment();
            continuingDevelopmentPage.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(continuingDevelopmentPage.title));
            dashboardPage.openSideMenuIfClosed();
            Assert.That(continuingDevelopmentPage.statusIndicator.GetAttribute("class").Contains("completed"));
        }
    }
}
