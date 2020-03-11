using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    class ProfessionalIndemnityInsuranceModel
    {
        public int id { get; set; }
        public bool hasIndemnityArrangements { get; set; }
        public bool isPracticeDisclosed { get; set; }
        public bool isPracticeOverseas { get; set; }
        public List<DocumentsModel> documents { get; set; }
    }
}
