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
    [Category("APIProfessionalInsurance")]
    class ProfessionalIndemnityInsuranceTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test]
        public void GetProfessionalIndemnityInsuranceTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/professional-indemnity-insurance", Method.GET);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            var actualRequests = JsonConvert.DeserializeObject<ProfessionalIndemnityInsuranceModel>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualRequests.hasIndemnityArrangements.Equals(true));
            Assert.That(actualRequests.isPracticeDisclosed.Equals(true));
            Assert.That(actualRequests.isPracticeOverseas.Equals(false));
        }
        [Test, Order(1)]
        public void PostProfessionalIndemnityInsuranceTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/professional-indemnity-insurance", Method.POST);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            DocumentsModel documentModel = new DocumentsModel();
            documentModel.fileName = "string";
            documentModel.blobStorageId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            ProfessionalIndemnityInsuranceModel professionalInsurance = new ProfessionalIndemnityInsuranceModel();
            professionalInsurance.id = 0;
            professionalInsurance.hasIndemnityArrangements = true;
            professionalInsurance.isPracticeDisclosed = true;
            professionalInsurance.isPracticeOverseas = true;
            professionalInsurance.documents = new List<DocumentsModel>();
            professionalInsurance.documents.Add(documentModel);
            var body = JsonConvert.SerializeObject(professionalInsurance);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        [Test, Order(2)]
        public void PutProfessionalIndemnityInsuranceTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/professional-indemnity-insurance", Method.PUT);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            var insuranceId = TestBase.getObjectID("/api/professional-indemnity-insurance", jwt);
            DocumentsModel documentModel = new DocumentsModel();
            documentModel.fileName = "string";
            documentModel.blobStorageId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            ProfessionalIndemnityInsuranceModel professionalInsurance = new ProfessionalIndemnityInsuranceModel();
            professionalInsurance.id = insuranceId;
            professionalInsurance.hasIndemnityArrangements = true;
            professionalInsurance.isPracticeDisclosed = true;
            professionalInsurance.isPracticeOverseas = false;
            professionalInsurance.documents = new List<DocumentsModel>();
            professionalInsurance.documents.Add(documentModel);
            var body = JsonConvert.SerializeObject(professionalInsurance);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
