using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    class MenuModel
    {
        public string userRole { get; set; }
        public bool applicantExists { get; set; }
        public bool probityStatementExists { get; set; }
        public bool professionalIndemnityInsurancesExists { get; set; }
        public bool professionalBehavioursExists { get; set; }
        public bool revalidationExists { get; set; }
        public bool operativeExposureExists { get; set; }
        public bool clinicalOutcomesExists { get; set; }
        public bool referencesExist { get; set; }
        public string applicantEmail { get; set; }
        public string applicantFullName { get; set; }
        public string applicantGmcSpecialty { get; set; }
        public int applicantStatus { get; set; }
        public string applicantCareerGrade { get; set; }
    }
}
