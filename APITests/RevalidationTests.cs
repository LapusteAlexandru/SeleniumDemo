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
    [Category("APIRevalidation")]
    class RevalidationTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test]
        public void GetRevalidationTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/revalidation", Method.GET);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            var actualRequests = JsonConvert.DeserializeObject<RevalidationModel>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualRequests.declareAppraisal.Equals(false));
        }
        [Test,Order(1)]
        public void PostRevalidationTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/revalidation", Method.POST);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            DocumentsModel documentModel = new DocumentsModel();
            documentModel.fileName = "string";
            documentModel.blobStorageId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            RevalidationModel revalidationModel = new RevalidationModel();
            revalidationModel.id = 0;
            revalidationModel.declareAppraisal = true;
            revalidationModel.gmcLetters = new List<DocumentsModel>();
            revalidationModel.gmcLetters.Add(documentModel);
            revalidationModel.annualAppraisals = new List<DocumentsModel>();
            revalidationModel.annualAppraisals.Add(documentModel);
            var body = JsonConvert.SerializeObject(revalidationModel);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        [Test,Order(2)]
        public void PutRevalidationTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/revalidation", Method.PUT);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            var insuranceId = TestBase.getObjectID("/api/revalidation", jwt);
            DocumentsModel documentModel = new DocumentsModel();
            documentModel.fileName = "string";
            documentModel.blobStorageId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            RevalidationModel revalidationModel = new RevalidationModel();
            revalidationModel.id = insuranceId;
            revalidationModel.declareAppraisal = false;
            revalidationModel.gmcLetters = new List<DocumentsModel>();
            revalidationModel.gmcLetters.Add(documentModel);
            revalidationModel.annualAppraisals = new List<DocumentsModel>();
            revalidationModel.annualAppraisals.Add(documentModel);
            var body = JsonConvert.SerializeObject(revalidationModel);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
