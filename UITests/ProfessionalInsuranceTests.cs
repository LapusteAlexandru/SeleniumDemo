using NUnit.Framework;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static Helpers.RadioButtonEnum;

namespace ProfessionalInsuranceTests
{
    [TestFixture]
    [Category("ProfessionalInsurance")]
    class ProfessionalInsuranceTests
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
            ProfessionalInsurancePage pip = dp.getProfessionalInsurance();
            foreach (var e in pip.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(2)]
        public void TestRequiredMsgs()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ProfessionalInsurancePage pip = dp.getProfessionalInsurance();
            pip.saveBtn.Click();
            Thread.Sleep(300);
            foreach (var e in pip.requiredMsgs)
                Assert.That(e.Displayed);
            Assert.That(pip.requiredMsgs.Count.Equals(4));
        }
        [Test, Order(3)]
        public void TestSubmitSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ProfessionalInsurancePage pip = dp.getProfessionalInsurance();
            YesOrNoRadio radioOption = YesOrNoRadio.Yes;
            pip.CompleteForm(radioOption,"png",false);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(pip.title));
            dp.openSideMenuIfClosed();
            Assert.That(pip.indemnityArrangementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(pip.disclosedNatureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(pip.yesPracticeRadio.GetAttribute("class").Contains("mat-radio-checked"));
            Assert.That(pip.statusIndicator.GetAttribute("class").Contains("completed"));
        }
        
        [Test]
        public void TestEditSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ProfessionalInsurancePage pip = dp.getProfessionalInsurance();
            YesOrNoRadio radioOption = YesOrNoRadio.No;
            pip.CompleteForm(radioOption);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(pip.title));
            dp.openSideMenuIfClosed();
            Assert.That(pip.indemnityArrangementsCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(pip.disclosedNatureCheckbox.GetAttribute("class").Contains("mat-checkbox-checked"));
            Assert.That(pip.noPracticeRadio.GetAttribute("class").Contains("mat-radio-checked"));
            Assert.That(pip.statusIndicator.GetAttribute("class").Contains("completed"));
        }
    }
}
