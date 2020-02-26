using RCoS;
using NUnit.Framework;
using Pages;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;

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
            Assert.That(TestBase.driver.Url.Equals("https://rcs-cosmetics-client-dev.azurewebsites.net/home"));
        }

        [Test]
        public void TestIntroductionBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.introToCosmeticsBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://www.gmc-uk.org/"));
        }
        [Test]
        public void TestEligibilityBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.eligibilityBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://www.gmc-uk.org/"));
        }
        [Test]
        public void TestAdditionalInfoBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.additionalInfoBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://www.gmc-uk.org/"));
        }
        [Test]
        public void TestStandardsBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.standardsBtn.Click();
            TestBase.wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h4[contains(text(),'Professional Standards for Cosmetic Surgery')]")));
            Assert.That(TestBase.driver.Url.Contains("/standards"));
        }
        [Test]
        public void TestGuildelinesBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            Assert.That(homePage.guidelinesBtn.GetAttribute("href").Equals("https://www.rcseng.ac.uk/-/media/files/rcs/standards-and-research/standards-and-policy/service-standards/cosmetic-surgery/cosmetic-surgery-certification-application-guidelines.pdf?la=en"));
        }
        [Test]
        public void TestEvidenceBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            Assert.That(homePage.evidenceBtn.GetAttribute("href").Equals("https://www.rcseng.ac.uk/-/media/files/rcs/standards-and-research/standards-and-policy/service-standards/cosmetic-surgery/application-evidence-summary.pdf?la=en"));
        }
        [Test]
        public void TestGMCBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.gmcBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Contains("/specialist-registration"));
        }

        [Test]

        public void TestTwitterBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.twitterBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://twitter.com/rcsnews"));
        }
        [Test]

        public void TestFacebookBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.facebookBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://www.facebook.com/royalcollegeofsurgeons"));
        }
        [Test]

        public void TestLinkedINBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.linkedInBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Contains("royal-college-of-surgeons-of-england"));
        }
        [Test]

        public void TestYoutubeBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.youtubeBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://www.youtube.com/channel/UCt4twMdD8E2EBlnYSOWS8HA"));
        }
        [Test]

        public void TestGoogleBtn()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            homePage.googleBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Contains("accounts.google.com/signin"));
        }
        
    }
}