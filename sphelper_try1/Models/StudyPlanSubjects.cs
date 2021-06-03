using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class StudyPlanSubjects
    {
        public int Semester { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectTitle { get; set; }
        public string SubjectDescription { get; set; }
    }
}