using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class TableItemsCrn
    {
        //public List<CrnSubject> CrnSubiects { get; set; }

        public string CRN { get; set; }
        public string SubjectCode { get; set; }
        //public string SubjectDescription { get; set; }
        public string CompetencyName { get; set; }
        public string TafeCompetencyCode { get; set; }

        public TableItemsCrn()
        {
            //CrnSubiects = new List<CrnSubject>();
        }
    }
}