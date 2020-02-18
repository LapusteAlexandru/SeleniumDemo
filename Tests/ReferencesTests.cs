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
            UploadModel uploadModel = Upload("large","doc");
            Assert.That(uploadModel.sizeLimitMsg.Displayed);
        }
        [Test, Order(2)]
        public void TestUploadWrongFormat()
        {
            UploadModel uploadModel = Upload("wrongFormat", "jfif");
            Assert.That(uploadModel.formatValidationMsg.Displayed);
        }
        [Test]
        public void TestSubmitSuccessfully()
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            ReferencesPage rp = dp.getReferences();
            TestBase.uploadField("png", "png");
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(rp.title));
            Assert.That(rp.statusIndicator.GetAttribute("class").Contains("completed"));
        }

        private UploadModel Upload(string fileName,string fileExtension)
        {
            HomePage hp = new HomePage(TestBase.driver);
            LoginPage lp = hp.GetLogin();
            DashboardPage dp = lp.DoLogin(TestBase.username, TestBase.password);
            dp.getReferences();
            IWebElement uploadInput = TestBase.driver.FindElement(By.XPath("//input[@type='file']"));
            uploadInput.SendKeys(TestContext.Parameters["uploadFilesPath"] + fileName + "." + fileExtension);
            switch (fileName)
            {
                case "large":
                    uploadInput.SendKeys(TestContext.Parameters["uploadFilesPath"] + fileName + "." + fileExtension);
                    break;
            
                case "wrongFormat":
                    uploadInput.SendKeys(TestContext.Parameters["uploadFilesPath"] + fileName + "." + fileExtension);
                    break;
                
                default:
                    break;
            }
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//snack-bar-container[@role='status']")));
            return new UploadModel(TestBase.driver);
        }
    }
}
