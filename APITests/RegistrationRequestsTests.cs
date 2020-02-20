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
    [Category("RegistrationRequests")]
    class RegistrationRequestsTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");



        [Test, Order(1)]
        public void GetRegistrationRequestsTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/registration-requests", Method.GET);
            var jwt = TestBase.getJWT(TestBase.adminUsername, TestBase.adminPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            var actualRequests = JsonConvert.DeserializeObject<List<RegistrationRequestsModel>>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualRequests.Count > 0);
        }
        [Test, Order(3)]
        public void AcceptRegistrationRequestsTest()
        {

            ApplicantTests.UpdateApplicant();
            // create request
            RestRequest request = new RestRequest("/api/registration-requests/accept", Method.POST);
            var jwt = TestBase.getJWT(TestBase.adminUsername, TestBase.adminPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            var id = TestBase.getObjectID("/api/registration-requests", jwt);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(id);
            // act
            var response = apiClient.Execute(request);
            // assert

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test, Order(2)]
        public void RejectRegistrationRequestsTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/registration-requests/reject", Method.POST);
            var jwt = TestBase.getJWT(TestBase.adminUsername, TestBase.adminPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            var id = TestBase.getObjectID("/api/registration-requests", jwt);
            RejectRequestModel rejectModel = new RejectRequestModel();
            rejectModel.id = id;
            rejectModel.comment = "Test";

            var body = JsonConvert.SerializeObject(rejectModel);
            request.AddJsonBody(body);
            // act
            var response = apiClient.Execute(request);
            // assert

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
