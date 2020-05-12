using NUnit.Framework;
using Pages;
using RCoS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChangeRoleTests
{
    class ChangeRoleTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[Applications]", TestBase.appUsername, "Status", 4);
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
            AdminUsersPage usersPage = getUsers();
            foreach (var e in usersPage.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test, Order(2)]
        [Repeat(2)]
        public void TestChangeRole()
        {
            AdminUsersPage usersPage = getUsers();
            string role = usersPage.ChangeRole(TestBase.appUsername);
            DashboardPage dashboardPage = new DashboardPage(TestBase.driver);
            dashboardPage.logout();
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboard = loginPage.DoLogin(TestBase.appUsername, TestBase.password);

            dashboard.openSideMenuIfClosed();
            if (role.Contains("user"))
                Assert.IsFalse(TestBase.ElementIsPresent(dashboard.applicationRequestsBtn));
            else
                Assert.IsTrue(TestBase.ElementIsPresent(dashboard.applicationRequestsBtn));
        }

        private AdminUsersPage getUsers()
        {

            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.adminUsername, TestBase.adminPassword);
            AdminUsersPage usersPage = dashboardPage.getUsers();
            return usersPage;
        }
    }
}
