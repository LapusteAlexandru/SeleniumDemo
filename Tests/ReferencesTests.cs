using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using RCoS_Automation.Helpers.Models;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReferencesTests
{
    [TestFixture]
    [Category("References")]
    class ReferencesTests
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
            ReferencesPage rp = dp.getReferences();
            foreach (var e in rp.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(2)]
        public void TestUploadSizeLimit()
        {
            UploadModel uploadModel = Upload("large");
            Assert.That(uploadModel.sizeLimitMsg.Displayed);
        }
        [Test, Order(2)]
        public void TestUploadWrongFormat()
        {
            UploadModel uploadModel = Upload("wrongFormat");
            Assert.That(uploadModel.formatValidationMsg.Displayed);
        }
        [Test, Order(2)]
        public void TestUploadSameFIle()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            dp.getReferences();
            UploadModel uploadModel = new UploadModel(TestBase.driver);
            TestBase.uploadField("png","png");
            TestBase.uploadField("png","png");
            Assert.That(uploadModel.sameFileMsg.Displayed);
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ReferencesPage rp = dp.getReferences();
            TestBase.uploadField("png", "png");
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mat-input-1")));
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(rp.title));
            Assert.That(rp.statusIndicator.GetAttribute("class").Contains("completed"));
        }

        private UploadModel Upload(string fileName)
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            dp.getReferences();
            switch (fileName)
            {
                case "large":
                    TestBase.uploadField("large","doc");
                    break;
            
                case "wrongFormat":
                    TestBase.uploadField("wrongFormat","jfif");
                    break;
                
                default:
                    break;
            }
            return new UploadModel(TestBase.driver);
        }
    }
}
