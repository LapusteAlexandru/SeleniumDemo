using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    class ReflectionOnPracticeModel
    {
        public int? id { get; set; }
        public int applicantId { get; set; }
        public int caseNumber { get; set; }
        public string caseDate { get; set; }
        public string hospitalSite { get; set; }
        public string locationOfEvent { get; set; }
        public string role { get; set; }
        public string procedure { get; set; }
        public string descriptionOfEvent { get; set; }
        public string nameOfColleague { get; set; }
        public string colleaguesEmail { get; set; }
        public string outcomeOfEvent { get; set; }
        public string whatLearnt { get; set; }
        public string practiceChange { get; set; }
        public string learningNeeds { get; set; }
        public List<DocumentsModel> documents { get; set; }
    }
}
