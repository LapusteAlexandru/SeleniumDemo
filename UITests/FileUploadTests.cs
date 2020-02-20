using Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;

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
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.username, TestBase.password);
            dashboardPage.getReferences();
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
