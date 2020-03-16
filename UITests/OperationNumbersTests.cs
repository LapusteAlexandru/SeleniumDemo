using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace OperativeExposureTests
{
    [TestFixture]
    [Category("OperativeExposure")]
    class OperativeExposureTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[OperativeExposures]", TestBase.uiUsername, "OperativeExposure");
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
            OperationNumbersPage ope = dashboardPage.getOperationNUmbers();
            foreach (var e in ope.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(1)]
        public void TestRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            OperationNumbersPage OperationNumbersPage = dashboardPage.getOperationNUmbers();
            OperationNumbersPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in OperationNumbersPage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(OperationNumbersPage.requiredMsgs.Count.Equals(3));
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            OperationNumbersPage OperationNumbersPage = dashboardPage.getOperationNUmbers();
            OperationNumbersPage.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(OperationNumbersPage.title));
            dashboardPage.openSideMenuIfClosed();
            Assert.That(OperationNumbersPage.proceduresCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(OperationNumbersPage.operativeExposureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(OperationNumbersPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }
    }
}
