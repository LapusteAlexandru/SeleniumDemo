using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ProfessionalBehavioursTests
{
    [TestFixture]
    [Category("ProfessionalBehaviours")]
    class ProfessionalBehavioursTests
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
            ProfessionalBehavioursPage pbp = dp.getProfessionalBehaviours();
            foreach (var e in pbp.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(2)]
        public void TestRequiredMsgs()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ProfessionalBehavioursPage pbp = dp.getProfessionalBehaviours();
            pbp.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in pbp.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(pbp.requiredMsgs.Count.Equals(2));
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ProfessionalBehavioursPage pbp = dp.getProfessionalBehaviours();
            pbp.CompleteForm("png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(pbp.title));
            Assert.That(pbp.professionalResponsibilitiesCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(pbp.statusIndicator.GetAttribute("class").Contains("completed"));
        }
    }
}
