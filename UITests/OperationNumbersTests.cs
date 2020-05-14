using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace OperativeExposureTests
{
    [TestFixture]
    [Category("OperationNumbers")]
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
            var operationNumbersPage = getOperationNumbers().Item1;
            foreach (var e in operationNumbersPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(1)]
        public void TestRequiredMsgs()
        {
            var operationNumbersPage = getOperationNumbers().Item1;
            operationNumbersPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in operationNumbersPage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(operationNumbersPage.requiredMsgs.Count.Equals(3));
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            var(operationNumbersPage, dashboardPage) = getOperationNumbers(); 
            operationNumbersPage.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(operationNumbersPage.title));
            Assert.That(operationNumbersPage.option1.GetAttribute("class").Contains("mat-radio-checked"));
            Assert.That(operationNumbersPage.proceduresCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(operationNumbersPage.operativeExposureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            Assert.That(operationNumbersPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }

        private (OperationNumbersPage,DashboardPage) getOperationNumbers()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            OperationNumbersPage operationNumbersPage = dashboardPage.getOperationNUmbers();
            return (operationNumbersPage, dashboardPage);
        }
    }
}
