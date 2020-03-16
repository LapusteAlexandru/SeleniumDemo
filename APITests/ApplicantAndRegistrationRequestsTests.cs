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
    [Category("APIRegistrationRequests")]
    class ApplicantAndRegistrationRequestsTests
    {
        private static RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");


        [Test, Order(2)]
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
        [Test, Order(4)]
        public void AcceptRegistrationRequestsTest()
        {

            TestBase.deleteUserData("[dbo].[Users]", TestBase.apiUsername);
            submitApplicant();
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
        [Test, Order(3)]
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
        [Test, Order(1)]
        public void AuthenticationTest()
        {
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.password);
            // assert
            Assert.That(jwt.Length > 0);
        }

        [Test]
        public void GetApplicantTest()
        {
            // create request
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            RestRequest request = new RestRequest($"/api/applicants", Method.GET);
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
            Assert.That(actualApplicant.certifications[0].id.Equals(8));

        }



        [Test, Order(1)]
        public void PostApplicantTest()
        {
            TestBase.deleteUserData("[dbo].[Users]", TestBase.apiUsername);
            // create request

            // assert
            var response = submitApplicant();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        [Test, Order(5)]
        public void PutApplicantTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/applicants", Method.PUT);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            GMCSPecialtiesModel specialitiesModel = new GMCSPecialtiesModel();
            GradesModel gradesModel = new GradesModel();
            CertificationsModel certificationsModel = new CertificationsModel();
            var id = TestBase.getObjectID("/api/applicants", jwt);
            specialitiesModel.id = 1;
            gradesModel.id = 1;
            certificationsModel.id = 8;
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
            applicantModel.gmcNumber = "1231231";
            applicantModel.role = "rcs.user";
            applicantModel.gmcSpecialty = specialitiesModel;
            applicantModel.careerGrade = gradesModel;
            applicantModel.status = 2;
            applicantModel.certifications = new List<CertificationsModel>();
            applicantModel.certifications.Add(certificationsModel);

            var body = JsonConvert.SerializeObject(applicantModel);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        public static IRestResponse submitApplicant()
        {
            RestRequest request = new RestRequest("/api/applicants", Method.POST);
            var jwt = TestBase.getJWT(TestBase.apiUsername, TestBase.apiPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            GMCSPecialtiesModel specialitiesModel = new GMCSPecialtiesModel();
            GradesModel gradesModel = new GradesModel();
            CertificationsModel certificationsModel = new CertificationsModel();
            specialitiesModel.id = 1;
            gradesModel.id = 1;
            certificationsModel.id = 8;
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
            applicantModel.gmcNumber = "1231231";
            applicantModel.role = "rcs.user";
            applicantModel.gmcSpecialty = specialitiesModel;
            applicantModel.careerGrade = gradesModel;
            applicantModel.status = 1;
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
