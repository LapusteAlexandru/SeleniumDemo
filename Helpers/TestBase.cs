

using Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace RCoS
{
    public class TestBase
    {
        public static string username = "amdaris.rcos@gmail.com";
        public static string evaluatorUsername = "amdaris.rcos.evaluator@gmail.com";
        public static string password = "123aA@123";
        public static string uiUsername = "amdaris.rcos.ui@gmail.com";
        public static string appUsername = "amdaris.rcos.application@gmail.com";
        public static string adminUsername = "mail@mail.com";
        public static string adminPassword = "P@ssword1";
        public static string apiUsername = "amdaris.rcos.api@gmail.com";
        public static string apiPassword = "123aA@123";
        public static string cardNumber = "4111111111111111";
        public static string expiryMonthDate = "02";
        public static string expiryYearDate = "30";
        public static string cvv = "123";
        public static string userTitle = "Dr.";
        public static string userFirstName = "John";
        public static string userLastName = "Doe";
        public static string userAddress = "UK";
        public static string userPhone = "123123123";
        public static string userGender = "Male";
        public static string userGmcNumber = "0000001";
        public static string apiUserGmcNumber = "0000003";
        public static string uiUserGmcNumber = "0000002";
        public static string appUserGmcNumber = "0000004";
        public static string evalUserGmcNumber = "0000005";
        public static string userGmcSpecialty = "Vascular Surgery";
        public static string userCareerGrade = "Consultant established";
        public static int sectionId ;
        public static DateTime dt = DateTime.Now;
        public static string currentMonth = dt.ToString("MMMM");
        public static int currentDay = dt.Day;
        public static string currentDayNumber = dt.Day.ToString();
        public static string currentYear = dt.Year.ToString();
        public static string currentDate = currentMonth + " " + currentDay.ToString("00") + ", " + currentYear;
        public static string userBirthday = currentMonth + " 01, " + currentYear;
        public static string caseDate = "1/"+ dt.Month + "/" + currentYear;
        public static List<string> userData = new List<string> {userGender, currentDate, username, userBirthday, userGmcNumber.ToString(),userGmcSpecialty,userCareerGrade };
        public static List<string> applicantData = new List<string> {appUsername, appUserGmcNumber.ToString(),userGmcSpecialty};
        public static List<string> expandableUserData = new List<string> {userPhone,userAddress};
        public static IWebDriver driver { get; set; }
        public static WebDriverWait wait { get; set; }
        public static WebDriverWait uploadWait { get; set; }
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
            uploadWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'Got it')]")));
                driver.FindElement(By.XPath("//a[contains(text(),'Got it')]")).Click();
            }
            catch(NoSuchElementException e)
            {
                Console.WriteLine(e);
            }
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
            string jwt;
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
        public static int getObjectID(string endpoint,string jwt,int id=0)
        {
            int objectId;
            RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
            RestRequest request;
            if (id>0) 
                request = new RestRequest(endpoint+"/"+id, Method.GET);
            else
                request = new RestRequest(endpoint , Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            var token = JToken.Parse(response.Content);

            if (token is JArray)
            {
                var responseBody = token.ToObject<List<JObject>>();
                objectId = (int)responseBody[0].GetValue("id");
            }
            else
            {
                var responseBody = token.ToObject<JObject>();
                objectId = (int)responseBody.GetValue("id");
            }
            return objectId;
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
            Thread.Sleep(500);
            IList <IWebElement> deleteButtons = driver.FindElements(By.XPath("//app-file-item//button"));
            foreach (IWebElement e in deleteButtons)
            {
                uploadWait.Until(ExpectedConditions.ElementToBeClickable(e));
            }

        }

        public static void deleteUserData(string tableName,string username)
        {
            SqlConnection cnn;
            SqlCommand command;
            string sql; 
            string connetionString;
            if (tableName.Contains("AspNetUsers"))
                connetionString = TestContext.Parameters["identityConnectionString"];
            else
                connetionString = TestContext.Parameters["cosmeticsConnectionString"];
            using (cnn = new SqlConnection(connetionString))
            {
                cnn.Open();
                var id = getUserId(username);
                if (cnn.State == ConnectionState.Open)
                    Console.WriteLine("Connected successfully!");
                if (tableName.Contains("AssignedApplications") || tableName.Contains("ApplicationReviews"))
                {
                    sql = $"DELETE FROM {tableName} WHERE UserId = {id}";
                }
                else
                    sql = $"DELETE FROM {tableName} WHERE Email = '{username}'";
                if (tableName.Contains("[dbo].[Applications]") && username.Equals(appUsername))
                    sql = $"DELETE FROM {tableName} WHERE UserId = {id} AND Status <> 4";
                else 
                    if (tableName.Contains("[dbo].[Applications]"))
                        sql = $"DELETE FROM {tableName} WHERE UserId = {id}";
                using (command = new SqlCommand(sql, cnn))
                    command.ExecuteNonQuery();
            }
        }
        public static void deleteSectionData(string tableName, string username, string section="",int statusValue=1)
        {
            SqlConnection cnn;
            SqlCommand command;
            object result = null;
            string sql;

            string connetionString = TestContext.Parameters["cosmeticsConnectionString"];
            var jwt = getJWT(username, password);
            var id = getObjectID("/api/applicants", jwt);
            using (cnn = new SqlConnection(connetionString))
            {
                cnn.Open();
                if (cnn.State == ConnectionState.Open)
                    Console.WriteLine("Connected successfully!");
                if (section != "" && section != "Status" && section != "Payments")
                {
                    sql = $"SELECT {section}Id FROM [dbo].[Applications] WHERE UserId ={id}";
                    command = new SqlCommand(sql, cnn);
                    try { result = command.ExecuteScalar(); }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                    if (result is System.DBNull)
                        return;
                    else
                        sectionId = (int)(result);
                    sql = $"UPDATE [dbo].[Applications] SET {section}Id = null WHERE UserId ={id}";
                    using (command = new SqlCommand(sql, cnn))
                        command.ExecuteNonQuery();
                }
                if(tableName.Contains("Applications") && section == "Status")
                {
                    sql = $"UPDATE [dbo].[Applications] SET {section} = {statusValue} WHERE Id in (SELECT Id FROM [dbo].[Applications] WHERE UserId ={id})";
                    using (command = new SqlCommand(sql, cnn))
                            command.ExecuteNonQuery();
                }
                if(tableName.Contains("Users") && section == "Payments")
                {
                    sql = $"UPDATE [dbo].[Users] SET Status = {statusValue} WHERE Id = {getUserId(username)}";
                    using (command = new SqlCommand(sql, cnn))
                            command.ExecuteNonQuery();
                }
                if (tableName.Contains("Documents"))
                {
                    if (username.Equals(appUsername))
                        sql = $"DELETE FROM {tableName} WHERE ApplicationId in ( SELECT Id FROM [dbo].[Applications] WHERE UserId ={id} AND Status <> 4 ) ";
                    else
                        sql = $"DELETE FROM {tableName} WHERE ApplicationId in ( SELECT Id FROM [dbo].[Applications] WHERE UserId ={id})";
                    using (command = new SqlCommand(sql, cnn))
                        command.ExecuteNonQuery();
                }
                if (tableName.Contains("ReflectionsOnPractices"))
                {
                    sql = $"DELETE FROM {tableName} WHERE ApplicationId in (SELECT Id FROM [dbo].[Applications] WHERE UserId ={id})";
                    using (command = new SqlCommand(sql, cnn))
                        command.ExecuteNonQuery();
                }
                else if (!tableName.Contains("Applications"))
                {

                    sql = $"DELETE FROM {tableName} WHERE Id ={sectionId}"; 
                    using (command = new SqlCommand(sql, cnn))
                            command.ExecuteNonQuery();
                }

            }
        }

        public static int getApplicationId(int applicantId)
        {
            SqlConnection cnn;
            SqlCommand command;
            string sql; int applicationId;
            string connetionString = TestContext.Parameters["cosmeticsConnectionString"];
            using (cnn = new SqlConnection(connetionString))
            {
                cnn.Open();
                sql = $"SELECT Id FROM [dbo].[Applications] WHERE UserId ={applicantId}";
                using (command = new SqlCommand(sql, cnn))
                    applicationId = (int)(command.ExecuteScalar());
            }
            return applicationId;
        }
        
        public static int getUserId(string username)
        {
            SqlConnection cnn;
            SqlCommand command;
            string sql;
            int userId = -1;
            string connetionString = TestContext.Parameters["cosmeticsConnectionString"];
            using (cnn = new SqlConnection(connetionString))
            {
                cnn.Open();
                sql = $"SELECT Id FROM [dbo].[Users] WHERE Email ='{username}'";
                using (command = new SqlCommand(sql, cnn))
                    try
                    {
                        userId = (int)(command.ExecuteScalar());
                    }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine(e.Message);
                    }
            }
            return userId;
        }
        public static int getRegistrationId(int userId)
        {
            SqlConnection cnn;
            SqlCommand command;
            string sql; int registrationId;
            string connetionString = TestContext.Parameters["cosmeticsConnectionString"];
            using (cnn = new SqlConnection(connetionString))
            {
                cnn.Open();
                sql = $"SELECT Id FROM [dbo].[RegistrationRequests] WHERE UserId ={userId}";
                using (command = new SqlCommand(sql, cnn))
                    registrationId = (int)(command.ExecuteScalar());
            }
            return registrationId;
        }

        public static void ScrollToElemnent(string xpath)
        {
            var element = driver.FindElement(By.XPath(xpath));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }
    }
}

