using OpenQA.Selenium;
using RCoS;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pages
{
    class AdminUsersPage
    {
        public AdminUsersPage(IWebDriver driver)
        {
            try
            {
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-users//table")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }

        public string roleSelect = "//span[contains(text(),'{0}')]/ancestor::tr//mat-select";
        public string selectedRole = "//span[contains(text(),'{0}')]/ancestor::tr//mat-select//span[contains(@class,'mat-select-value-text')]//span";

        [FindsBy(How = How.XPath, Using = "//span[text()='Filter']/ancestor::div[@class='mat-form-field-infix']//input")]
        public IWebElement filterInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Full Name')]")]
        public IWebElement fullNameTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Email')]")]
        public IWebElement emailTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'GMC Number')]")]
        public IWebElement gmcNumberTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'GMC Specialty')]")]
        public IWebElement gmcSpecialtyTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Role')]")]
        public IWebElement roleTableHeader { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-option//span[contains(text(),'user')]")]
        public IWebElement userOption { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-option//span[contains(text(),'evaluator')]")]
        public IWebElement evaluatorOption { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Role was successfully changed')]")]
        public IWebElement successMsg { get; set; }

        [FindsBy(How = How.XPath, Using = "//button//span[contains(text(),'Change role')]")]
        public IWebElement changeRoleBtn { get; set; }


        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(filterInput);
            mainElements.Add(fullNameTableHeader);
            mainElements.Add(emailTableHeader);
            mainElements.Add(gmcNumberTableHeader);
            mainElements.Add(gmcSpecialtyTableHeader);
            mainElements.Add(roleTableHeader);
            return mainElements;
        }

        public string ChangeRole(string username)
        {
            TestBase.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//table")));
            filterInput.Clear();
            filterInput.SendKeys(username);
            string curretRole= TestBase.driver.FindElement(By.XPath(string.Format(selectedRole, username))).Text;
            IWebElement user = TestBase.driver.FindElement(By.XPath(string.Format(roleSelect, username)));
            user.Click();
            if (curretRole.Contains("user"))
                evaluatorOption.Click();
            else
                userOption.Click();

            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(changeRoleBtn));
            changeRoleBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'Role was successfully changed')]")));
            curretRole = TestBase.driver.FindElement(By.XPath(string.Format(selectedRole, username))).Text;
            return curretRole;
        }
    }
}
