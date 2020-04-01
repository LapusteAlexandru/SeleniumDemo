

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
        public static string userGmcNumber = "1231231";
        public static string userGmcSpecialty = "Vascular Surgery";
        public static string userCareerGrade = "Consultant established";
        public static int sectionId ;
        public static DateTime dt = DateTime.Now;
        public static string currentMonth = dt.ToString("MMMM");
        public static int currentDay = dt.Day;
        public static string currentDayNumber = dt.ToString("dd");
        public static string currentYear = dt.Year.ToString();
        public static string currentDate = currentMonth + " " + currentDay + ", " + currentYear;
        public static string userBirthday = currentMonth + " 01, " + currentYear;
        public static string caseDate = dt.Month + "/1/" + currentYear;
        public static List<string> userData = new List<string> {userGender, currentDate, username, userBirthday, userGmcNumber.ToString(),userGmcSpecialty,userCareerGrade };
        public static List<string> applicantData = new List<string> {appUsername, userGmcNumber.ToString(),userGmcSpecialty};
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
        public static int getObjectID(string endpoint,string jwt,int id)
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
        public static int getObjectID(string endpoint, string jwt)
        {
            return getObjectID(endpoint, jwt, 0);
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
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            if (cnn.State == ConnectionState.Open)
                Console.WriteLine("Connected successfully!");
            if (tableName.Contains("AssignedApplications"))
            {
                var id = getUserId(username);
                sql = $"DELETE FROM {tableName} WHERE EvaluatorID = {id}";
            }
            else
                sql = $"DELETE FROM {tableName} WHERE Email = {username}";
            command = new SqlCommand(sql, cnn);
            command.ExecuteNonQuery();
            command.Dispose();
        }
        public static void deleteSectionData(string tableName, string username, string section,int statusValue)
        {
            SqlConnection cnn;
            SqlCommand command;
            object result = null;
            string sql;
            string connetionString = TestContext.Parameters["cosmeticsConnectionString"];
            var jwt = getJWT(username, password);
            var id = getObjectID("/api/applicants", jwt);
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            if (cnn.State == ConnectionState.Open)
                Console.WriteLine("Connected successfully!");
            if (section != "" && section !="Status")
            {
                sql = $"SELECT {section}Id FROM [dbo].[Applications] WHERE ApplicantId ={id}";
                command = new SqlCommand(sql, cnn);
                try { result = command.ExecuteScalar(); }
                catch(Exception e) { Console.WriteLine(e.Message); }
                if (result is System.DBNull)
                    return;
                else
                    sectionId = (int)(result);
                sql = $"UPDATE [dbo].[Applications] SET {section}Id = null WHERE ApplicantId ={id}" ;
                command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            if(section == "Status")
            {
                sql = $"SELECT Id FROM [dbo].[Applications] WHERE ApplicantId ={id}";
                command = new SqlCommand(sql, cnn);
                int applicationId = (int)(command.ExecuteScalar());
                command.Dispose();
                sql = $"UPDATE [dbo].[Applications] SET {section} = {statusValue} WHERE Id ={applicationId}";
                command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            if (tableName.Contains("Documents"))
            {
                sql = $"SELECT Id FROM [dbo].[Applications] WHERE ApplicantId ={id}";
                command = new SqlCommand(sql, cnn);
                int applicationId = (int)(command.ExecuteScalar());
                command.Dispose();
                sql = $"DELETE FROM {tableName} WHERE ApplicationId ={applicationId}";
                command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            if (tableName.Contains("Practice"))
            {
                sql = $"SELECT Id FROM [dbo].[Applications] WHERE ApplicantId ={id}";
                command = new SqlCommand(sql, cnn);
                int applicationId = (int)(command.ExecuteScalar());
                command.Dispose();
                sql = $"DELETE FROM {tableName} WHERE ApplicationId ={applicationId}";
                command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            else if (!tableName.Contains("Applications"))
            {
                sql = $"DELETE FROM {tableName} WHERE Id ={sectionId}"; 
                command = new SqlCommand(sql, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            
        }

        public static int getApplicationId(int applicantId)
        {
            SqlConnection cnn;
            SqlCommand command;
            string sql;
            string connetionString = TestContext.Parameters["cosmeticsConnectionString"];
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            sql = $"SELECT Id FROM [dbo].[Applications] WHERE ApplicantId ={applicantId}";
            command = new SqlCommand(sql, cnn);
            int applicationId = (int)(command.ExecuteScalar());
            command.Dispose();
            return applicationId;
        }
        
        public static int getUserId(string username)
        {
            SqlConnection cnn;
            SqlCommand command;
            string sql;
            string connetionString = TestContext.Parameters["cosmeticsConnectionString"];
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            sql = $"SELECT Id FROM [dbo].[Users] WHERE Email ='{username}'";
            command = new SqlCommand(sql, cnn);
            int userId = (int)(command.ExecuteScalar());
            command.Dispose();
            return userId;
        }
        public static int getRegistrationId(int userId)
        {
            SqlConnection cnn;
            SqlCommand command;
            string sql;
            string connetionString = TestContext.Parameters["cosmeticsConnectionString"];
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            sql = $"SELECT Id FROM [dbo].[RegistrationRequests] WHERE UserId ={userId}";
            command = new SqlCommand(sql, cnn);
            int registrationId = (int)(command.ExecuteScalar());
            command.Dispose();
            return registrationId;
        }
        public static void deleteSectionData(string tableName, string username)
        {
            deleteSectionData(tableName, username, "",1);
        }public static void deleteSectionData(string tableName, string username,string section)
        {
            deleteSectionData(tableName, username, section,1);
        }
    }
}

