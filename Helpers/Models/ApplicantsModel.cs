using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    class ApplicantsModel
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string birthday { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int gender { get; set; }
        public string address { get; set; }
        public int gmcNumber { get; set; }
        public GMCSPecialtiesModel gmcSpecialty { get; set; }
        public GradesModel careerGrade { get; set; }
        public int status { get; set; }
        public List<CertificationsModel> certifications { get; set; }
    }
}
