using NUnit.Framework;
using Pages;
using RCoS;
using System.Threading;

namespace SidebarTests
{
    class SidebarTests
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

        [Test]
        public void TestClickAccountDetails()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            AccountDetailsPage adp = dp.getAccountDetails();
            Assert.That(adp.title.Displayed);
        }
        [Test]
        public void TestClickProbityStatements()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ProbityStatementsPage psp = dp.getProbityStatements();
            Assert.That(psp.title.Displayed);
        }
        [Test]
        public void TestHideSidebar()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            dp.sidebarMenuBtn.Click();
            Thread.Sleep(500);
            Assert.That(!dp.sidebar.Displayed);
        }
        [Test]
        public void TestShowSidebar()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            dp.sidebarMenuBtn.Click();
            Thread.Sleep(500);
            dp.sidebarMenuBtn.Click();
            Thread.Sleep(500);
            Assert.That(dp.sidebar.Displayed);
        }
    }
}
