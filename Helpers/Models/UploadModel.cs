using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    class UploadModel
    {
        public UploadModel(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//input[@type='file']")]
        public IWebElement uploadInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'File size cannot exceed 10 mb')]")]
        public IWebElement sizeLimitMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Unaccepted file format')]")]
        public IWebElement formatValidationMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'File name cannot exceed 100 characters')]")]
        public IWebElement longNameFileMsg { get; set; }



    }
}
