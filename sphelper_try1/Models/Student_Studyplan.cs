using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class Student_Studyplan
    {
        public Student Student { get; set; }
        public Qualification QualCode { get; set; }
        public Term_DateTime TermCodeStart { get; set; }
        public Term_DateTime TermYearStart { get; set; }
        public string EnrollementType { get; set; }
    }
}