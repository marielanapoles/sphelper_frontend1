using sphelper_try1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.ViewModel
{
    public class CrnVM
    {
        public string Name { get; set; }

        public string QualCode { get; set; }

        //get subjects
        public List<TableItemsSelectSubject> TableItemsSelectSubject { get; set; }
        
        public CrnVM()
        {
            TableItemsSelectSubject = new List<TableItemsSelectSubject>();
        }
    };
}