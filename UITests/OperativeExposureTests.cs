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
            OperativeExposurePage ope = dashboardPage.getOperativeExposure();
            foreach (var e in ope.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(1)]
        public void TestRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            OperativeExposurePage operativeExposurePage = dashboardPage.getOperativeExposure();
            operativeExposurePage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in operativeExposurePage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(operativeExposurePage.requiredMsgs.Count.Equals(3));
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            OperativeExposurePage operativeExposurePage = dashboardPage.getOperativeExposure();
            operativeExposurePage.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(operativeExposurePage.title));
            dashboardPage.openSideMenuIfClosed();
            Assert.That(operativeExposurePage.proceduresCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(operativeExposurePage.operativeExposureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(operativeExposurePage.statusIndicator.GetAttribute("class").Contains("completed"));
        }
    }
}
