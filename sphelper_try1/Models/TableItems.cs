using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class TableItems
    {
        //public string TermSemester { get; set; }
        public List<semGroup> Subjects { get; set; }

        public string Semester { get; set; }

        public TableItems()
        {
            Subjects = new List<semGroup>();
        }
    }
}