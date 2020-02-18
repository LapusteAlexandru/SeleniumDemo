using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static Helpers.RadioButtonEnum;

namespace RevalidationTests
{
    [TestFixture]
    [Category("Revalidation")]
    class RevalidationTests
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
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            RevalidationPage rp = dp.getRevalidation();
            foreach (var e in rp.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(1)]
        public void TestRequiredMsgs()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            RevalidationPage rp = dp.getRevalidation();
            rp.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in rp.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(rp.requiredMsgs.Count.Equals(3));
        }
        [Test, Order(2)]
        public void TestSubmitSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            RevalidationPage rp = dp.getRevalidation();
            rp.CompleteForm(YesOrNoRadio.Yes,"png",false);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(rp.title));
            dp.openSideMenuIfClosed();
            Assert.That(rp.declareAppraisalYes.GetAttribute("class").Contains("mat-radio-checked"));
            Assert.That(rp.statusIndicator.GetAttribute("class").Contains("completed"));
        }
        [Test]
        public void TestEditSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            RevalidationPage rp = dp.getRevalidation();
            YesOrNoRadio radioOption = YesOrNoRadio.No;
            rp.CompleteForm(radioOption);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(rp.title));
            dp.openSideMenuIfClosed();
            Assert.That(rp.declareAppraisalNo.GetAttribute("class").Contains("mat-radio-checked"));
            Assert.That(rp.statusIndicator.GetAttribute("class").Contains("completed"));
        }
    }
}
