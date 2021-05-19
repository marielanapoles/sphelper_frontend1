using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sphelper_try1.Models
{
    public class SeekAdvice
    {
        public List<string> Lecturers { get; set; }
        public string SendTo { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public SeekAdvice()
        {
            Lecturers = new List<string>();
        }


    }
}