using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class Studyplan_Subject
    {
        public Studyplan_Qualification StudyPlanCode { get; set; }
        public Subject SubjectCode { get; set; }
        public int TimingSemester { get; set; }
        public int TimingSemesterTerm { get; set; }
    }
}