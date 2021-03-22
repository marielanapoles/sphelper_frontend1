using sphelper_try1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sphelper_try1.Controllers
{
    public class HomeController : Controller
    {
        private string[] Lecturers = { "IT Studies", "Kym Bond", "KT Lau" };
        public ActionResult Index()
        {
            var student1 = new Student()
            {
                StudentId = "001061329",
                GivenName = "Mariela",
                LastName = "Napoles",
                EmailAddress = "mariela.napoles@student.tafesa.edu.au"
            };

            return View(student1);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SeekAdvice()
        {
            var seekadvice = new SeekAdvice()
            {
                Lecturers = Lecturers.ToList()
            };
            return View( seekadvice);
        }
    }
}