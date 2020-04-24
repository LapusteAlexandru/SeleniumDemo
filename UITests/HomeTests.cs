using RCoS;
using NUnit.Framework;
using Pages;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using System.Threading;

namespace HomeTests
{
    [TestFixture]
    [Category("Home")]
    public class HomeTests
    {
        [SetUp]
        public void Setup()
        {
            TestBase.RootInit();
        }

        [TearDown]
        public void Teardown()
        {
            TestBase.TakeScreenShot();
            TestBase.driver.Quit();
        }

        [Test, Order(1)]
        public void TestPageLoads()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            foreach (var e in homePage.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test]
        public void TestHomeBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.homeBtn.Click();
            Thread.Sleep(500);
            Assert.That(TestBase.driver.Url.Equals("https://rcs-cosmetics-client-dev.azurewebsites.net/home"));
        }
        [Test]
        public void TestContactsBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.contactsBtn.Click();
            Thread.Sleep(500);
            Assert.That(TestBase.driver.Url.Contains("/contacts"));
        }

        [Test]
        public void TestIntroductionBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.introToCosmeticsBtn.Click();
            Thread.Sleep(500);
            Assert.That(TestBase.driver.Url.Contains("/introduction-to-cosmetics"));
            Assert.That(TestBase.driver.FindElement(By.TagName("app-introduction-to-cosmetics")).Displayed);
        } 
        
        [Test]
        public void TestPatientInformationBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.patientInformationBtn.Click();
            Thread.Sleep(500);
            Assert.That(TestBase.driver.Url.Contains("/patient-information"));
            Assert.That(TestBase.driver.FindElement(By.TagName("app-patient-information")).Displayed);
        }
        
        [Test]
        public void TestRegisterSurgeonsBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.registerOfSurgeonsBtn.Click();
            Thread.Sleep(500);
            Assert.That(TestBase.driver.Url.Contains("/register-of-certified-cosmetics-surgeons"));
            Assert.That(TestBase.driver.FindElement(By.TagName("app-register-of-certified-cosmetics-surgeons")).Displayed);
        }
        
        [Test]
        public void TestInformationForApplicantsBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.informationForApplicantsBtn.Click();
            Thread.Sleep(500);
            Assert.That(TestBase.driver.Url.Contains("/information-for-applicants"));
            Assert.That(TestBase.driver.FindElement(By.TagName("app-information-for-applicants")).Displayed);
        }
        [Test]
        public void TestEligibilityBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.eligibilityBtn.Click();
            Thread.Sleep(500);
            Assert.That(TestBase.driver.Url.Contains("/eligibility-for-certification"));
            Assert.That(TestBase.driver.FindElement(By.TagName("app-eligibility-for-certification")).Displayed);
        }
        [Test]
        public void TestStandardsBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.informationForApplicantsBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-information-for-applicants")));
            Assert.That(homePage.standardsBtn.GetAttribute("href").Contains("/assets/pdfs/professional-standards-for-cosmetic-surgery-april-2020.pdf"));

        }
        [Test]
        public void TestGuildelinesBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.informationForApplicantsBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-information-for-applicants")));
            Assert.That(homePage.guidelinesBtn.GetAttribute("href").Contains("/assets/pdfs/certification-application-guidelines-april-2020.pdf"));
        }
        [Test]
        public void TestEvidenceBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.informationForApplicantsBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//app-information-for-applicants")));
            Assert.That(homePage.evidenceBtn.GetAttribute("href").Contains("/assets/pdfs/application-evidence-summary-april-2020.pdf"));
        }
        [Test]
        public void TestGMCBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.informationForApplicantsBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(homePage.gmcBtn));
            homePage.gmcBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Contains("/specialist-registration"));
        }
        
    }
}