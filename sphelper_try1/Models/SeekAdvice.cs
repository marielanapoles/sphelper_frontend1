using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sphelper_try1.Models
{
    public class SeekAdvice
    {
        public List<SelectListItem> Lecturers { get; set; }
        public List<SelectListItem> Issues { get; set; }
        public string SendTo { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public List<lecturer> LecturerList { get; set; }

        public SeekAdvice()
        {
            Lecturers = new List<SelectListItem>();
            LecturerList = new List<lecturer>();
        }

        public SeekAdvice(List<SelectListItem> issues, List<SelectListItem> lecturers, string sendTo, string title, string body, List<lecturer> LecturerList)
        {
            Issues = issues;
            Lecturers = lecturers;
            SendTo = sendTo;
            Title = title;
            Body = body;
        }


    }
}