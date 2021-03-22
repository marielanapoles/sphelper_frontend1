using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class Studyplan_Qualification
    {
        public string StudyPlanCode { get; set; }
        public Qualification QualCode { get; set; }
        public int Priority { get; set; }
    }
}