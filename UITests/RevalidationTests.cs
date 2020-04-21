using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using static Helpers.RadioButtonEnum;

namespace RevalidationTests
{
    [TestFixture]
    [Category("Revalidation")]
    class RevalidationTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[Revalidations]", TestBase.uiUsername, "Revalidation");
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
            RevalidationPage revalidationPage = dashboardPage.getRevalidation();
            foreach (var e in revalidationPage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(1)]
        public void TestRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            RevalidationPage revalidationPage = dashboardPage.getRevalidation();
            revalidationPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in revalidationPage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(revalidationPage.requiredMsgs.Count.Equals(5));
        }
        [Test, Order(2)]
        public void TestSubmitSuccessfully()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            RevalidationPage revalidationPage = dashboardPage.getRevalidation();
            revalidationPage.CompleteForm(YesOrNoRadio.Yes,"png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(revalidationPage.title));
            Assert.That(revalidationPage.declareAppraisalYes.GetAttribute("class").Contains("mat-radio-checked"));
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            Assert.That(revalidationPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }
        [Test]
        public void TestEditSuccessfully()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            RevalidationPage revalidationPage = dashboardPage.getRevalidation();
            YesOrNoRadio radioOption = YesOrNoRadio.No;
            revalidationPage.CompleteForm(radioOption);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(revalidationPage.title));
            Assert.That(revalidationPage.declareAppraisalNo.GetAttribute("class").Contains("mat-radio-checked"));
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            Assert.That(revalidationPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }
    }
}
