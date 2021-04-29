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
        SPHelperEntities db = new SPHelperEntities();

        // GET: Studyplan_Subject
        public ActionResult Index(string studentId, string qualificationCode, string studyplanCode, string studentName)
        {
            //get qualification, tafecode & national code
            var qualification = Student_StudyplanManager.FindQualificationByQualCode(qualificationCode);
            
            //get subjects & semester group
            var query = from subj in db.subjects.ToList()
                         join sp_subj in db.studyplan_subject.ToList()
                         on subj.SubjectCode equals sp_subj.SubjectCode
                         orderby sp_subj.TimingSemesterTerm
                         where sp_subj.StudyPlanCode == studyplanCode
                         select new
                         {
                             Semester = sp_subj.TimingSemester,
                             SubjectCode = subj.SubjectCode,
                             SubjectTitle = subj.SubjectDescription,
                             SubjectDescription = subj.SubjectLongDescription,
                         };
            var semGroup = query.GroupBy(x => x.Semester).ToList();

            //tableitems 
            var tableItems = new List<TableItems>();
            
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
                        SubjectDescription = x.SubjectDescription
                    }).ToList()
                });
            };

            //ViewModel instance 
            Studyplan_SubjectVM viewModel = new Studyplan_SubjectVM();

            //Pass all the data I need from the objects to the viewmodel
            viewModel.StudentName = studentName;
            viewModel.Qualification = qualification.QualName;
            viewModel.NationalCode = qualification.NationalQualCode;
            viewModel.TafeCode = qualification.TafeQualCode;
            viewModel.TableItems = tableItems;

            //pass the ViewModel to the View
            return View(viewModel);
        }

        //public ActionResult SelectSubject(string studentId, string qualificationCode, string studyplanCode, string studentName)
        //{
        //    var qualification = Student_StudyplanManager.FindQualificationByQualCode(qualificationCode);
        //    var query = from subj in db.subjects.ToList()
        //                join sp_subj in db.studyplan_subject.ToList()
        //                on subj.SubjectCode equals sp_subj.SubjectCode
        //                orderby sp_subj.TimingSemesterTerm
        //                where sp_subj.StudyPlanCode == studyplanCode
        //                select new
        //                {
        //                    Semester = sp_subj.TimingSemester,
        //                    SubjectCode = subj.SubjectCode,
        //                    SubjectTitle = subj.SubjectDescription,
        //                    SubjectDescription = subj.SubjectLongDescription,
        //                };
        //    var semGroup = query.GroupBy(x => x.Semester).ToList();


        //    //tableitems 
        //    var subjectCheckItems = new List<SubjectCheckItems>();
        //    //ViewModel instance 
        //    Studyplan_SubjectVM viewModel = new Studyplan_SubjectVM();
        //    //I made an instance f a List<TableItems> where I use foreach loop to add data to this list
        //    foreach (var subject in semGroup)
        //    {
        //        subjectCheckItems.Add(new SubjectCheckItems()
        //        {
        //            Semester = subject.First().Semester.ToString(),
        //            SUb = subject.Select(x => new semGroup
        //            {
        //                SubjectCode = x.SubjectCode,
        //                SubjectTitle = x.SubjectTitle,
        //                SubjectDescription = x.SubjectDescription
        //            })
        //            .ToList(),
        //            IsChecked = false
        //        });
        //        viewModel.Timing.Add(subject.First().Semester.ToString());
                
        //    };

        //    //Pass all the data I need from the objects to the viewmodel
        //    viewModel.StudentName = studentName;
        //    viewModel.Qualification = qualification.QualName;
        //    viewModel.NationalCode = qualification.NationalQualCode;
        //    viewModel.TafeCode = qualification.TafeQualCode;
        //    viewModel.SubjectCheckItems = subjectCheckItems;

        //    //pass the ViewModel to the View
        //    return View(viewModel);
        //}
    }
}