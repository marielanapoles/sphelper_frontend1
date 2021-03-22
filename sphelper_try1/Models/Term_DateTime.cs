using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class Term_DateTime
    {
        public int TermCode { get; set; }
        public int TermYear { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}