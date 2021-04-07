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
    public class Studyplan_SubjectController : Controller
    {
        public List<TableItems> TableItems = new List<TableItems>();

        // GET: Studyplan_Subject
        public ActionResult Index()
        {
            var studentId = "001061329";
            var student = Student_StudyplanManager.FindStudentById(studentId);
            var qualification = Student_StudyplanManager.FindQualificationByStudentId(studentId);
            var studyplan = Student_StudyplanManager.FindStudyplanByQualificaitonCode(qualification.QualCode.ToString());
            var allsubject = Student_StudyplanManager.GetAllSubjectByStudyplan(studyplan);

            //tableitems 
            var tableItems = new List<TableItems>();

            //ViewModel instance 
            Studyplan_SubjectVM viewModel = new Studyplan_SubjectVM();

            //I made an instance of a List<TableItems> where I use foreach loop to add data to this list
            foreach (var subject in allsubject)
            {
                tableItems.Add(new TableItems() {
                    TermSemester = subject.Semester.ToString(),
                    Semester = subject.TermSemester.ToString(),
                    SubjectCode = subject.SubjectCode,
                    SubjectTitle = subject.SubjectTitle,
                    SubjectDescription = subject.SubjectDescription,
                    Prerequisite = "NA", 
                    Status = "FC"
                });

                //add items to the dropdown list
                viewModel.Timing.Add(subject.Semester + " " + subject.TermSemester);
            }

            //Pass all the data I need from the objects to the viewmodel
            viewModel.StudentName = student.GivenName + " " + student.LastName;
            viewModel.Qualification = qualification.QualName;
            viewModel.NationalCode = qualification.NationalQualCode;
            viewModel.TafeCode = qualification.TafeQualCode;
            viewModel.TableItems = tableItems;

            //pass the ViewModel to the View
            return View(viewModel);
        }

        public ActionResult SelectSubject()
        {
            return View();
        }
    }
}