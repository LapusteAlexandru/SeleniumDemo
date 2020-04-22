﻿using OpenQA.Selenium;
using RCoS;
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
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("home")));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.LinkText, Using = "Home")]
        public IWebElement homeBtn { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Introduction")]
        public IWebElement introToCosmeticsBtn { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Eligibility")]
        public IWebElement eligibilityBtn { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Standards")]
        public IWebElement standardsBtn { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Guidelines")]
        public IWebElement guidelinesBtn { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Evidence")]
        public IWebElement evidenceBtn { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Information for Applicants")]
        public IWebElement informationForApplicantsBtn { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Patient Information")]
        public IWebElement patientInformationBtn { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Register of Certified Cosmetic Surgeons")]
        public IWebElement registerOfSurgeonsBtn { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "GMC")]
        public IWebElement gmcBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "mat-button-wrapper")]
        public IWebElement loginRegisterBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "fa-youtube")]
        public IWebElement contactsBtn { get; set; }


        [FindsBy(How = How.XPath, Using = "//h4[text()='Cosmetic Surgery Certification']")]
        public IWebElement title { get; set; }


        public IList<IWebElement> mainElements = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(homeBtn);
            mainElements.Add(introToCosmeticsBtn);
            mainElements.Add(eligibilityBtn);
            mainElements.Add(informationForApplicantsBtn);
            mainElements.Add(patientInformationBtn);
            mainElements.Add(registerOfSurgeonsBtn);
            mainElements.Add(loginRegisterBtn);
            mainElements.Add(contactsBtn);
            return mainElements;
        }

        public LoginPage GetLogin()
        {
            loginRegisterBtn.Click();
            return new LoginPage(TestBase.driver);
        }
    }
}
