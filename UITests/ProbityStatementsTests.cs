using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using static Helpers.RadioButtonEnum;

namespace ProbityStatementsTests
{
    [TestFixture]
    [Category("ProbityStatements")]
    class ProbityStatementsTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[Applications]", TestBase.uiUsername, "Status");
            TestBase.deleteSectionData("[dbo].[Documents]", TestBase.uiUsername);
            TestBase.deleteSectionData("[dbo].[ProbityStatements]", TestBase.uiUsername, "ProbityStatement");
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

            ProbityStatementsPage probityStatementsPage = getProbityStatements().Item1;
            foreach (var e in probityStatementsPage.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test, Order(2)]
        public void TestSubmitApplicationDisabled()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@id='submit']")));
            Assert.True(TestBase.driver.FindElement(By.XPath("//a[@id='submit']/parent::li")).GetAttribute("class").Contains("disabled"));
        }
        [Test, Order(2)]
        public void TestRequiredMsgs()
        {
           
            ProbityStatementsPage probityStatementsPage = getProbityStatements().Item1;
            probityStatementsPage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in probityStatementsPage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(probityStatementsPage.requiredMsgs.Count.Equals(3));
        }
        [Test, Order(3)]
        public void TestSubmitSuccessfully()
        {

            var (probityStatementsPage, dashboardPage) = getProbityStatements();
            ProbityRadio radioOption = ProbityRadio.Nothing;
            probityStatementsPage.CompleteForm(radioOption);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(probityStatementsPage.title));
            Assert.That(probityStatementsPage.professionalObligationsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(probityStatementsPage.suspensionCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(probityStatementsPage.nothingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"));
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openSideMenuIfClosed();
            Assert.That(probityStatementsPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }
        [Test]
        public void TestEditSuccessfully()
        {
            var (probityStatementsPage, dashboardPage) = getProbityStatements();
            ProbityRadio radioOption = ProbityRadio.Nothing;
            if (probityStatementsPage.nothingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"))
                radioOption = ProbityRadio.Something;
            probityStatementsPage.CompleteForm(radioOption);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(probityStatementsPage.title));
            Assert.That(probityStatementsPage.professionalObligationsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(probityStatementsPage.suspensionCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            if (radioOption.Equals(ProbityRadio.Nothing))
                Assert.That(probityStatementsPage.nothingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"));
            else
                Assert.That(probityStatementsPage.somethingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"));
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openSideMenuIfClosed();
            Assert.That(probityStatementsPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }

        private (ProbityStatementsPage,DashboardPage) getProbityStatements()
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            ProbityStatementsPage probityStatementsPage = dashboardPage.getProbityStatements();
            return (probityStatementsPage, dashboardPage);
        }
    }
}
