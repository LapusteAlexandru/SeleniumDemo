using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ClinicalOutcomesTests
{
    [TestFixture]
    [Category("ClinicalOutcomes")]
    class ClinicalOutcomesTests
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
            ClinicalOutcomesPage cop = dp.getClinicalOutcomes();
            foreach (var e in cop.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(2)]
        public void TestRequiredMsgs()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ClinicalOutcomesPage cop = dp.getClinicalOutcomes();
            cop.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in cop.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(cop.requiredMsgs.Count.Equals(2));
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ClinicalOutcomesPage cop = dp.getClinicalOutcomes();
            cop.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(cop.title));
            Assert.That(cop.CMARequirementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(cop.statusIndicator.GetAttribute("class").Contains("completed"));
        }
    }
}
