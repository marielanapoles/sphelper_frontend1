using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class studentinfo
    {
        public string StudentId { get; set; }
        public string Name { get; set; }

        public qualification Qualification { get; set; }

        public string StudyPlanCode { get; set; }

        public List<StudyPlanSubjects> StudyPlanSubjects { get; set; }

        public SemesterNow SemesterNow { get; set; }

        public List<SubjectResults> SubjectResultsList {get; set;}

        public studentinfo()
        {
            SubjectResultsList = new List<SubjectResults>();
            StudyPlanSubjects = new List<StudyPlanSubjects>();
        }
    }
}