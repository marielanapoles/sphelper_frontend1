using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class SubjectCheckItems
    {
        public string Semester { get; set; }
        //added
        public string SubjectCode { get; set; }
        public string SubjectTitle { get; set; }
        public string SubjectDescription { get; set; }

        public bool IsChecked { get; set; }
    }
}