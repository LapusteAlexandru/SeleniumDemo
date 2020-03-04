using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace ProfessionalBehavioursTests
{
    [TestFixture]
    [Category("ProfessionalBehaviours")]
    class ProfessionalBehavioursTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[ProfessionalBehaviours]", TestBase.uiUsername, "ProfessionalBehaviours");
        }
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
            ProfessionalBehavioursPage professionalBehavioursPage = dashboardPage.getProfessionalBehaviours();
            foreach (var e in professionalBehavioursPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(2)]
        public void TestRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            ProfessionalBehavioursPage professionalBehavioursPage = dashboardPage.getProfessionalBehaviours();
            professionalBehavioursPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in professionalBehavioursPage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(professionalBehavioursPage.requiredMsgs.Count.Equals(2));
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            ProfessionalBehavioursPage professionalBehavioursPage = dashboardPage.getProfessionalBehaviours();
            professionalBehavioursPage.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalBehavioursPage.title));
            dashboardPage.openSideMenuIfClosed();
            Assert.That(professionalBehavioursPage.professionalResponsibilitiesCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(professionalBehavioursPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }
    }
}
