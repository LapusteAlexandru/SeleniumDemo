using Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using RCoS;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace APITests
{
    [TestFixture]
    [Category("APIReflectionOnPractice")]
    class ReflectionOnPracticeTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test]
        public void GetReflectionOnPracticeTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/reflection-on-practice", Method.GET);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test, Order(1)]
        public void PostReflectionOnPracticeTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/reflection-on-practice", Method.POST);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            var id = TestBase.getObjectID("/api/applicants", jwt);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            DocumentsModel documentModel = new DocumentsModel();
            documentModel.fileName = "string";
            documentModel.blobStorageId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            
            List<ReflectionOnPracticeModel> reflectionOnPracticeList = new List<ReflectionOnPracticeModel>();
            for (int i = 1; i <= 4; i++)
            {
                ReflectionOnPracticeModel reflectionOnPractice = new ReflectionOnPracticeModel();
                reflectionOnPractice.id = 0;
                reflectionOnPractice.applicantId = id;
                reflectionOnPractice.caseNumber = i;
                reflectionOnPractice.caseDate = "2020-02-10T09:43:25.179Z";
                reflectionOnPractice.hospitalSite = "HospitalSite "+i;
                reflectionOnPractice.locationOfEvent = "LocationOfEvent " + i;
                reflectionOnPractice.role = "Role " + i;
                reflectionOnPractice.procedure = "Procedure " + i;
                reflectionOnPractice.descriptionOfEvent = "DescriptionOfEvent " + i;
                reflectionOnPractice.nameOfColleague = "NameOfColleague " + i;
                reflectionOnPractice.colleaguesEmail = "colleagueemail@test.com";
                reflectionOnPractice.outcomeOfEvent = "OutcomeEvent " + i;
                reflectionOnPractice.whatLearnt = "WhatLearnt " + i;
                reflectionOnPractice.practiceChange = "PracticeChange " + i;
                reflectionOnPractice.learningNeeds = "LearningNeeds " + i;
                reflectionOnPractice.documents = new List<DocumentsModel>();
                reflectionOnPractice.documents.Add(documentModel);
                reflectionOnPracticeList.Add(reflectionOnPractice);
            }
            var body = JsonConvert.SerializeObject(reflectionOnPracticeList);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}
