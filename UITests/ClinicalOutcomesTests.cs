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
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[ClinicalOutcomes]", TestBase.uiUsername, "ClinicalOutcomes");
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

            var clinicalOutcomesPage = getClinicalOutcomes().Item1;
            foreach (var e in clinicalOutcomesPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(1)]
        public void TestRequiredMsgs()
        {

            var clinicalOutcomesPage = getClinicalOutcomes().Item1;
            clinicalOutcomesPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in clinicalOutcomesPage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(clinicalOutcomesPage.requiredMsgs.Count.Equals(2));
        }
        [Test,Order(2)]
        public void TestSubmitSuccessfully()
        {

            var (clinicalOutcomesPage, dashboardPage) = getClinicalOutcomes();
            clinicalOutcomesPage.CompleteForm("PHINLink","png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(clinicalOutcomesPage.title));
            Assert.That(clinicalOutcomesPage.CMARequirementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(clinicalOutcomesPage.phinLink.GetAttribute("value").Equals("PHINLink"));

            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            Assert.That(clinicalOutcomesPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }

        [Test]
        public void TestEditSuccessfully()
        {
            var (clinicalOutcomesPage, dashboardPage) = getClinicalOutcomes();
            clinicalOutcomesPage.CompleteForm("UpdatedPHINLink","png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(clinicalOutcomesPage.title));
            Assert.That(clinicalOutcomesPage.CMARequirementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(clinicalOutcomesPage.phinLink.GetAttribute("value").Equals("UpdatedPHINLink"));
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            Assert.That(clinicalOutcomesPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }

        private (ClinicalOutcomesPage,DashboardPage) getClinicalOutcomes()
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            ClinicalOutcomesPage clinicalOutcomesPage = dashboardPage.getClinicalOutcomes();
            return (clinicalOutcomesPage,dashboardPage);
        }
    }
}
