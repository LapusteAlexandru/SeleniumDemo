using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    class RegistrationRequestsModel
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public string createdAt { get; set; }
        public string modifiedBy { get; set; }
        public string birthday { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string gmcNumber { get; set; }
        public string gmcSpecialty { get; set; }
        public string careerGrade { get; set; }
        public List<string> certifications { get; set; }

    }
}
