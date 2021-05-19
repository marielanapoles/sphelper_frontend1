using sphelper_try1.Models;
using sphelper_try1.Models.DataManager;
using sphelper_try1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;

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
            var studentName = Student_StudyplanManager.FindstudentName(studentId);

            //get qualification, tafecode & national code
            var qualification = Student_StudyplanManager.FindQualificationByStudentId(studentId);

            //get study plan for each student
            string studyPlanCode = Student_StudyplanManager.FindStudyplanByQualificaitonCode(qualification.QualCode);

            var student = new studentinfo();
            student.StudentId = studentId;
            student.Name = studentName;
            student.QualCode = qualification.QualCode;
            student.StudyPlanCode = studyPlanCode;

            var viewModel = new HomeVM()
            {
                StudentInfo = student
            };

            //memory cache
            var date = DateTime.Now;
            var expirydate = date.AddDays(1);
            MemoryCache.Default.Add("studentinfo", student, expirydate);

            return View(viewModel);
        }

        public ActionResult SeekAdvice()
        {
            return View();
        }
    }
}