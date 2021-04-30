using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class TableItemsSelectSubject
    {
        public List<SubjectCheckItems> CheckItems { get; set; }

        public string Semester { get; set; }

        public TableItemsSelectSubject()
        {
            CheckItems = new List<SubjectCheckItems>();
        }
    }
}