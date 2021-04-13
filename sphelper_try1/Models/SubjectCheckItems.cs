using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class SubjectCheckItems
    {
        public string Semester { get; set; }
        public List<semGroup> Subjects { get; set; }
        public bool IsChecked { get; set; }

        public SubjectCheckItems()
        {
            Subjects = new List<semGroup>();
        }


    }
}