using NUnit.Framework;
using OpenQA.Selenium;
using Pages;
using RCoS;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ReflectionOnPracticeTests
{
    [TestFixture]
    [Category("ReflectionOnPractice")]
    class ReflectionOnPracticeTests
    {
        [OneTimeSetUp]
        public void Clear()
        {
            TestBase.deleteSectionData("[dbo].[ReflectionsOnPractices]", TestBase.uiUsername);
        }
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

            ReflectionOnPracticePage reflectionOnPracticePage = getReflectionOnPractice().Item1;
            foreach (var e in reflectionOnPracticePage.GetMainElements())
                Assert.That(e.Displayed);
        }
        [Test, Order(2)]
        public void TestRequiredMsgsCase1()
        {

            requiredMessages(1);
        }
        [Test, Order(2)]
        public void TestRequiredMsgsCase2()
        {

            requiredMessages(2);
        }
        [Test, Order(2)]
        public void TestRequiredMsgsCase3()
        {

            requiredMessages(3);
        }
        [Test, Order(2)]
        public void TestRequiredMsgsCase4()
        {
            requiredMessages(4);
        }
        [Test, Order(4)]
        public void TestSubmitSuccessfully()
        {
            var(reflectionOnPracticePage, dashboardPage) = getReflectionOnPractice();
            
            reflectionOnPracticePage.CompleteForm(1,"png", false);
            reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
            reflectionOnPracticePage.CompleteForm(2,"png", false);
            reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
            reflectionOnPracticePage.CompleteForm(3,"png", false);
            reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
            reflectionOnPracticePage.CompleteForm(4,"png", false);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(reflectionOnPracticePage.title));
            foreach (var e in reflectionOnPracticePage.GetFormTabs())
            {
                e.Click();
                var selected = e.GetAttribute("aria-selected");
                TestBase.wait.Equals(selected.Equals(true));
                reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
                Thread.Sleep(500);
                IList<IWebElement> actualInputs = reflectionOnPracticePage.GetTextElements();
                for (int i = 0; i < actualInputs.Count; i++)
                {
                    if (actualInputs[i].TagName.Equals("input"))
                    {
                        if (actualInputs[i].GetAttribute("formcontrolname").Contains("Email"))
                            Assert.That(actualInputs[i].GetAttribute("value").Equals(TestBase.uiUsername));
                        else
                            Assert.That(actualInputs[i].GetAttribute("value").Equals(reflectionOnPracticePage.inputText)); 
                    }
                    else
                        Assert.That(actualInputs[i].GetAttribute("value").Equals(reflectionOnPracticePage.textareaText + reflectionOnPracticePage.inputText)); 
                    
                }
                Assert.That(reflectionOnPracticePage.dateInput.GetAttribute("value").Equals(TestBase.caseDate));
            }
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            Assert.That(reflectionOnPracticePage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }
        [Test, Order(3)]
        public void TestSaveAsDraft()
        {

            var (reflectionOnPracticePage, dashboardPage) = getReflectionOnPractice();
            reflectionOnPracticePage.CompleteForm(1,"png", false,true);
            reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
            reflectionOnPracticePage.CompleteForm(2,"png", false, true);
            reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
            reflectionOnPracticePage.CompleteForm(3,"png", false, true);
            reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
            reflectionOnPracticePage.CompleteForm(4,"png", false, true);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(reflectionOnPracticePage.title));
            foreach (var e in reflectionOnPracticePage.GetFormTabs())
            {
                e.Click();
                var selected = e.GetAttribute("aria-selected");
                TestBase.wait.Equals(selected.Equals(true));
                reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
                Thread.Sleep(500);
                IList<IWebElement> actualInputs = reflectionOnPracticePage.GetTextElements();
                for (int i = 0; i < actualInputs.Count; i++)
                {
                    if (actualInputs[i].TagName.Equals("input"))
                    {
                        if (actualInputs[i].GetAttribute("formcontrolname").Contains("Email"))
                            Assert.That(actualInputs[i].GetAttribute("value").Equals(TestBase.uiUsername));
                        else
                            Assert.That(actualInputs[i].GetAttribute("value").Equals(reflectionOnPracticePage.inputText)); 
                    }
                    else
                        Assert.That(actualInputs[i].GetAttribute("value").Equals(reflectionOnPracticePage.textareaText + reflectionOnPracticePage.inputText)); 
                    
                }
                Assert.That(reflectionOnPracticePage.dateInput.GetAttribute("value").Equals(TestBase.caseDate));
            }
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            Assert.That(reflectionOnPracticePage.statusIndicator.GetAttribute("mattooltip").Contains("Not Started"));
        }
        [Test]
        public void TestNoOfCharLimit()
        {
            string dummyExtraLongText = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,Lorem ipsum dolor sit amet, consectetuer adipiscing elit.Aenean commodo ligula eget dolor.Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim.Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium.Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus.Phasellus viverra nulla ut metus varius laoreet.Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue.Curabitur ullamcorper ultricies nisi. Nam eget dui.Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus.Donec vitae sapien ut libero venenatis faucibus.Nullam quis ante.Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim.Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium.Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus.Phasellus viverra nulla ut metus varius laoreet.Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue.Curabitur ullamcorper ultricies nisi. Nam eget dui.Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus.Donec vitae sapien ut libero venenatis faucibus.Nullam quis ante.Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc, Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim.Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium.Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus.Phasellus viverra nulla ut metus varius laoreet.Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue.Curabitur ullamcorper ultricies nisi. Nam eget dui.Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus.Donec vitae sapien ut libero venenatis faucibus.Nullam quis ante.Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim.Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium.Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus.Phasellus viverra nulla ut metus varius laoreet.Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue.Curabitur ullamcorper ultricies nisi. Nam eget dui.Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus.Donec vitae sapien ut libero venenatis faucibus.Nullam quis ante.";
            string dummyLongText = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem.";

            ReflectionOnPracticePage reflectionOnPracticePage = getReflectionOnPractice().Item1;
            reflectionOnPracticePage.textareaText = dummyExtraLongText;
            reflectionOnPracticePage.inputText = dummyLongText;
            reflectionOnPracticePage.CompleteForm(1,"png", false);
            reflectionOnPracticePage.case1TabBtn.Click();
            foreach (var e in reflectionOnPracticePage.limitReachedMsgs)
                Assert.That(e.Displayed);
            foreach (var e in reflectionOnPracticePage.textareaLimitCounter)
                Assert.That(e.Text.Equals("8000/8000"));
            Assert.That(reflectionOnPracticePage.limitReachedMsgs.Count.Equals(6));
           

        }

        [Test]
        public void TestEditSuccessfully()
        {

            var (reflectionOnPracticePage, dashboardPage) = getReflectionOnPractice();
            reflectionOnPracticePage.CompleteForm(1,"", true);
            reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
            reflectionOnPracticePage.CompleteForm(2,"", true);
            reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
            reflectionOnPracticePage.CompleteForm(3,"", true);
            reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
            reflectionOnPracticePage.CompleteForm(4,"", true);
            TestBase.driver.Navigate().Refresh();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(reflectionOnPracticePage.title));
            foreach (var e in reflectionOnPracticePage.GetFormTabs())
            {
                e.Click();
                var selected = e.GetAttribute("aria-selected");
                TestBase.wait.Equals(selected.Equals(true));
                reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
                Thread.Sleep(500);
                IList<IWebElement> actualInputs = reflectionOnPracticePage.GetTextElements();
                for (int i = 0; i < actualInputs.Count; i++)
                {
                    if (actualInputs[i].TagName.Equals("input"))
                    {
                        if (actualInputs[i].GetAttribute("formcontrolname").Contains("Email"))
                            Assert.That(actualInputs[i].GetAttribute("value").Equals(TestBase.uiUsername));
                        else
                            Assert.That(actualInputs[i].GetAttribute("value").Equals(reflectionOnPracticePage.inputText + "Updated"));
                    }
                    else
                        Assert.That(actualInputs[i].GetAttribute("value").Equals(reflectionOnPracticePage.textareaText + "Updated" + reflectionOnPracticePage.inputText + "Updated"));
                }
                Assert.That(reflectionOnPracticePage.dateInput.GetAttribute("value").Equals(TestBase.caseDate));
            }
            dashboardPage.openSideMenuIfClosed();
            dashboardPage.openCurrentAppIfClosed();
            Assert.That(reflectionOnPracticePage.statusIndicator.GetAttribute("mattooltip").Contains("Completed"));
        }

        private void requiredMessages(int caseFormNo)
        {
            ReflectionOnPracticePage reflectionOnPracticePage = getReflectionOnPractice().Item1;
            IWebElement tab = reflectionOnPracticePage.case1TabBtn;
            switch (caseFormNo)
            {
                case 1:
                    break;
                case 2:

                    tab=reflectionOnPracticePage.case2TabBtn;
                    break;
                case 3:

                    tab = reflectionOnPracticePage.case3TabBtn;
                    break;
                case 4:

                    tab=reflectionOnPracticePage.case4TabBtn;
                    break;

                default:break;
            }
            tab.Click();
            TestBase.wait.Until(ExpectedConditions.ElementToBeClickable(reflectionOnPracticePage.hospitalSiteInput));
            reflectionOnPracticePage.submitBtn.Click();
            tab.Click();
            var selected = tab.GetAttribute("aria-selected");
            TestBase.wait.Equals(selected.Equals(true));
            reflectionOnPracticePage = new ReflectionOnPracticePage(TestBase.driver);
            Thread.Sleep(500);
            foreach (var e in reflectionOnPracticePage.requiredMsgs)
            {
                Assert.That(e.Displayed);
            }
            Assert.That(reflectionOnPracticePage.requiredMsgs.Count.Equals(12));
        }

        private (ReflectionOnPracticePage,DashboardPage) getReflectionOnPractice()
        {
            HomePage homePage = new HomePage(TestBase.driver);
            LoginPage loginPage = homePage.GetLogin();
            DashboardPage dashboardPage = loginPage.DoLogin(TestBase.uiUsername, TestBase.password);
            ReflectionOnPracticePage reflectionOnPracticePage = dashboardPage.getReflectionOnPractice();
            return (reflectionOnPracticePage, dashboardPage);
        }
    }
}
