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
    [Category("GMCSpeciality")]
    class GMCSpecialtyTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test]
        public void GetGMCSpecialtiesTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/gmc-specialties", Method.GET);
            var jwt = TestBase.getJWT(TestBase.adminUsername, TestBase.adminPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            List<string> expectedSpecialties = new List<string> { "Vascular Surgery", "Cardiothoracic Surgery",
                "General Surgery", "Neurosurgery", "Oral and Maxillofacial surgery", "Otolaryngology", "Plastic Surgery"
            ,"Paediatric Surgery","Trauma and Orthopaedic Surgery","Urology","Ophthalmology"};

            var actualSpecialties = JsonConvert.DeserializeObject<List<GMCSPecialtiesModel>>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            for (int i = 0; i < actualSpecialties.Count; i++)
            {
                Assert.That(actualSpecialties[i].description.Equals(expectedSpecialties[i]));
            }
        }
    }
}
