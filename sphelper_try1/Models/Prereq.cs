using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class Prereq
    {
        public string CombinationLegend { get; set; }
        public List<string> Prereq_SubjectCode { get; set; }

        public Prereq()
        {
            Prereq_SubjectCode = new List<string>();
        }

    }
}