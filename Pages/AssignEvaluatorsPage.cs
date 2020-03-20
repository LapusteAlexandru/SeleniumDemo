using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class AssignEvaluatorsPage
    {
        public AssignEvaluatorsPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-assign-evaluators//table")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }

        public string evaluatorName = "//app-assign-evaluators//td[contains(@class,'mat-column-fullName')]//span[contains(text(),'{0}')]";
        public string evaluatorCheckbox = "//app-assign-evaluators//td[contains(@class,'mat-column-fullName')]//span[contains(text(),'{0}')]/ancestor::tr//mat-checkbox";
        public string assignedEvaluatorName = "//div[@class='selected-evaluators']//div[contains(text(),'{0}')]";
        public string assignedEvaluatorRemoveBtn = "//div[@class='selected-evaluators']//div[contains(text(),'{0}')]/following-sibling::i";

        [FindsBy(How = How.XPath, Using = "//h4[contains(text(),'Assign evaluators')]")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-assign-evaluators//input[@placeholder='Filter']")]
        public IWebElement filterInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-assign-evaluators//button[contains(text(),'Full Name')]")]
        public IWebElement fullNameTableHeader { get; set; }


        [FindsBy(How = How.XPath, Using = "//app-assign-evaluators//button[@aria-label='Next page']")]
        public IWebElement nextPageBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-assign-evaluators//button[@aria-label='Previous page']")]
        public IWebElement prevPageBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-dialog-actions//span[contains(text(),'Assign')]")]
        public IWebElement assignBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//button//span[contains(text(),'Cancel')]")]
        public IWebElement cancelBtn { get; set; }
        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(title);
            mainElements.Add(filterInput);
            mainElements.Add(fullNameTableHeader);
            mainElements.Add(assignBtn);
            mainElements.Add(cancelBtn);
            mainElements.Add(nextPageBtn);
            mainElements.Add(prevPageBtn);
            return mainElements;
        }
        public void AssignEvaluator(string evaluatorName)
        {
            filterInput.Clear();
            filterInput.SendKeys(evaluatorName);
            IWebElement assignCheckbox = TestBase.driver.FindElement(By.XPath(string.Format(evaluatorCheckbox, TestBase.userFirstName)));
            assignCheckbox.Click();

        }
    }
}
