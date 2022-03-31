using NUnit.Framework;
using Helpers;
using Pages;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    [Category("Home")]
    class HomeTest
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

        [Test]
        public void TestPageLoads()
        {
            HomePage hp = new HomePage(TestBase.driver);
            IList<IWebElement> mainElements = hp.GetMainElements();
            foreach(IWebElement element in mainElements )
            {
                Assert.True(element.Displayed);
            }
        }
    }
}
