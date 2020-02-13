using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static Helpers.RadioButtonEnum;

namespace ProbityStatementsTests
{
    [TestFixture]
    [Category("ProbityStatements")]
    class ProbityStatementsTests
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
            ProbityStatementsPage psp = dp.getProbityStatements();
            foreach (var e in psp.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(2)]
        public void TestRequiredMsgs()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ProbityStatementsPage psp = dp.getProbityStatements();
            psp.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in psp.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(psp.requiredMsgs.Count.Equals(3));
        }
        [Test, Order(3)]
        public void TestSubmitSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ProbityStatementsPage psp = dp.getProbityStatements();
            ProbityRadio radioOption = ProbityRadio.Nothing;
            psp.CompleteForm(radioOption);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(psp.title));
            Assert.That(psp.professionalObligationsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(psp.suspensionCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(psp.nothingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked")); 
            Assert.That(psp.statusIndicator.GetAttribute("class").Contains("completed"));
        }
        [Test]
        public void TestEditSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ProbityStatementsPage psp = dp.getProbityStatements();
            ProbityRadio radioOption = ProbityRadio.Nothing;
            if (psp.nothingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"))
                radioOption = ProbityRadio.Something;
            psp.CompleteForm(radioOption);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(psp.title));
            Assert.That(psp.professionalObligationsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(psp.suspensionCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            if (radioOption.Equals(ProbityRadio.Nothing))
                Assert.That(psp.nothingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"));
            else
                Assert.That(psp.somethingToDeclareRadio.GetAttribute("class").Contains("mat-radio-checked"));
        }
    }
}
