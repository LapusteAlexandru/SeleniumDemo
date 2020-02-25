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
    [Category("Menu")]
    class MenuTests
    {
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test]
        public void GetMenuTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/menu", Method.GET);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            var actualApplicant = JsonConvert.DeserializeObject<MenuModel>(response.Content);
            string fullName = TestBase.userTitle + " " + TestBase.userFirstName + " " + TestBase.userLastName;
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualApplicant.userRole.Equals("rcs.user"));
            Assert.That(actualApplicant.applicantExists.Equals(true));
            Assert.That(actualApplicant.probityStatementExists.Equals(true));
            Assert.That(actualApplicant.professionalIndemnityInsurancesExists.Equals(true));
            Assert.That(actualApplicant.professionalBehavioursExists.Equals(true));
            Assert.That(actualApplicant.revalidationExists.Equals(true));
            Assert.That(actualApplicant.operativeExposureExists.Equals(true));
            Assert.That(actualApplicant.clinicalOutcomesExists.Equals(true));
            Assert.That(actualApplicant.continuingProfessionalDevelopmentExists.Equals(false));
            Assert.That(actualApplicant.reflectionOnPracticeExist.Equals(true));
            Assert.That(actualApplicant.referencesExist.Equals(false));
            Assert.That(actualApplicant.applicantFullName.Equals(fullName));
            Assert.That(actualApplicant.applicantGmcSpecialty.Equals(TestBase.userGmcSpecialty));
            Assert.That(actualApplicant.applicantStatus.Equals(2));
            Assert.That(actualApplicant.applicantCareerGrade.Equals(TestBase.userCareerGrade));

        }
    }
}
