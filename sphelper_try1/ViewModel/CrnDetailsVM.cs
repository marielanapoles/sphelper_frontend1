using sphelper_try1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.ViewModel
{
    public class CrnDetailsVM
    {
        public List<CrnSubject> CrnSubjects { get; set; }

        public List<TableItemsCrn> CrnTableItems { get; set; }


        public CrnDetailsVM()
        {
            CrnSubjects = new List<CrnSubject>();
            CrnTableItems = new List<TableItemsCrn>();

        }
    }
}