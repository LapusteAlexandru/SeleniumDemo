using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using static Helpers.RadioButtonEnum;

namespace ProfessionalInsuranceTests
{
    [TestFixture]
    [Category("ProfessionalInsurance")]
    class ProfessionalInsuranceTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[ProfessionalIndemnityInsurances]", TestBase.uiUsername, "ProfessionalIndemnityInsurance");
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
            ProfessionalInsurancePage professionalInsurancePage = dashboardPage.getProfessionalInsurance();
            foreach (var e in professionalInsurancePage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(2)]
        public void TestRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            ProfessionalInsurancePage professionalInsurancePage = dashboardPage.getProfessionalInsurance();
            professionalInsurancePage.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in professionalInsurancePage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(professionalInsurancePage.requiredMsgs.Count.Equals(4));
        }
        [Test, Order(3)]
        public void TestSubmitSuccessfully()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            ProfessionalInsurancePage professionalInsurancePage = dashboardPage.getProfessionalInsurance();
            YesOrNoRadio radioOption = YesOrNoRadio.Yes;
            professionalInsurancePage.CompleteForm(radioOption,"png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalInsurancePage.title));
            Assert.That(professionalInsurancePage.indemnityArrangementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(professionalInsurancePage.disclosedNatureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(professionalInsurancePage.yesPracticeRadio.GetAttribute("class").Contains("mat-radio-checked"));
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            Assert.That(professionalInsurancePage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }
        
        [Test]
        public void TestEditSuccessfully()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            ProfessionalInsurancePage professionalInsurancePage = dashboardPage.getProfessionalInsurance();
            YesOrNoRadio radioOption = YesOrNoRadio.No;
            professionalInsurancePage.CompleteForm(radioOption);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalInsurancePage.title));
            Assert.That(professionalInsurancePage.indemnityArrangementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(professionalInsurancePage.disclosedNatureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(professionalInsurancePage.noPracticeRadio.GetAttribute("class").Contains("mat-radio-checked"));
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            Assert.That(professionalInsurancePage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }
    }
}
