using OpenQA.Selenium;
using Helpers;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class HomePage
    {
        
        public HomePage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//title[text()='My Store']")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.LinkText, Using = "Women")]
        public IWebElement womenTab { get; set; }

        [FindsBy(How = How.XPath, Using = "(//a[@title='Dresses'])[2]")]
        public IWebElement dressesTab { get; set; }

        [FindsBy(How = How.XPath, Using = "(//a[@title='T-shirts'])[2]")]
        public IWebElement tshirtsTab { get; set; }

       

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(womenTab);
            mainElements.Add(dressesTab);
            mainElements.Add(tshirtsTab);
            return mainElements;
        }

        public WomenPage GetWomenPage(IWebDriver driver)
        {
            womenTab.Click();
            return new WomenPage(driver);
        }
    }
}
