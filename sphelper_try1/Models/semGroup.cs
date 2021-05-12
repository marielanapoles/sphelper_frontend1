using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class semGroup
    {
        public List<subject> Subjects { get; set; }

        public string SubjectCode { get; set; }

        public string SubjectTitle { get; set; }

        public string SubjectDescription {get; set;}
        
        public string Status { get; set; }
        

        public semGroup()
        {
            Subjects = new List<subject>();
        }
    }


}