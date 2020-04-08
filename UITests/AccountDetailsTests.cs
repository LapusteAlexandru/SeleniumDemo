
using NUnit.Framework;
using OpenQA.Selenium;
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

        [Test, Order(1)]
        public void TestEvaluatorPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.evaluatorUsername, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            foreach (var e in accountDetailsPage.GetEvaluatorMainElements())
                Assert.That(e.Displayed);
        }

        [Test, Order(2)]
        public void TestRequiredMsgs()
        {

            AccountDetailsPage accountDetailsPage = TriggerRequiredMsgs(TestBase.username);
            foreach (var e in accountDetailsPage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(accountDetailsPage.requiredMsgs.Count.Equals(10));
            Assert.That(accountDetailsPage.certificationsRequiredMsgs.Displayed);
        }

        [Test, Order(2)]
        public void TestEvaluatorRequiredMsgs()
        {
            AccountDetailsPage accountDetailsPage = TriggerRequiredMsgs(TestBase.evaluatorUsername);
            foreach (var e in accountDetailsPage.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(accountDetailsPage.requiredMsgs.Count.Equals(10));
        }

        [Test, Order(2)]
        public void TestGMCNumberValidation()
        {
            AccountDetailsPage accountDetailsPage = TriggerGMCNumberValidatio(TestBase.username);
            Assert.That(accountDetailsPage.gmcNumberMinCharValidation.Displayed);
        }

        [Test, Order(2)]
        public void TestEvaluatorGMCNumberValidation()
        {

            AccountDetailsPage accountDetailsPage = TriggerGMCNumberValidatio(TestBase.evaluatorUsername);
            Assert.That(accountDetailsPage.gmcNumberMinCharValidation.Displayed);
        }
        [Test, Order(2)]
        public void TestEmptyApplicationRequests()
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            Assert.That(applicationRequestsPage.noRequestsMsg.Displayed);
            Assert.False(applicationRequestsPage.requestsTable.Displayed);
        }
        [Test, Order(2)]
        public void TestEmptyRegistrationRequests()
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            RegistrationRequestsPage registrationRequestsPage = dashboardPage.getRegistrationRequests();
            Assert.That(registrationRequestsPage.noRequestsMsg.Displayed);
            Assert.False(registrationRequestsPage.requestsTable.Displayed);
        }

        [Test]
        public void TestSubmitSuccessfully()
        {
            DashboardPage dashboardPage = new DashboardPage(TestBase.driver);
            AccountDetailsPage accountDetailsPage = Submit(TestBase.username);
            foreach (var e in accountDetailsPage.GetMainElements())
                Assert.That(!e.Enabled || e.GetAttribute("class").Contains("mat-checkbox-disabled") || e.GetAttribute("class").Contains("mat-select-disabled"));
            dashboardPage.openSideMenuIfClosed();
            Assert.That(accountDetailsPage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));

        }
        [Test]
        public void TestEvaluatorSubmitSuccessfully()
        {
            AccountDetailsPage accountDetailsPage = Submit(TestBase.evaluatorUsername);
            Assert.That(accountDetailsPage.accountSubmittedMsg.Displayed);

        }

        [Test, Order(2)]
        public void TestNoOfCharLimit()
        {
           AccountDetailsPage accountDetailsPage = TriggerCharLimitErrors(TestBase.username);
           foreach (var e in accountDetailsPage.limitReachedMsgs)
                Assert.That(e.Displayed);
            Assert.That(accountDetailsPage.limitReachedMsgs.Count.Equals(5));

        }
        [Test, Order(2)]
        public void TestEvaluatorNoOfCharLimit()
        {
           AccountDetailsPage accountDetailsPage = TriggerCharLimitErrors(TestBase.evaluatorUsername);
           foreach (var e in accountDetailsPage.limitReachedMsgs)
                Assert.That(e.Displayed);
            Assert.That(accountDetailsPage.limitReachedMsgs.Count.Equals(5));

        }

        [Test]
        public void TestInformationPanelCloses()
        {
            AccountDetailsPage accountDetailsPage = CloseInfoPanel(TestBase.username);
            Assert.That(accountDetailsPage.inforPanel.GetAttribute("aria-expanded").Equals("false") && !accountDetailsPage.inforPanelContent.Displayed);
        }

        [Test]
        public void TestEvaluatorInformationPanelCloses()
        {
            AccountDetailsPage accountDetailsPage = CloseInfoPanel(TestBase.evaluatorUsername);
            Assert.That(accountDetailsPage.inforPanel.GetAttribute("aria-expanded").Equals("false") && !accountDetailsPage.inforPanelContent.Displayed);
        }

        [Test]
        public void TestInformationPanelOpens()
        {
            AccountDetailsPage accountDetailsPage = OpenInfoPanel(TestBase.username);
            Assert.That(accountDetailsPage.inforPanel.GetAttribute("aria-expanded").Equals("true") && accountDetailsPage.inforPanelContent.Displayed);
        }

        [Test]
        public void TestEvaluatorInformationPanelOpens()
        {
            AccountDetailsPage accountDetailsPage = OpenInfoPanel(TestBase.evaluatorUsername);
            Assert.That(accountDetailsPage.inforPanel.GetAttribute("aria-expanded").Equals("true") && accountDetailsPage.inforPanelContent.Displayed);
        }
        private AccountDetailsPage OpenInfoPanel(string username)
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            if (TestBase.ElementIsPresent(accountDetailsPage.inforPanelContent))
            {
                accountDetailsPage.inforPanel.Click();
                accountDetailsPage.inforPanel.Click();
            }
            else
                accountDetailsPage.inforPanel.Click();
            Thread.Sleep(300);
            return accountDetailsPage;
        }

        private AccountDetailsPage CloseInfoPanel(string username)
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            if (TestBase.ElementIsPresent(accountDetailsPage.inforPanelContent))
                accountDetailsPage.inforPanel.Click();
            else
            {
                accountDetailsPage.inforPanel.Click();
                accountDetailsPage.inforPanel.Click();
            }
            Thread.Sleep(300);
            return accountDetailsPage;
        }
        private AccountDetailsPage TriggerCharLimitErrors(string username)
        {
            string testText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum";
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            accountDetailsPage.CompleteForm(testText, testText, testText, testText, testText, TestBase.userGender, TestBase.userGmcNumber.ToString(), TestBase.userGmcSpecialty, TestBase.userCareerGrade);
            return accountDetailsPage;
        }

        private AccountDetailsPage Submit(string username)
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            accountDetailsPage.CompleteForm(TestBase.userTitle, TestBase.userFirstName, TestBase.userLastName, TestBase.userAddress, TestBase.userPhone, TestBase.userGender, TestBase.userGmcNumber.ToString(), TestBase.userGmcSpecialty, TestBase.userCareerGrade);
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(accountDetailsPage.accountSubmittedMsg));
            return accountDetailsPage;
        }

        private AccountDetailsPage TriggerGMCNumberValidatio(string username)
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            accountDetailsPage.gmcNumberInput.SendKeys("123");
            accountDetailsPage.submitBtn.Click();
            return accountDetailsPage;
        }

        private AccountDetailsPage TriggerRequiredMsgs(string username)
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(username, TestBase.password);
            AccountDetailsPage accountDetailsPage = dashboardPage.getAccountDetails();
            accountDetailsPage.submitBtn.Click();
            Thread.Sleep(300);
            return accountDetailsPage;
        }
    }
}
