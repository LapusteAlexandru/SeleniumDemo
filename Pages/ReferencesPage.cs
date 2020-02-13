using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pages
{
    class ReferencesPage
    {
        public ReferencesPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//h4[text()='References']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-card-content//div[@class='panel']")]
        public IWebElement infoPanel { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='file']")]
        public IWebElement uploadInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'References')]//i[contains(@class,'far')]")]
        public IWebElement statusIndicator { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(infoPanel);
            mainElements.Add(title);
            mainElements.Add(uploadInput);
            return mainElements;
        }

        public void CompleteForm(string filename)
        {

            uploadInput.SendKeys(new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\UploadFiles\\"+filename);
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mat-input-1")));
        }
    }
}
