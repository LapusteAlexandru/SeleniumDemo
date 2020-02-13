using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Pages
{
    class RegistrationRequestsPage
    {
        public RegistrationRequestsPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.Id("mat-input-0")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        public string acceptBtn = "//td[contains(@class,'mat-column-email')]//span[contains(text(),'{0}')]/ancestor::tr/following-sibling::tr[1]//span[contains(text(),'Accept')]";
        public string rejectBtn = "//td[contains(@class,'mat-column-email')]//span[contains(text(),'{0}')]/ancestor::tr/following-sibling::tr[1]//span[contains(text(),'Reject')]";
        public string tableEmailCell = "//td[contains(@class,'mat-column-email')]//span[contains(text(),'{0}')]";
        public string registrationTD = "//span[contains(text(),'{0}')]/ancestor::tr/following-sibling::tr//i[@mattooltip='{1}']/following-sibling::span";
        public List<string> dataRows = new List<string>{ "Gender", "Created At", "Email", "Phone Number", "Address", "Birthday", "GMC Number", "GMC Specialty", "Career Grade" };
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

        [FindsBy(How = How.XPath, Using = "//textarea")]
        public IWebElement rejectTextArea { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Submit')]")]
        public IWebElement requestSubmitBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement confirmAcceptBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//button//span[contains(text(),'Cancel')]")]
        public IWebElement cancelBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Request was successfully rejected')]")]
        public IWebElement rejectedMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Request was successfully accepted')]")]
        public IWebElement acceptedMsg { get; set; }

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

        public void openRequestData(string username)
        {
            TestBase.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//table")));
            IWebElement user = TestBase.driver.FindElement(By.XPath(string.Format(tableEmailCell, username)));
            user.Click();

        }
        public void RejectRequest(string user,string reason)
        {
            openRequestData(user);
            Thread.Sleep(300);
            IWebElement reject = TestBase.driver.FindElement(By.XPath(string.Format(rejectBtn, user)));
            reject.Click();
            rejectTextArea.Clear();
            rejectTextArea.SendKeys(reason);
            requestSubmitBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(rejectedMsg));
        }
        public void AcceptRequest(string user)
        {
            openRequestData(user);
            Thread.Sleep(300);
            IWebElement accept = TestBase.driver.FindElement(By.XPath(string.Format(acceptBtn, user)));
            accept.Click();
            confirmAcceptBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(acceptedMsg));
        }
    }
}
