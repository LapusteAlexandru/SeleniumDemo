using Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUploadTests
{
    class FileUploadTests
    {
        
        [Test]
        public void TestUploadSizeLimit()
        {
            UploadModel uploadModel = Upload("large", "doc");
            Assert.That(uploadModel.sizeLimitMsg.Displayed);
        }
        [Test]
        public void TestUploadWrongFormat()
        {
            UploadModel uploadModel = Upload("wrongFormat", "jfif");
            Assert.That(uploadModel.formatValidationMsg.Displayed);
        }

        private UploadModel Upload(string fileName, string fileExtension)
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
