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
    [Category("APIProbityStatements")]
    class ProbityStatementTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test]
        public void GetProbityStatmentTest()
        {
            // create request

            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            var id = TestBase.getObjectID("/api/applicants", jwt);
            var applicationId = TestBase.getApplicationId(id);
            RestRequest request = new RestRequest($"/api/probity-statements/{applicationId}", Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            List<string> expectedProbity = new List<string> {"I have something to declare" };
            // act
            IRestResponse response = apiClient.Execute(request);
            var actualRequests = JsonConvert.DeserializeObject<ProbityStatementsModel>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualRequests.acceptProfessionalObligations.Equals(true));
            Assert.That(actualRequests.acceptAbsenceOfSuspensions.Equals(true));
            Assert.That(actualRequests.subjectOfInvestigation.Equals(expectedProbity[0]));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test, Order(1)]
        public void PostProbityTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/probity-statements", Method.POST);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            
            ProbityStatementsModel probityStatement = new ProbityStatementsModel();
            var id = TestBase.getObjectID("/api/applicants", jwt);
            probityStatement.id = 0;
            probityStatement.applicationId = TestBase.getApplicationId(id); 
            probityStatement.acceptAbsenceOfSuspensions = true;
            probityStatement.acceptProfessionalObligations = true;
            probityStatement.subjectOfInvestigation = "I have nothing to declare";

            var body = JsonConvert.SerializeObject(probityStatement);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        [Test, Order(2)]
        public void PutProbityTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/probity-statements", Method.PUT);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            var id = TestBase.getObjectID("/api/applicants", jwt);
            var applicationId = TestBase.getApplicationId(id);
            var probityId = TestBase.getObjectID("/api/probity-statements",  jwt, applicationId);

            ProbityStatementsModel probityStatement = new ProbityStatementsModel();


            probityStatement.id = probityId;
            probityStatement.applicationId = applicationId;
            probityStatement.acceptAbsenceOfSuspensions = true;
            probityStatement.acceptProfessionalObligations = true;
            probityStatement.subjectOfInvestigation = "I have something to declare";

            var body = JsonConvert.SerializeObject(probityStatement);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
