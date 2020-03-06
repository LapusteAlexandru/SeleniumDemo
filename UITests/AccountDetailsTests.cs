
using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace AccountDetailsTests
{
    [TestFixture]
    [Category("AccountDetails")]
    class AccountDetailsTests
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
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            foreach (var e in accountDetailsPage.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test, Order(2)]
        public void TestRequiredMsgs()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            accountDetailsPage.submitBtn.Click();
            Thread.Sleep(300);
            foreach (var e in accountDetailsPage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(accountDetailsPage.requiredMsgs.Count.Equals(10));
            Assert.That(accountDetailsPage.certificationsRequiredMsgs.Displayed);
        }

        [Test, Order(2)]
        public void TestGMCNumberValidation()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            accountDetailsPage.gmcNumberInput.SendKeys("123");
            accountDetailsPage.submitBtn.Click();
            Assert.That(accountDetailsPage.gmcNumberMinCharValidation.Displayed);
        }

        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            dashboardPage.openSideMenuIfClosed();
            Assert.That(accountDetailsPage.statusIndicator.GetAttribute("mattooltip").Contains("Not Stared"));
            accountDetailsPage.CompleteForm(TestBase.userTitle, TestBase.userFirstName, TestBase.userLastName, TestBase.userAddress, TestBase.userPhone,TestBase.userGender, TestBase.userGmcNumber.ToString(), TestBase.userGmcSpecialty, TestBase.userCareerGrade);
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(accountDetailsPage.accountSubmittedMsg));
            foreach (var e in accountDetailsPage.GetMainElements())
                Assert.That(!e.Enabled || e.GetAttribute("class").Contains("mat-checkbox-disabled") || e.GetAttribute("class").Contains("mat-select-disabled"));
            dashboardPage.openSideMenuIfClosed();
            Assert.That(accountDetailsPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));

        }

        [Test, Order(2)]
        public void TestNoOfCharLimit()
        {
            string testText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum";
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            accountDetailsPage.CompleteForm(testText, testText, testText, testText, testText, TestBase.userGender, TestBase.userGmcNumber.ToString(), TestBase.userGmcSpecialty, TestBase.userCareerGrade);
            foreach (var e in accountDetailsPage.limitReachedMsgs)
                Assert.That(e.Displayed);
            Assert.That(accountDetailsPage.limitReachedMsgs.Count.Equals(5));

        }

        [Test]
        public void TestInformationPanelCloses()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            accountDetailsPage.inforPanel.Click();
            Thread.Sleep(300);
            Assert.That(accountDetailsPage.inforPanel.GetAttribute("aria-expanded").Equals("false") && !accountDetailsPage.inforPanelContent.Displayed);
        }

        [Test]
        public void TestInformationPanelOpens()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            accountDetailsPage.inforPanel.Click();
            accountDetailsPage.inforPanel.Click();
            Thread.Sleep(300);
            Assert.That(accountDetailsPage.inforPanel.GetAttribute("aria-expanded").Equals("true") && accountDetailsPage.inforPanelContent.Displayed);
        }
    }
}
