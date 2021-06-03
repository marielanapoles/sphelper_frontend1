using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class SubjectCheckItems
    {
        public string Semester { get; set; }

        public string SubjectCode { get; set; }

        public string SubjectTitle { get; set; }

        public string SubjectDescription { get; set; }

        public string Result { get; set; }

        public List<string> Prerequisite_SubjectCode { get; set; } 

        public bool IsPrerequisiteSatisfied { get; set; }

        public bool IsChecked { get; set; }

        public SubjectCheckItems()
        {
            Prerequisite_SubjectCode = new List<string>();
        }
    }
}