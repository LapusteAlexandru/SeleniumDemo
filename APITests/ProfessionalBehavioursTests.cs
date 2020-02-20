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
    [Category("ProfessionalBehaviours")]
    class ProfessionalBehavioursTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test]
        public void GetProfessionalBehavioursTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/professional-behaviours", Method.GET);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            var actualRequests = JsonConvert.DeserializeObject<ProfessionalBehavioursModel>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualRequests.accept.Equals(true));
        }
        [Test, Order(1)]
        public void PostProfessionalBehavioursTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/professional-behaviours", Method.POST);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            var id = TestBase.getObjectID("/api/applicants", jwt);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            DocumentsModel documentModel = new DocumentsModel();
            documentModel.fileName = "string";
            documentModel.blobStorageId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
            ProfessionalBehavioursModel professionalBehaviour = new ProfessionalBehavioursModel();
            professionalBehaviour.id = 0;
            professionalBehaviour.applicantId = id;
            professionalBehaviour.accept = true;
            professionalBehaviour.documents = new List<DocumentsModel>();
            professionalBehaviour.documents.Add(documentModel);
            var body = JsonConvert.SerializeObject(professionalBehaviour);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
