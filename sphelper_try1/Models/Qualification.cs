using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class Qualification
    {
        public string QualCode { get; set; }
        public string NationalQualCode { get; set; }
        public string TafeQualCode { get; set; }
        public string QualName { get; set; }
        public int TotalUnits { get; set; }
        public int CoreUnits { get; set; }
        public int ElectedUnits { get; set; }
        public int ReqListedElectedUnits { get; set; }
    }
}