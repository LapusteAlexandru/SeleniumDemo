using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class SignUpTyPage
    {
        public SignUpTyPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
    }
}
