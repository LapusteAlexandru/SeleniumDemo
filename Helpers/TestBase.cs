

using Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace RCoS
{
    public class TestBase
    {
        public static string username = "amdaris.rcos@gmail.com";
        public static string password = "123aA@123";
        public static string adminUsername = "mail@mail.com";
        public static string adminPassword = "P@ssword1";
        public static string apiUsername = "amdaris.rcos.api@gmail.com";
        public static string apiPassword = "123aA@123";
        public static string userTitle = "Dr.";
        public static string userFirstName = "John";
        public static string userLastName = "Doe";
        public static string userAddress = "UK";
        public static string userPhone = "123123123";
        public static string userGender = "Male";
        public static int userGmcNumber = 1231231;
        public static string userGmcSpecialty = "Vascular Surgery";
        public static string userCareerGrade = "Consultant established";
        public static string currentMonth = DateTime.Now.ToString("MMMM");
        public static string currentDay = DateTime.Now.ToString("dd");
        public static string currentYear = DateTime.Now.Year.ToString();
        public static string currentDate = currentMonth + " " + currentDay + ", " + currentYear;
        public static string userBirthday = currentMonth + " 01, " + currentYear;
        public static string caseDate = DateTime.Now.Month + "/1/" + currentYear;
        public static List<string> userData = new List<string> {userGender, currentDate, username, userBirthday, userGmcNumber.ToString(),userGmcSpecialty,userCareerGrade };
        public static List<string> expandableUserData = new List<string> {userPhone,userAddress};
        public static IWebDriver driver { get; set; }
        public static WebDriverWait wait { get; set; }
        public static void RootInit()
        {
            var cap = new ChromeOptions();
            cap.AddArgument("start-maximized");
            cap.AddArgument("--ignore-certificate-errors");
            cap.AddArgument("--disable-popup-blocking");
            cap.AddArgument("--incognito");
            driver = new ChromeDriver(cap);
            string baseURL= TestContext.Parameters["baseURL"];
            if (baseURL == null || baseURL == "")
                driver.Url = "https://rcs-cosmetics-client-dev.azurewebsites.net/";
            else
                driver.Url = baseURL;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }
        public static void SwitchTab()
        {
            var parentTab = driver.CurrentWindowHandle;
            IList<string> tabs = new List<string>(driver.WindowHandles);
            foreach (var handle in tabs)
                if (!parentTab.Equals(handle))
                    driver.SwitchTo().Window(handle);
        }

            
        public static void SelectOption(string select,string option)
        {
            driver.FindElement(By.Id(select)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(string.Format("//span[contains(text(),'{0}')]", option))));
            driver.FindElement(By.XPath(string.Format("//span[contains(text(),'{0}')]",option))).Click();
        }

        public static bool ElementIsPresent(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }


        public static void TakeScreenShot()
        {

            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                string testName = TestContext.CurrentContext.Test.Name;
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss"); 
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string path = Directory.GetCurrentDirectory() + timestamp+testName+ ".png";
                ss.SaveAsFile(path);
                TestContext.AddTestAttachment(path);
            }
        }

        public static string getJWT(string username,string password)
        {
            string jwt = "";
            RestRequest request = new RestRequest("/connect/token", Method.POST); 
            RestClient identityClient = new RestClient("https://rcs-cosmetics-identity-dev.azurewebsites.net");
            request.AddParameter("client_id", "rcs.api.swagger.client");
            request.AddParameter("client_secret", "secret");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("grant_type", "password");
            // act
            IRestResponse response = identityClient.Execute(request);
            var responseBody = JObject.Parse(response.Content);
            jwt = responseBody.GetValue("access_token").ToString();
            return jwt;
        }
        public static int getObjectID(string endpoint,string jwt)
        {
            int id;
            RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
            RestRequest request = new RestRequest(endpoint, Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            var token = JToken.Parse(response.Content);
            if (token is JArray)
            {
                var responseBody = token.ToObject<List<JObject>>();
                id = (int)responseBody[0].GetValue("id");
            }
            else
            {
                var responseBody = token.ToObject<JObject>();
                id = (int)responseBody.GetValue("id");
            }
            return id;
        }

        public static void uploadField(string fileName,string fileExtension)
        {
            IList<IWebElement> uploadInputs = driver.FindElements(By.XPath("//input[@type='file']"));
            foreach (IWebElement e in uploadInputs)
            {
                e.SendKeys(TestContext.Parameters["uploadFilesPath"] + fileName + "." + fileExtension);
                try
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//mat-form-field[contains(@class,'uploaded-file')]//input")));
                    
                }
                catch { }
            }
            IList<IWebElement> uploadProgressBars = driver.FindElements(By.XPath("//mat-progress-bar/following-sibling::span"));
            foreach (IWebElement e in uploadProgressBars)
            {
                wait.Until(ExpectedConditions.TextToBePresentInElement(e, "100/100"));
            }

            Thread.Sleep(2000);
        }
    }
}

