using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
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
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            OperativeExposurePage ope = dp.getOperativeExposure();
            foreach (var e in ope.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(1)]
        public void TestRequiredMsgs()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            OperativeExposurePage oep = dp.getOperativeExposure();
            oep.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in oep.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(oep.requiredMsgs.Count.Equals(3));
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            OperativeExposurePage oep = dp.getOperativeExposure();
            oep.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(oep.title));
            Assert.That(oep.proceduresCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(oep.operativeExposureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(oep.statusIndicator.GetAttribute("class").Contains("completed"));
        }
    }
}
