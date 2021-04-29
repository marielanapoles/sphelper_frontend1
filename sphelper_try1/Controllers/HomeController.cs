using sphelper_try1.Models;
using sphelper_try1.Models.DataManager;
using sphelper_try1.ViewModel;
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
        private string s_id = "001061329";

        public ActionResult Index(string studentId)
        {
            studentId = s_id;
            //get student name
            var student = Student_StudyplanManager.FindStudentById(studentId);

            //get qualification, tafecode & national code
            var qualification = Student_StudyplanManager.FindQualificationByStudentId(studentId);

            //get study plan for each student
            string studyplan = Student_StudyplanManager.FindStudyplanByQualificaitonCode(qualification.QualCode);

            var viewModel = new HomeVM();
            viewModel.Id = studentId;
            viewModel.Name = student.GivenName;
            viewModel.Qualification = qualification.QualCode;
            viewModel.StudyplanCode = studyplan;

            return View(viewModel);
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
           
            return View();
        }
    }
}