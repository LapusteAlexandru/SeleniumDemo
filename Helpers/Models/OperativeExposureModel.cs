using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    class OperativeExposureModel
    {
        public int id { get; set; }
        public int applicationId { get; set; }
        public bool declareProceduresAreTrue { get; set; }
        public bool declareEnvironmentMeetsStandards { get; set; }
        public List<DocumentsModel> documents { get; set; }
    }
}
