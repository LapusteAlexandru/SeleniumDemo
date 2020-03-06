using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Helpers.RadioButtonEnum;

namespace Pages
{
    class RevalidationPage
    {
        public RevalidationPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-revalidation//form")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//h4[text()='Revalidation']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-card-content//div[@class='panel']")]
        public IWebElement infoPanel { get; set; }

        [FindsBy(How = How.XPath, Using = "(//input[@type='file']/ancestor::button)[1]")]
        public IWebElement GMCLetterUploadInput { get; set; }

        [FindsBy(How = How.XPath, Using = "(//input[@type='file']/ancestor::button)[2]")]
        public IWebElement annualAppraisalSummaryUploadInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Yes')]/ancestor::mat-radio-button")]
        public IWebElement declareAppraisalYes { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'No')]/ancestor::mat-radio-button")]
        public IWebElement declareAppraisalNo { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement saveBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-error[contains(text(),'required') or contains(text(),'At least one file')]")]
        public IList<IWebElement> requiredMsgs { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Revalidation')]/..//i[contains(@class,'far')]")]
        public IWebElement statusIndicator { get; set; }
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Revalidation was successfully updated')]")]
        public IWebElement pageUpdatedMsg { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(infoPanel);
            mainElements.Add(title);
            mainElements.Add(annualAppraisalSummaryUploadInput);
            mainElements.Add(GMCLetterUploadInput);
            mainElements.Add(declareAppraisalYes);
            mainElements.Add(declareAppraisalNo);
            mainElements.Add(saveBtn);
            return mainElements;
        }

        public void CompleteForm(YesOrNoRadio radio, string filename1)
        {
            string fileExtension = "png";
            if (radio.Equals(YesOrNoRadio.Yes))
                declareAppraisalYes.Click();
            else
                declareAppraisalNo.Click();
            if (filename1.Length > 0)
            {
                TestBase.uploadField(filename1, fileExtension);
            }
            saveBtn.Click();
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(pageUpdatedMsg));
            }
            catch(NoSuchElementException e) { Console.WriteLine(e); }
        }
        public void CompleteForm(YesOrNoRadio radio)
        {
            CompleteForm(radio, "");
        }
    }
}
