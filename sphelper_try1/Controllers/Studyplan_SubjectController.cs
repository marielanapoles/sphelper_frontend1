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
    public class Studyplan_SubjectController : Controller
    {
        SPHelperEntities db = new SPHelperEntities();

        // GET: Studyplan_Subject
        //public ActionResult Index(string studentId, string qualificationCode, string studyplanCode, string studentName)
        public ActionResult Index()
        {
            //get student info from the memory
            var studentInfo = MemoryCache.Default["studentinfo"] as studentinfo;

            //get stubject for each studyplan
            var studyplansubject = studentInfo.StudyPlanSubjects;

            //group the subjects by semester
            var semGroup = studyplansubject.GroupBy(x => x.Semester).ToList();
            
            //create an instance of table items for my displa template
            var tableItems = new List<TableItems>();

            //ViewModel instance 
            Studyplan_SubjectVM viewModel = new Studyplan_SubjectVM();

            //I made an instance of a List<TableItems> where I use foreach loop to add data to this list
            foreach (var subject in semGroup)
            {
                tableItems.Add(new TableItems()
                {
                    Semester = subject.First().Semester.ToString(),
                    Subjects = subject.Select(x => new semGroup
                    {
                        SubjectCode = x.SubjectCode,
                        SubjectTitle = x.SubjectTitle,
                        SubjectDescription = x.SubjectDescription,
                        Status = Student_StudyplanManager.FindSubjectGrade(x.SubjectCode)
                    }).ToList(),
                });

                //get number of semester for each student and that I will need to add to a dropdown list later
                viewModel.Timing.Add("Semester: " + subject.First().Semester.ToString());
            };

            //Pass all the data I need to the viewmodel
            viewModel.StudentName = studentInfo.Name;
            viewModel.Qualification = studentInfo.Qualification.QualName;
            viewModel.NationalCode = studentInfo.Qualification.NationalQualCode;
            viewModel.TafeCode = studentInfo.Qualification.TafeQualCode;
            viewModel.TableItems = tableItems;

            //pass the ViewModel to the View
            return View(viewModel);
        }
    }
}