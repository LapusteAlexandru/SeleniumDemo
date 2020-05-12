using OpenQA.Selenium;
using RCoS;
using Pages;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Pages
{
    class DashboardPage
    {
        public DashboardPage(IWebDriver driver)
        {
            try
            {
                IWebElement homeContainer = TestBase.driver.FindElement(By.ClassName("home"));
                if(TestBase.ElementIsPresent(homeContainer))
                    TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("home")));
                else
                    TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-account-details//form")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.XPath, Using = "//title[text()='RCS.Cosmetics.Web']")]
        public IWebElement title { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Current Application')]")]
        public IWebElement currentAppBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Payment Check')]")]
        public IWebElement paymentCheckBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'New Application')]")]
        public IWebElement newAppBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Applications History')]")]
        public IWebElement appHistoryBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Account Details')]")]
        public IWebElement accountDetailsBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Probity Statements')]")]
        public IWebElement probityStatementsBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Professional Indemnity Insurance')]")]
        public IWebElement professionalInsuranceBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Professional Behaviours')]")]
        public IWebElement professionalBehavioursBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Revalidation')]")]
        public IWebElement revalidationBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Operation Numbers')]")]
        public IWebElement operationNumbersBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Clinical Outcomes')]")]
        public IWebElement clinicalOutcomesBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Continuing Professional Development')]")]
        public IWebElement professionalDevelopmentBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Reflection on Practice')]")]
        public IWebElement practiceBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'References')]")]
        public IWebElement referencesBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Submit Application')]")]
        public IWebElement submitAppBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Registration Requests')]")]
        public IWebElement registrationRequestsBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Application Requests')]")]
        public IWebElement applicationRequestsBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Users')]")]
        public IWebElement usersBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(@class,'user-info')]")]
        public IWebElement username { get; set; }

        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'menu-icon')]")]
        public IWebElement sidebarMenuBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'sign-out')]")]
        public IWebElement signOutBtn { get; set; }

        [FindsBy(How = How.LinkText, Using = "Home")]
        public IWebElement homeBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-sidenav")]
        public IWebElement sidebar { get; set; }

        [FindsBy(How = How.XPath, Using = "//i[contains(@class,'sign-out')]")]
        public IWebElement logoutBtn { get; set; }

        public IList<IWebElement> mainElements = new List<IWebElement>();
        public IList<IWebElement> sidebarElements = new List<IWebElement>();
        public IList<IWebElement> sections = new List<IWebElement>();

        public IList<IWebElement> GetMainElements()
        {
            mainElements.Add(sidebarMenuBtn);
            mainElements.Add(username);
            mainElements.Add(signOutBtn);
            mainElements.Add(accountDetailsBtn);
            mainElements.Add(homeBtn);
            return mainElements;
        }
        public IList<IWebElement> GetSidebarElements()
        {
            sidebarElements.Add(currentAppBtn);
            sidebarElements.Add(probityStatementsBtn);
            sidebarElements.Add(professionalInsuranceBtn);
            sidebarElements.Add(professionalBehavioursBtn);
            sidebarElements.Add(revalidationBtn);
            sidebarElements.Add(operationNumbersBtn);
            sidebarElements.Add(clinicalOutcomesBtn);
            sidebarElements.Add(professionalDevelopmentBtn);
            sidebarElements.Add(practiceBtn);
            sidebarElements.Add(referencesBtn);
            sidebarElements.Add(submitAppBtn);
            sidebarElements.Add(newAppBtn);
            sidebarElements.Add(appHistoryBtn);
            return sidebarElements;
        }
        public IList<IWebElement> GetSections()
        {
            sections.Add(probityStatementsBtn);
            sections.Add(professionalInsuranceBtn);
            sections.Add(professionalBehavioursBtn);
            sections.Add(revalidationBtn);
            sections.Add(operationNumbersBtn);
            sections.Add(clinicalOutcomesBtn);
            return sections;
        }
        public IList<IWebElement> GetAllElements()
        {
            mainElements.Add(sidebarMenuBtn);
            mainElements.Add(username);
            mainElements.Add(signOutBtn);
            mainElements.Add(currentAppBtn);
            mainElements.Add(accountDetailsBtn);
            mainElements.Add(homeBtn);
            mainElements.Add(probityStatementsBtn);
            mainElements.Add(professionalInsuranceBtn);
            mainElements.Add(professionalBehavioursBtn);
            mainElements.Add(revalidationBtn);
            mainElements.Add(operationNumbersBtn);
            mainElements.Add(clinicalOutcomesBtn);
            mainElements.Add(professionalDevelopmentBtn);
            mainElements.Add(practiceBtn);
            mainElements.Add(referencesBtn);
            mainElements.Add(newAppBtn);
            mainElements.Add(appHistoryBtn);
            mainElements.Add(submitAppBtn);

            return mainElements;
        }

        public AccountDetailsPage getAccountDetails()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(accountDetailsBtn));
            accountDetailsBtn.Click();
            return new AccountDetailsPage(TestBase.driver);
        }
        public ProbityStatementsPage getProbityStatements()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(probityStatementsBtn));
            probityStatementsBtn.Click();
            return new ProbityStatementsPage(TestBase.driver);
        }
        public ProfessionalInsurancePage getProfessionalInsurance()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalInsuranceBtn));
            professionalInsuranceBtn.Click();
            return new ProfessionalInsurancePage(TestBase.driver);
        } 
        public ProfessionalBehavioursPage getProfessionalBehaviours()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalBehavioursBtn));
            professionalBehavioursBtn.Click();
            return new ProfessionalBehavioursPage(TestBase.driver);
        }
        public ReflectionOnPracticePage getReflectionOnPractice()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(practiceBtn));
            practiceBtn.Click();
            return new ReflectionOnPracticePage(TestBase.driver);
        }
        public RevalidationPage getRevalidation()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(revalidationBtn));
            revalidationBtn.Click();
            return new RevalidationPage(TestBase.driver);
        }
        public OperationNumbersPage getOperationNUmbers()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(operationNumbersBtn));
            operationNumbersBtn.Click();
            return new OperationNumbersPage(TestBase.driver);
        }
        public ClinicalOutcomesPage getClinicalOutcomes()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(clinicalOutcomesBtn));
            clinicalOutcomesBtn.Click();
            return new ClinicalOutcomesPage(TestBase.driver);
        }
        public ContinuingDevelopmentPage getContinuingDevelopment()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(professionalDevelopmentBtn));
            professionalDevelopmentBtn.Click();
            return new ContinuingDevelopmentPage(TestBase.driver);
        }
        public ReferencesPage getReferences()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(referencesBtn));
            referencesBtn.Click();
            return new ReferencesPage(TestBase.driver);
        }
        public CertificateConfirmationPage getSubmitApplication()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(submitAppBtn));
            submitAppBtn.Click();
            return new CertificateConfirmationPage(TestBase.driver);
        }
        public RegistrationRequestsPage getRegistrationRequests()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(registrationRequestsBtn));
            registrationRequestsBtn.Click();
            return new RegistrationRequestsPage(TestBase.driver);
        }
        public ApplicationRequestsPage getApplicationRequests()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(applicationRequestsBtn));
            applicationRequestsBtn.Click();
            return new ApplicationRequestsPage(TestBase.driver);
        }
        public AdminUsersPage getUsers()
        {
            openSideMenuIfClosed();
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(usersBtn));
            usersBtn.Click();
            return new AdminUsersPage(TestBase.driver);
        }
        public NewApplicationPage getNewApp()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(newAppBtn));
            newAppBtn.Click();
            return new NewApplicationPage(TestBase.driver);
        }
        public PaymentCheckPage getPaymentCheck()
        {
            openSideMenuIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(paymentCheckBtn));
            paymentCheckBtn.Click();
            return new PaymentCheckPage(TestBase.driver);
        }
        public HomePage logout()
        {
            openSideMenuIfClosed(); 
            openCurrentAppIfClosed();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(logoutBtn));
            logoutBtn.Click();
            return new HomePage(TestBase.driver);
        }

        public void openSideMenuIfClosed()
        {
            if (TestBase.driver.Manage().Window.Size.Width < 1200 && !TestBase.ElementIsPresent(sidebar)){
                TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(sidebarMenuBtn));
                sidebarMenuBtn.Click();
                Thread.Sleep(500);
            }
            else
                TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("mat-tree")));
        } 
        
        public void openCurrentAppIfClosed()
        {
            try
            {
                if (TestBase.driver.FindElement(By.TagName("mat-nested-tree-node")).GetAttribute("aria-expanded").Equals("false"))
                {
                    TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(currentAppBtn));
                    currentAppBtn.Click();
                    Thread.Sleep(500);
                }
            }
            
            catch (NoSuchElementException e) { Console.WriteLine(e); }
        }
    }
}
