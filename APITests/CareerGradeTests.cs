using Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using RCoS;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace APITests
{
    [TestFixture]
    [Category("CareerGrades")]
    class CareerGradeTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test]
        public void GetCareerGradesTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/career-grades", Method.GET);
            var jwt = TestBase.getJWT(TestBase.adminUsername, TestBase.adminPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            List<string> expectedGrades = new List<string> { "Consultant established", "Consultant newly appointed", "Associate Specialist", "Fellow", "Trainee" };
            var actualGrades = JsonConvert.DeserializeObject<List<GradesModel>>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            foreach (var actualGrade in actualGrades)
            {
                Assert.That(expectedGrades.Contains(actualGrade.description));
            }
        }
    }
}
