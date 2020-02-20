using Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RCoS;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace APITests
{
    [TestFixture]
    [Category("APIApplicant")]
    class ApplicantTests
    {
        
        static RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
        [Test, Order(1)]
        public void AuthenticationTest()
        {
            var jwt = TestBase.getJWT(TestBase.username,TestBase.password);
            // assert
            Assert.That(jwt.Length>0);
        }
        
        [Test]
        public void GetApplicantTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/applicants", Method.GET);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            var actualApplicant = JsonConvert.DeserializeObject<ApplicantsModel>(response.Content);
            int gender;
            if (TestBase.userGender.Equals("Male"))
                gender = 1;
            else
                gender = 2;
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(actualApplicant.title.Equals(TestBase.userTitle));
            Assert.That(actualApplicant.firstName.Equals(TestBase.userFirstName));
            Assert.That(actualApplicant.lastName.Equals(TestBase.userLastName));
            Assert.That(actualApplicant.email.Equals(TestBase.apiUsername));
            Assert.That(actualApplicant.phoneNumber.Equals(TestBase.userPhone));
            Assert.That(actualApplicant.gender.Equals(gender));
            Assert.That(actualApplicant.address.Equals(TestBase.userAddress));
            Assert.That(actualApplicant.gmcNumber.Equals(TestBase.userGmcNumber));
            Assert.That(actualApplicant.gmcSpecialty.description.Equals(TestBase.userGmcSpecialty));
            Assert.That(actualApplicant.careerGrade.description.Equals(TestBase.userCareerGrade));
            Assert.That(actualApplicant.certifications[0].id.Equals(1));

        }
        
        

        [Test, Order(1)]
        public void PostApplicantTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/applicants", Method.POST);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            GMCSPecialtiesModel specialitiesModel = new GMCSPecialtiesModel();
            GradesModel gradesModel = new GradesModel();
            CertificationsModel certificationsModel = new CertificationsModel();
            specialitiesModel.id = 1;
            gradesModel.id = 1;
            certificationsModel.id = 1;
            int gen;
            if (TestBase.userGender.Equals("Male"))
                gen = 1;
            else
                gen = 2;
            ApplicantsModel applicantModel = new ApplicantsModel();
            applicantModel.id = 0;
            applicantModel.title = TestBase.userTitle;
            applicantModel.firstName = TestBase.userFirstName;
            applicantModel.lastName = TestBase.userLastName;
            applicantModel.birthday = "2020-02-10T09:43:25.179Z";
            applicantModel.email = TestBase.apiUsername;
            applicantModel.phoneNumber = TestBase.userPhone;
            applicantModel.gender = gen;
            applicantModel.address = TestBase.userAddress;
            applicantModel.gmcNumber = 1231231;
            applicantModel.gmcSpecialty = specialitiesModel;
            applicantModel.careerGrade = gradesModel;
            applicantModel.status = 3;
            applicantModel.certifications = new List<CertificationsModel>();
            applicantModel.certifications.Add(certificationsModel);

            var body = JsonConvert.SerializeObject(applicantModel);
            request.AddJsonBody(body);
            
            // act
            var response = apiClient.Execute(request);
            // assert

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        
        [Test, Order(2)]
        public void PutApplicantTest()
        {
            // create request
            var response = UpdateApplicant();
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        public static IRestResponse UpdateApplicant()
        {
            RestRequest request = new RestRequest("/api/applicants", Method.PUT);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            GMCSPecialtiesModel specialitiesModel = new GMCSPecialtiesModel();
            GradesModel gradesModel = new GradesModel();
            CertificationsModel certificationsModel = new CertificationsModel();
            var id = TestBase.getObjectID("/api/applicants", jwt);
            specialitiesModel.id = 1;
            gradesModel.id = 1;
            certificationsModel.id = 1;
            int gen;
            if (TestBase.userGender.Equals("Male"))
                gen = 1;
            else
                gen = 2;
            ApplicantsModel applicantModel = new ApplicantsModel();
            applicantModel.id = id;
            applicantModel.title = TestBase.userTitle;
            applicantModel.firstName = TestBase.userFirstName;
            applicantModel.lastName = TestBase.userLastName;
            applicantModel.birthday = "2020-02-10T09:43:25.179Z";
            applicantModel.email = TestBase.apiUsername;
            applicantModel.phoneNumber = TestBase.userPhone;
            applicantModel.gender = gen;
            applicantModel.address = TestBase.userAddress;
            applicantModel.gmcNumber = 1231231;
            applicantModel.gmcSpecialty = specialitiesModel;
            applicantModel.careerGrade = gradesModel;
            applicantModel.status = 2;
            applicantModel.certifications = new List<CertificationsModel>();
            applicantModel.certifications.Add(certificationsModel);

            var body = JsonConvert.SerializeObject(applicantModel);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            return response;
        }
    }
}
