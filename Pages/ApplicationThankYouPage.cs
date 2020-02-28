using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class ApplicationThankYouPage
    {
        public ApplicationThankYouPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//mat-card")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        
        [FindsBy(How = How.XPath, Using = "//mat-card-content")]
        public IWebElement thankYouMsgCard { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//h5[contains(text(),'Thank you')]")]
        public IWebElement thankYouMsg { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//h5[contains(text(),'hear from us')]")]
        public IWebElement hearUsMsg { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//h5[contains(text(),'contact via email')]")]
        public IWebElement contactMsg { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();
        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(thankYouMsg);
            mainElements.Add(hearUsMsg);
            mainElements.Add(contactMsg);
            return mainElements;
        }

    }
}
