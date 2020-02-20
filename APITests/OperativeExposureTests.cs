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
    [Category("OperativeExposure")]
    class OperativeExposureTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test]
        public void GetOperativeExposure()
        {
            // create request
            RestRequest request = new RestRequest("/api/operative-exposure", Method.GET);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            var actualRequests = JsonConvert.DeserializeObject<OperativeExposureModel>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualRequests.declareEnvironmentMeetsStandards.Equals(true));
            Assert.That(actualRequests.declareProceduresAreTrue.Equals(true));
        }
        [Test, Order(1)]
        public void PostOperativeExposure()
        {
            // create request
            RestRequest request = new RestRequest("/api/operative-exposure", Method.POST);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            var id = TestBase.getObjectID("/api/applicants", jwt);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            DocumentsModel documentModel = new DocumentsModel();
            documentModel.fileName = "string";
            documentModel.blobStorageId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            OperativeExposureModel operativeExposure = new OperativeExposureModel();
            operativeExposure.id = 0;
            operativeExposure.applicantId = id;
            operativeExposure.declareEnvironmentMeetsStandards = true;
            operativeExposure.declareProceduresAreTrue = true;
            operativeExposure.documents = new List<DocumentsModel>();
            operativeExposure.documents.Add(documentModel);
            var body = JsonConvert.SerializeObject(operativeExposure);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}
