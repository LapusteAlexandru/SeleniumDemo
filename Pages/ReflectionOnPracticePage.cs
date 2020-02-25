using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support;
using System.Threading;

namespace Pages
{
    class ReflectionOnPracticePage
    {
        public ReflectionOnPracticePage(IWebDriver driver)
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
        private static string hospitalSite = "Hospital Site";
        private static string eventLocation = "Event Location";
        private static string role = "Role";
        private static string procedure = "procedure";
        private static string eventDescription = "Event Description";
        private static string colleagueName = "Colleague Name";
        private static string colleagueEmail = "Colleague Email";
        private static string eventOutcome = "Event Outcome";
        private static string learnt = "Learnt";
        private static string result = "Result";
        private static string futureLearning = "Future Learning";
        public List<string> userData = new List<string> { hospitalSite, eventLocation, role, procedure, eventDescription, colleagueName, colleagueEmail,eventOutcome,learnt,result,futureLearning };
        public string inputText = "TestInput";
        public string textareaText = "TestTextarea";
        [FindsBy(How = How.XPath, Using = "//h4[text()='Reflection on Practice']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-card-content//div[@class='panel']")]
        public IWebElement infoPanel { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='file']/ancestor::button")]
        public IWebElement uploadInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Reflection on Practice')]//i[contains(@class,'far')]")]
        public IWebElement statusIndicator { get; set; }

        [FindsBy(How = How.Id, Using = "mat-tab-label-0-0")]
        public IWebElement case1TabBtn { get; set; }

        [FindsBy(How = How.Id, Using = "mat-tab-label-0-1")]
        public IWebElement case2TabBtn { get; set; }

        [FindsBy(How = How.Id, Using = "mat-tab-label-0-2")]
        public IWebElement case3TabBtn { get; set; }

