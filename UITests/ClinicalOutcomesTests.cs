using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace ClinicalOutcomesTests
{
    [TestFixture]
    [Category("ClinicalOutcomes")]
    class ClinicalOutcomesTests
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
            ClinicalOutcomesPage clinicalOutcomesPage = dashboardPage.getClinicalOutcomes();
            foreach (var e in clinicalOutcomesPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(2)]
        public void TestRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            ClinicalOutcomesPage clinicalOutcomesPage = dashboardPage.getClinicalOutcomes();
            clinicalOutcomesPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in clinicalOutcomesPage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(clinicalOutcomesPage.requiredMsgs.Count.Equals(2));
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            ClinicalOutcomesPage clinicalOutcomesPage = dashboardPage.getClinicalOutcomes();
            clinicalOutcomesPage.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(clinicalOutcomesPage.title));
            dashboardPage.openSideMenuIfClosed();
            Assert.That(clinicalOutcomesPage.CMARequirementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(clinicalOutcomesPage.statusIndicator.GetAttribute("class").Contains("completed"));
        }
    }
}
