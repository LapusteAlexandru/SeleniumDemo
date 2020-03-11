using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
        class ClinicalOutcomesModel
        {
            public int id { get; set; }
            public bool declareFamiliarWithRequirements { get; set; }
            public List<DocumentsModel> documents { get; set; }
        }
}