        [FindsBy(How = How.Id, Using = "mat-tab-label-0-3")]
        public IWebElement case4TabBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='caseDate']")]
        public IWebElement dateInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='hospitalSite']")]
        public IWebElement hospitalSiteInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='locationOfEvent']")]
        public IWebElement locationOfEventInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='role']")]
        public IWebElement roleInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='procedure']")]
        public IWebElement procedureInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='descriptionOfEvent']")]
        public IWebElement descriptionOfEventInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='nameOfColleague']")]
        public IWebElement nameOfColleagueInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='colleaguesEmail']")]
        public IWebElement colleaguesEmailInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//textarea[@formcontrolname='outcomeOfEvent']")]
        public IWebElement outcomeOfEventTextarea { get; set; }

        [FindsBy(How = How.XPath, Using = "//textarea[@formcontrolname='whatLearnt']")]
        public IWebElement whatLearntTextarea { get; set; }

        [FindsBy(How = How.XPath, Using = "//textarea[@formcontrolname='practiceChange']")]
        public IWebElement practiceChangeTextarea { get; set; }

        [FindsBy(How = How.XPath, Using = "//textarea[@formcontrolname='learningNeeds']")]
        public IWebElement learningNeedsTextarea { get; set; }
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Reflection on practice was successfully created')]")]
        public IWebElement pageSubmitedMsg { get; set; }
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Reflection on practice was successfully updated')]")]
        public IWebElement pageUpdatedMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'required') or contains(text(),'At least one file')]")]
        public IList<IWebElement> requiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'The length must not exceed')]")]
        public IList<IWebElement> limitReachedMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-hint")]
        public IList<IWebElement> textareaLimitCounter { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement submitBtn { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();
        public IList<IWebElement> inputElements = new List<IWebElement>();
        public IList<IWebElement> textareaElements = new List<IWebElement>();
        public IList<IWebElement> textElements = new List<IWebElement>();
        public IList<IWebElement> tabs = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(infoPanel);
            mainElements.Add(title);
            mainElements.Add(dateInput);
            mainElements.Add(hospitalSiteInput);
            mainElements.Add(locationOfEventInput);
            mainElements.Add(roleInput);
            mainElements.Add(procedureInput);
            mainElements.Add(descriptionOfEventInput);
            mainElements.Add(nameOfColleagueInput);
            mainElements.Add(colleaguesEmailInput);
            mainElements.Add(outcomeOfEventTextarea);
            mainElements.Add(whatLearntTextarea);
            mainElements.Add(practiceChangeTextarea);
            mainElements.Add(learningNeedsTextarea);
            mainElements.Add(uploadInput);
            mainElements.Add(case1TabBtn);
            mainElements.Add(case2TabBtn);
            mainElements.Add(case3TabBtn);
            mainElements.Add(case4TabBtn);
            mainElements.Add(submitBtn);
            return mainElements;
        }
        public IList<IWebElement> GetInputElements()
        {
            inputElements.Add(hospitalSiteInput);
            inputElements.Add(locationOfEventInput);
            inputElements.Add(roleInput);
            inputElements.Add(procedureInput);
            inputElements.Add(descriptionOfEventInput);
            inputElements.Add(nameOfColleagueInput);
            inputElements.Add(colleaguesEmailInput);
           
            return inputElements;
        }
        public IList<IWebElement> GetTextareaElements()
        {

            textareaElements.Add(outcomeOfEventTextarea);
            textareaElements.Add(whatLearntTextarea);
            textareaElements.Add(practiceChangeTextarea);
            textareaElements.Add(learningNeedsTextarea);

            return textareaElements;
        }
        public IList<IWebElement> GetTextElements()
        {

            textElements.Add(hospitalSiteInput);
            textElements.Add(locationOfEventInput);
            textElements.Add(roleInput);
            textElements.Add(procedureInput);
            textElements.Add(descriptionOfEventInput);
            textElements.Add(nameOfColleagueInput);
            textElements.Add(colleaguesEmailInput);
            textElements.Add(outcomeOfEventTextarea);
            textElements.Add(whatLearntTextarea);
            textElements.Add(practiceChangeTextarea);
            textElements.Add(learningNeedsTextarea);

            return textElements;
        }
        
        public IList<IWebElement> GetFormTabs()
        {
            tabs.Add(case1TabBtn);
            tabs.Add(case2TabBtn);
            tabs.Add(case3TabBtn);
            tabs.Add(case4TabBtn);
           
            return tabs;
        }


        public void CompleteForm(int formNo, string filename, bool update)
        {
            IWebElement tab = case1TabBtn;
            IList<IWebElement> input = GetInputElements();
            IList<IWebElement> textarea = GetTextareaElements();
            switch (formNo)
            {
                case 1:
                    break;
                case 2:

                    tab = case2TabBtn;
                    break;
                case 3:

                    tab = case3TabBtn;
                    break;
                case 4:

                    tab = case4TabBtn;
                    break;

                default: break;
            }
            if (update)
            {
                inputText += "Updated";
                textareaText += "Updated";
                TestBase.wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//span[contains(text(),'successfully updated')]")));
            } 
            tab.Click();
            var selected = tab.GetAttribute("aria-selected");
            TestBase.wait.Equals(selected.Equals(true));
            Thread.Sleep(1000);
            IWebElement datePicker = TestBase.driver.FindElement(By.XPath("//mat-datepicker-toggle"));
            datePicker.Click();
            IWebElement firstDayOfMonth = TestBase.driver.FindElement(By.XPath("//div[text()='1']"));
            firstDayOfMonth.Click();
            for (int i = 0; i < input.Count; i++)
            {
                input[i].Clear();
                input[i].SendKeys(inputText); }
            for (int i = 0; i < textarea.Count; i++) 
            {
                textarea[i].Clear();
                IJavaScriptExecutor js = (IJavaScriptExecutor)TestBase.driver;
                string myExecution = "document.getElementsByTagName('textarea')["+ i +"].value='"+ textareaText +"'";
                js.ExecuteScript(myExecution);
                textarea[i].SendKeys(inputText);
            }
            
            string fileExtension = "png";
            if (filename.Length > 0)
            {
                TestBase.uploadField(filename, fileExtension);
            }
            submitBtn.Click();
            try
            {

                if (update)
                    TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(pageUpdatedMsg));
                else
                    TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(pageSubmitedMsg));
            }
            catch { }
        }
    }
}
