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
    [Category("APIClinicalOutcomes")]
    class ClinicalOutcomesTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test]
        public void GetClinicalOutcomesTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/clinical-outcomes", Method.GET);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            var actualRequests = JsonConvert.DeserializeObject<ClinicalOutcomesModel>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualRequests.declareFamiliarWithRequirements.Equals(true));
        }
        [Test, Order(1)]
        public void PostClinicalOutcomesTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/clinical-outcomes", Method.POST);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            DocumentsModel documentModel = new DocumentsModel();
            documentModel.fileName = "string";
            documentModel.blobStorageId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            ClinicalOutcomesModel clinicalOutcome = new ClinicalOutcomesModel();
            clinicalOutcome.id = 0;
            clinicalOutcome.declareFamiliarWithRequirements = true;
            clinicalOutcome.documents = new List<DocumentsModel>();
            clinicalOutcome.documents.Add(documentModel);
            var body = JsonConvert.SerializeObject(clinicalOutcome);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}
