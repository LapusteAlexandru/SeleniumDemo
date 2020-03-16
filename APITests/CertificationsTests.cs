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
    [Category("Certifications")]
    class CertificationsTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test]
        public void GetCertificationsTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/certifications", Method.GET);
            var jwt = TestBase.getJWT(TestBase.adminUsername, TestBase.adminPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            List<string> expectedCertificates = new List<string> { "Cosmetic breast surgery", "Cosmetic nasal surgery", "Cosmetic Surgery of the periorbital region", "Cosmetic surgery of the ear", "Cosmetic facial contouring surgery", "Cosmetic surgery of the face", "Cosmetic surgery of the face/nose/periorbital region/ears", "Cosmetic body contouring surgery", "Supplementary certificate in body contouring following massive weight loss", "Cosmetic Surgery" };
            var actualCertificates = JsonConvert.DeserializeObject<List<CertificationsModel>>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            for (int i = 0; i < actualCertificates.Count; i++)
            {
                Assert.That(actualCertificates[i].description.Equals(expectedCertificates[i]));
            }
        }
    }
}
