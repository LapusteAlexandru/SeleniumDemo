using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    class ProfessionalBehavioursModel
    {
        public int id { get; set; }
        public bool accept { get; set; }
        public List<DocumentsModel> documents { get; set; }
    }
}
