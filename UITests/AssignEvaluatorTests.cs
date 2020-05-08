using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;

namespace AssignEvaluatorTests
{
    [TestFixture]
    [Category("AssignEvaluator")]
    class AssignEvaluatorTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[Applications]", TestBase.appUsername, "Status", 2);
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


            AssignEvaluatorsPage assignEvaluatorsPage = getAssignEvaluatorPopup(TestBase.appUsername);
            foreach (var e in assignEvaluatorsPage.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test, Order(1)]
        public void TestSelectEvaluator()
        {

            AssignEvaluatorsPage assignEvaluatorsPage = getAssignEvaluatorPopup(TestBase.appUsername);
            assignEvaluatorsPage.AssignEvaluator();
            Assert.That(TestBase.driver.FindElement(By.XPath(string.Format(assignEvaluatorsPage.assignedEvaluatorName, TestBase.userFirstName))).Displayed);
        }
        [Test, Order(1)]
        public void TestRemoveSelectedEvaluator()
        {

            AssignEvaluatorsPage assignEvaluatorsPage = getAssignEvaluatorPopup(TestBase.appUsername);
            assignEvaluatorsPage.AssignEvaluator();
            IWebElement selectedEvaluator = TestBase.driver.FindElement(By.XPath(string.Format(assignEvaluatorsPage.assignedEvaluatorName, TestBase.userFirstName)));
            Assert.That(selectedEvaluator.Displayed);
            IWebElement removeSselectedEvaluatorBtn = TestBase.driver.FindElement(By.XPath(string.Format(assignEvaluatorsPage.assignedEvaluatorRemoveBtn, TestBase.userFirstName)));
            removeSselectedEvaluatorBtn.Click();
            Assert.That(TestBase.driver.FindElements(By.XPath(string.Format(assignEvaluatorsPage.assignedEvaluatorName, TestBase.userFirstName))).Count.Equals(0));

        }
        [Test, Order(1)]
        public void TestCancelAssignEvaluator()
        {

            AssignEvaluatorsPage assignEvaluatorsPage = getAssignEvaluatorPopup(TestBase.appUsername);
            assignEvaluatorsPage.AssignEvaluator();
            IWebElement selectedEvaluator = TestBase.driver.FindElement(By.XPath(string.Format(assignEvaluatorsPage.assignedEvaluatorName, TestBase.userFirstName)));
            Assert.That(selectedEvaluator.Displayed);
            assignEvaluatorsPage.cancelBtn.Click();
            TestBase.driver.Navigate().Refresh();
            assignEvaluatorsPage = getAssignEvaluatorPopup(TestBase.appUsername);
            Assert.That(TestBase.driver.FindElements(By.XPath(string.Format(assignEvaluatorsPage.assignedEvaluatorName, TestBase.userFirstName))).Count.Equals(0));

        }
        [Test, Order(2)]
        public void TestAssignEvaluator()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            AssignEvaluatorsPage assignEvaluatorsPage = applicationRequestsPage.GetAssignEvaluatorsPanel(TestBase.appUsername);
            assignEvaluatorsPage.AssignEvaluator();
            assignEvaluatorsPage.assignBtn.Click();
            TestBase.driver.Navigate().Refresh();
            applicationRequestsPage.GetAssignEvaluatorsPanel(TestBase.appUsername);
            IWebElement selectedEvaluator = TestBase.driver.FindElement(By.XPath(string.Format(assignEvaluatorsPage.assignedEvaluatorName, TestBase.userFirstName)));
            Assert.That(selectedEvaluator.Displayed);
            assignEvaluatorsPage.cancelBtn.Click();
            dashboardPage.logout();
            homePage.GetLogin();
            loginPage.DoLogin(TestBase.evaluatorUsername, TestBase.password);
            dashboardPage.getApplicationRequests();
            applicationRequestsPage.filterInput.Clear();
            applicationRequestsPage.filterInput.SendKeys(TestBase.appUsername);
            string rowValue;
            rowValue = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.emailCell, TestBase.appUsername))).Text;
            Assert.That(rowValue.Equals(TestBase.applicantData[0]));
            rowValue = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.GMCNumberCell, TestBase.appUsername))).Text;
            Assert.That(rowValue.Equals(TestBase.applicantData[1]));
            rowValue = TestBase.driver.FindElement(By.XPath(string.Format(applicationRequestsPage.GMCSpecialtyCell, TestBase.appUsername))).Text;
            Assert.That(rowValue.Equals(TestBase.applicantData[2]));
        }
        [Test, Order(3)]
        public void TestRemoveAssignedEvaluator()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            AssignEvaluatorsPage assignEvaluatorsPage = applicationRequestsPage.GetAssignEvaluatorsPanel(TestBase.appUsername);
            IWebElement removeSelectedEvaluatorBtn = TestBase.driver.FindElement(By.XPath(string.Format(assignEvaluatorsPage.assignedEvaluatorRemoveBtn, TestBase.userFirstName)));
            removeSelectedEvaluatorBtn.Click();
            assignEvaluatorsPage.assignBtn.Click();
            TestBase.driver.Navigate().Refresh();
            applicationRequestsPage.GetAssignEvaluatorsPanel(TestBase.appUsername);
            Assert.That(TestBase.driver.FindElements(By.XPath(string.Format(assignEvaluatorsPage.assignedEvaluatorName, TestBase.userFirstName))).Count.Equals(0));
            assignEvaluatorsPage.cancelBtn.Click();
            dashboardPage.logout();
            homePage.GetLogin();
            loginPage.DoLogin(TestBase.evaluatorUsername, TestBase.password);
            dashboardPage.getApplicationRequests();
            Assert.That(applicationRequestsPage.noRequestsMsg.Displayed);
        }

        private AssignEvaluatorsPage getAssignEvaluatorPopup(string username)
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            ApplicationRequestsPage applicationRequestsPage = dashboardPage.getApplicationRequests();
            AssignEvaluatorsPage assignEvaluatorsPage = applicationRequestsPage.GetAssignEvaluatorsPanel(username);
            return assignEvaluatorsPage;
        }
    }
}
