﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCoS_Automation.Helpers.Models
{
    class UploadModel
    {
        public UploadModel(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//input[@type='file']")]
        public IWebElement uploadInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'You have already uploaded this file')]")]
        public IWebElement sameFileMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Size cannot exceed 10 mb')]")]
        public IWebElement sizeLimitMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Unaccepted file format')]")]
        public IWebElement formatValidationMsg { get; set; }



    }
}