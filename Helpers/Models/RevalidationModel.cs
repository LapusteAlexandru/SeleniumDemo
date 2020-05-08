using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    class RevalidationModel
    {
        public int id { get; set; }
        public int applicationId { get; set; }
        public bool declareAppraisal { get; set; }
        public string mostRecentRevalidation { get; set; }
        public string nextRevalidation { get; set; }
        public List<DocumentsModel> gmcLetters { get; set; }
        public List<DocumentsModel> annualAppraisals { get; set; }
    }
}
