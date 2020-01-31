using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class RegistrationRequestsPage
    {
        public RegistrationRequestsPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementExists(By.XPath("//mat-sidenav")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.XPath, Using = "//h3[contains(text(),'Registration Requests')]")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.Id, Using = "mat-input-0")]
        public IWebElement filterInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//button//span[contains(text(),'Add column')]")]
        public IWebElement addColumnBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//button//span[contains(text(),'Remove column')]")]
        public IWebElement removeColumnBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Full Name')]")]
        public IWebElement fullNameTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Email')]")]
        public IWebElement emailTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'GMC Number')]")]
        public IWebElement gmcNumberTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'GMC Specialty')]")]
        public IWebElement gmcSpecialtyTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Career Grade')]")]
        public IWebElement careerGradeTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Telephone')]")]
        public IWebElement telephoneTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Created At')]")]
        public IWebElement createdAtTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Next page']")]
        public IWebElement nextPageBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Previous page']")]
        public IWebElement prevPageBtn { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(filterInput);
            mainElements.Add(addColumnBtn);
            mainElements.Add(removeColumnBtn);
            mainElements.Add(fullNameTableHeader);
            mainElements.Add(emailTableHeader);
            mainElements.Add(gmcNumberTableHeader);
            mainElements.Add(gmcSpecialtyTableHeader);
            return mainElements;
        }
    }
}
