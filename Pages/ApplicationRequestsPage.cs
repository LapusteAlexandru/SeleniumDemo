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
    class ApplicationRequestsPage
    {
        public ApplicationRequestsPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-application-requests//table")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        public string approveBtn = "//span[contains(text(),'{0}')]/ancestor::tr//i[contains(@class,'fa-check-circle')]/ancestor::button";
        public string feedBackBtn = "//span[contains(text(),'{0}')]/ancestor::tr//i[contains(@class,'fa-comments')]/ancestor::button";
        public string feedbackExpansionPanel = "//div[contains(text(),'{0}')]/..//mat-expansion-panel";
        public string feedbackComment = "//div[contains(text(),'{0}')]/..//div[contains(@class,'comments')]";
        public string feedbackStatus = "//app-evaluators-comments-dialog//div[contains(text(),'{0}')]";
        public string editBtn = "//span[contains(text(),'{0}')]/ancestor::tr//i[contains(@class,'fa-edit')]/ancestor::button";
        public string assignEvaluatorBtn = "//span[contains(text(),'{0}')]/ancestor::tr//i[contains(@class,'fa-tasks')]/ancestor::button";
        public string fullNameCell = "//span[contains(text(),'{0}')]/ancestor::tr//td[contains(@class,'cdk-column-fullName')]";
        public string emailCell = "//span[contains(text(),'{0}')]/ancestor::tr//td[contains(@class,'cdk-column-email')]";
        public string GMCNumberCell = "//span[contains(text(),'{0}')]/ancestor::tr//td[contains(@class,'cdk-column-gmcNumber')]";
        public string GMCSpecialtyCell = "//span[contains(text(),'{0}')]/ancestor::tr//td[contains(@class,'cdk-column-gmcSpecialty')]";
        public string statusCell = "//span[contains(text(),'{0}')]/ancestor::tr//td[contains(@class,'cdk-column-status')]";
        public string paymentStatusCell = "//span[contains(text(),'{0}')]/ancestor::tr//td[contains(@class,'cdk-column-paymentStatus')]";
        public string careerGradeCell = "//span[contains(text(),'{0}')]/ancestor::tr//td[contains(@class,'cdk-column-careerGrade')]";
        public string telephoneCell = "//span[contains(text(),'{0}')]/ancestor::tr//td[contains(@class,'cdk-column-phoneNumber')]";
        public string registrationTD = "//span[contains(text(),'{0}')]/ancestor::tr//td[contains(@class,'{1}')]";
        public string tableCell = "//td[contains(@class,'mat-cell')]//span[contains(text(),'{0}')]";
        [FindsBy(How = How.XPath, Using = "//h4[contains(text(),'Application Requests')]")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[text()='Filter']/ancestor::div[@class='mat-form-field-infix']//input")]
        public IWebElement filterInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//app-application-requests//table")]
        public IWebElement requestsTable { get; set; }

        [FindsBy(How = How.XPath, Using = "//p[contains(text(),'No requests found')]")]
        public IWebElement noRequestsMsg { get; set; }

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

        [FindsBy(How = How.XPath, Using = "//th[contains(@class,'mat-column-approve')]")]
        public IWebElement approveTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//th[contains(@class,'mat-column-edit')]")]
        public IWebElement editTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//th[contains(@class,'mat-column-assignEvaluators')]")]
        public IWebElement assignEvaluatorTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Payment Status')]")]
        public IWebElement paymentStatusTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Status')]")]
        public IWebElement statusTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Next page']")]
        public IWebElement nextPageBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Previous page']")]
        public IWebElement prevPageBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//textarea")]
        public IWebElement approveTextArea { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-dialog-actions//span[contains(text(),'Approve')]")]
        public IWebElement requestApproveBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-dialog-actions//span[contains(text(),'Reject')]")]
        public IWebElement requestRejectBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//button//span[contains(text(),'Cancel')]")]
        public IWebElement cancelBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Application was successfully')]")]
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
            mainElements.Add(statusTableHeader);
            mainElements.Add(paymentStatusTableHeader);
            mainElements.Add(editTableHeader);
            mainElements.Add(approveTableHeader);
            mainElements.Add(assignEvaluatorTableHeader);
            mainElements.Add(nextPageBtn);
            mainElements.Add(prevPageBtn);
            return mainElements;
        }

        public EditApplicationPage EditUser(string username)
        {
            TestBase.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//table")));
            filterInput.Clear();
            filterInput.SendKeys(username);
            IWebElement user = TestBase.driver.FindElement(By.XPath(string.Format(editBtn, username)));
            user.Click();
            return new EditApplicationPage(TestBase.driver);
        }
        
        public void AcceptRequest(string user)
        {

            filterInput.Clear();
            filterInput.SendKeys(user);
            IWebElement accept = TestBase.driver.FindElement(By.XPath(string.Format(approveBtn, user)));
            accept.Click(); 
            Thread.Sleep(300);
            approveTextArea.SendKeys("Test accept " + TestBase.currentDay);
            requestApproveBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(acceptedMsg));
        }
        public void RejectRequest(string user)
        {
            filterInput.Clear();
            filterInput.SendKeys(user);
            IWebElement accept = TestBase.driver.FindElement(By.XPath(string.Format(approveBtn, user)));
            accept.Click(); 
            Thread.Sleep(300);
            approveTextArea.SendKeys("Test reject " + TestBase.currentDay);
            requestRejectBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(acceptedMsg));
        }
        public string ReadComment(string applicationUser,string evaluatorUser)
        {
            filterInput.Clear();
            filterInput.SendKeys(applicationUser);
            IWebElement feedback = TestBase.driver.FindElement(By.XPath(string.Format(feedBackBtn, applicationUser)));
            feedback.Click();
            Thread.Sleep(300);

            IWebElement feedbackPanel = TestBase.driver.FindElement(By.XPath(string.Format(feedbackExpansionPanel, evaluatorUser)));
            feedbackPanel.Click();

            string feedbackText = TestBase.driver.FindElement(By.XPath(string.Format(feedbackComment, evaluatorUser))).Text;
            return feedbackText;
        }

        public AssignEvaluatorsPage GetAssignEvaluatorsPanel(string username)
        {
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Filter']/ancestor::div[@class='mat-form-field-infix']//input")));
            filterInput.Clear();
            filterInput.SendKeys(username);
            IWebElement assign = TestBase.driver.FindElement(By.XPath(string.Format(assignEvaluatorBtn, username)));
            assign.Click();
            return new AssignEvaluatorsPage(TestBase.driver);
        }
    }
}
