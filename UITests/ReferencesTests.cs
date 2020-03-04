using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;

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
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            ReferencesPage referencesPage = dashboardPage.getReferences();
            foreach (var e in referencesPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            ReferencesPage referencesPage = dashboardPage.getReferences();
            referencesPage.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(referencesPage.title));
            dashboardPage.openSideMenuIfClosed();
            Assert.That(referencesPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }
    }
}
