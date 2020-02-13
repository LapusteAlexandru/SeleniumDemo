using RCoS;
using NUnit.Framework;
using Pages;

namespace HomeTests
{
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
            HomePage hp = new HomePage(TestBase.driver);
            foreach (var e in hp.GetMainElements())
                Assert.That(e.Displayed);
        }

        [Test]
        public void TestHomeBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            hp.homeBtn.Click();
            Assert.That(TestBase.driver.Url.Equals("https://rcs-cosmetics-client-dev.azurewebsites.net/home"));
        }

        [Test]
        public void TestIntroductionBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            hp.introToCosmeticsBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://www.gmc-uk.org/"));
        }
        [Test]
        public void TestEligibilityBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            hp.eligibilityBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://www.gmc-uk.org/"));
        }
        [Test]
        public void TestAdditionalInfoBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            hp.additionalInfoBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://www.gmc-uk.org/"));
        }
        [Test]
        public void TestStandardsBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            hp.standardsBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Contains("/professional-standards-for-cosmetic-surgery/"));
        }
        [Test]
        public void TestGuildelinesBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            Assert.That(hp.guidelinesBtn.GetAttribute("href").Equals("https://www.rcseng.ac.uk/-/media/files/rcs/standards-and-research/standards-and-policy/service-standards/cosmetic-surgery/cosmetic-surgery-certification-application-guidelines.pdf?la=en"));
        }
        [Test]
        public void TestEvidenceBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            Assert.That(hp.evidenceBtn.GetAttribute("href").Equals("https://www.rcseng.ac.uk/-/media/files/rcs/standards-and-research/standards-and-policy/service-standards/cosmetic-surgery/application-evidence-summary.pdf?la=en"));
        }
        [Test]
        public void TestGMCBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            hp.gmcBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Contains("/specialist-registration"));
        }

        [Test]

        public void TestTwitterBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            hp.twitterBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://twitter.com/rcsnews"));
        }
        [Test]

        public void TestFacebookBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            hp.facebookBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://www.facebook.com/royalcollegeofsurgeons"));
        }
        [Test]

        public void TestLinkedINBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            hp.linkedInBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://www.linkedin.com/company/royal-college-of-surgeons-of-england"));
        }
        [Test]

        public void TestYoutubeBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            hp.youtubeBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Equals("https://www.youtube.com/channel/UCt4twMdD8E2EBlnYSOWS8HA"));
        }
        [Test]

        public void TestGoogleBtn()
        {
            HomePage hp = new HomePage(TestBase.driver);
            hp.googleBtn.Click();
            TestBase.SwitchTab();
            Assert.That(TestBase.driver.Url.Contains("accounts.google.com/signin"));
        }
        
    }
}