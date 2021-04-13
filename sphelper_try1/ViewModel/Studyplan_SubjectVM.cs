using sphelper_try1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.ViewModel
{
    public class Studyplan_SubjectVM
    {
        //get Student Name
        public string StudentName {get; set;}

        //get Qualification
        public string Qualification { get; set; }

        //get nationalCode
        public string NationalCode { get; set; }

        //get tafecode
        public string TafeCode { get; set; }


        //get semester and term timing 
        public List<string> Timing { get; set; }

        //get subjects
        public List<TableItems> TableItems { get; set; }

        public List<SubjectCheckItems> SubjectCheckItems { get; set; }

        public Studyplan_SubjectVM()
        {
            TableItems = new List<TableItems>();
            SubjectCheckItems = new List<SubjectCheckItems>();
            Timing = new List<string>();
        }
    }
}