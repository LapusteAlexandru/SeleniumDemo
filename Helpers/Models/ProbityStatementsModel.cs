using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    class ProbityStatementsModel
    {
        public int id { get; set; }
        public int applicantId { get; set; }
        public bool acceptProfessionalObligations { get; set; }
        public bool acceptAbsenceOfSuspensions { get; set; }
        public string subjectOfInvestigation { get; set; }
    }
}
