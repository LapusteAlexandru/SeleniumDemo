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
    class APITests
    {
        
        RestClient apiClient = new RestClient("https://rcs-cosmetics-api-dev.azurewebsites.net");
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
            var jwt = TestBase.getJWT(TestBase.username,TestBase.password);
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
            Assert.That(actualApplicant.title.Equals(TestBase.userTitle));
            Assert.That(actualApplicant.firstName.Equals(TestBase.userFirstName));
            Assert.That(actualApplicant.lastName.Equals(TestBase.userLastName));
            Assert.That(actualApplicant.email.Equals(TestBase.username));
            Assert.That(actualApplicant.phoneNumber.Equals(TestBase.userPhone));
            Assert.That(actualApplicant.gender.Equals(gender));
            Assert.That(actualApplicant.address.Equals(TestBase.userAddress));
            Assert.That(actualApplicant.gmcNumber.Equals(TestBase.userGmcNumber));
            Assert.That(actualApplicant.gmcSpecialty.description.Equals(TestBase.userGmcSpecialty));
            Assert.That(actualApplicant.careerGrade.description.Equals(TestBase.userCareerGrade));
            Assert.That(actualApplicant.certifications[0].id.Equals(1));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        
        [Test]
        public void GetCareerGradesTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/career-grades", Method.GET);
            var jwt = TestBase.getJWT(TestBase.adminUsername,TestBase.adminPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            List<string> expectedGrades =new List<string> { "Consultant established", "Consultant newly appointed", "Associate Specialist", "Fellow", "Trainee" };
            var actualGrades = JsonConvert.DeserializeObject<List<GradesModel>>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            for (int i = 0; i < actualGrades.Count; i++)
            {
                Assert.That(actualGrades[i].description.Equals(expectedGrades[i]));
            }
        }
        [Test]
        public void GetCertificationsTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/certifications", Method.GET);
            var jwt = TestBase.getJWT(TestBase.adminUsername,TestBase.adminPassword);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            // act
            IRestResponse response = apiClient.Execute(request);
            List<string> expectedCertificates =new List<string> { "Cosmetic surgery of the face/nose/periorbital region/ears", "Cosmetic breast surgery", "Cosmetic facial contouring surgery", "Cosmetic surgery of ear", "Cosmetic Surgery of periorbital region", "Cosmetic nasal surgery", "Cosmetic surgery of the face" };
            var actualCertificates = JsonConvert.DeserializeObject<List<CertificationsModel>>(response.Content);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            for (int i = 0; i < actualCertificates.Count; i++)
            {
                Assert.That(actualCertificates[i].description.Equals(expectedCertificates[i]));
            }
        }
        [Test]
        public void GetGMCSpecialtiesTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/gmc-specialties", Method.GET);
            var jwt = TestBase.getJWT(TestBase.adminUsername,TestBase.adminPassword);
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

        [Test]
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

        [Test]
        public void GetProbityStatmentTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/probity-statements", Method.GET);
            var jwt = TestBase.getJWT(TestBase.username, TestBase.password);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            List<string> expectedProbity = new List<string> {"true","true", "I have something to declare" };
            // act
            IRestResponse response = apiClient.Execute(request);
            var actualRequests = JsonConvert.DeserializeObject<ProbityStatementsModel>(response.Content);
            // assert

            Assert.That(actualRequests.acceptProfessionalObligations.Equals(expectedProbity[0]));
            Assert.That(actualRequests.acceptAbsenceOfSuspensions.Equals(expectedProbity[1]));
            Assert.That(actualRequests.subjectOfInvestigation.Equals(expectedProbity[2]));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void PostApplicantTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/applicants", Method.POST);
            var jwt = TestBase.getJWT(TestBase.username, TestBase.password);
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
            applicantModel.email = TestBase.username;
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
        [Test]
        public void AcceptRegistrationRequestsTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/registration-requests/accept", Method.POST);
            var jwt = TestBase.getJWT(TestBase.adminUsername, TestBase.adminPassword);
            var userJWT = TestBase.getJWT(TestBase.username, TestBase.password);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            var id = TestBase.getObjectID("/api/applicants",TestBase.username,TestBase.password,userJWT);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(id);
            // act
            var response = apiClient.Execute(request);
            // assert

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public void RejectRegistrationRequestsTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/registration-requests/reject", Method.POST);
            var jwt = TestBase.getJWT(TestBase.adminUsername, TestBase.adminPassword);
            var userJWT = TestBase.getJWT(TestBase.username, TestBase.password);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            var id = TestBase.getObjectID("/api/applicants",TestBase.username,TestBase.password,userJWT);
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
        
        [Test]
        public void PutApplicantTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/applicants", Method.PUT);
            var jwt = TestBase.getJWT(TestBase.username, TestBase.password);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            GMCSPecialtiesModel specialitiesModel = new GMCSPecialtiesModel();
            GradesModel gradesModel = new GradesModel();
            CertificationsModel certificationsModel = new CertificationsModel(); 
            var id = TestBase.getObjectID("/api/applicants", TestBase.username, TestBase.password, jwt);
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
            applicantModel.email = TestBase.username;
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
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public void PostProbityTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/probity-statements", Method.POST);
            var jwt = TestBase.getJWT(TestBase.username, TestBase.password); 
            var id = TestBase.getObjectID("/api/applicants",TestBase.username, TestBase.password, jwt);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));

            ProbityStatementsModel probityStatement = new ProbityStatementsModel();
            probityStatement.id = 0;
            probityStatement.applicantId = id;
            probityStatement.acceptAbsenceOfSuspensions = true;
            probityStatement.acceptProfessionalObligations = true;
            probityStatement.subjectOfInvestigation = "I have nothing to declare";

            var body = JsonConvert.SerializeObject(probityStatement);
            request.AddJsonBody(body);
            
            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
        [Test]
        public void PutProbityTest()
        {
            // create request
            RestRequest request = new RestRequest("/api/probity-statements", Method.PUT);
            var jwt = TestBase.getJWT(TestBase.username, TestBase.password);
            var userId = TestBase.getObjectID("/api/applicants",TestBase.username, TestBase.password, jwt);
            request.AddHeader("Authorization", string.Format("Bearer {0}", jwt));
            var probityId = TestBase.getObjectID("/api/probity-statements", TestBase.username,TestBase.password,jwt);

            ProbityStatementsModel probityStatement = new ProbityStatementsModel();
            probityStatement.id = probityId;
            probityStatement.applicantId = userId;
            probityStatement.acceptAbsenceOfSuspensions = true;
            probityStatement.acceptProfessionalObligations = true;
            probityStatement.subjectOfInvestigation = "I have something to declare";

            var body = JsonConvert.SerializeObject(probityStatement);
            request.AddJsonBody(body);

            // act
            var response = apiClient.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

    }
}
