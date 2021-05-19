using sphelper_try1.Models;
using sphelper_try1.Models.DataManager;
using sphelper_try1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace sphelper_try1.Controllers
{
    public class CRNController : Controller
    {
        SPHelperEntities db = new SPHelperEntities();

        // GET: CRN
        public ActionResult Index(CrnVM model)
        {
            var studentInMemoryCache = MemoryCache.Default["studentinfo"] as studentinfo;

            var group = model.TableItemsSelectSubject.ToList();

            //create an instance of a list of string
            List<string> selectedSubjects = new List<string>();

            //loop through each item in the group to get the check items
            foreach (var item in group)
            {
                //get the subject code of the checked items
                var query = from i in item.CheckItems
                            where i.IsChecked == true
                            select i.SubjectCode;
                query.ToList();

                //loop through the subject code and add to a list
                foreach (var q in query)
                {
                    selectedSubjects.Add(q.ToString());
                }
            }

            //get qualcode from viewmodel then call manager to get list of tafe compcode & crndetails
            var qualCode = studentInMemoryCache.QualCode;
            var tafeCompSubjCode = CrnManager.GetTafeComptencyCodeBySubjectCodeQualCode(selectedSubjects, qualCode);
            var crnDetails = CrnManager.GetCrn_Details(tafeCompSubjCode, 1, 2019);
            
            //convert crnvm to crndetailsvm
            var viewModel = new CrnDetailsVM { CrnTableItems = crnDetails };
            
            //redirect to index
            return View(viewModel);
        }

        public ActionResult SelectSubjectForCRN()
        {
            var student = MemoryCache.Default["studentinfo"] as studentinfo;

            var qualification = Student_StudyplanManager.FindQualificationByQualCode(student.QualCode);

            var query = from subj in db.subjects.ToList()
                        join sp_subj in db.studyplan_subject.ToList()
                        on subj.SubjectCode equals sp_subj.SubjectCode
                        orderby sp_subj.TimingSemesterTerm
                        where sp_subj.StudyPlanCode == student.StudyPlanCode
                        select new
                        {
                            Semester = sp_subj.TimingSemester,
                            SubjectCode = subj.SubjectCode,
                            SubjectTitle = subj.SubjectDescription,
                            SubjectDescription = subj.SubjectLongDescription,
                        };

            var semGroup = query.GroupBy(x => x.Semester).ToList();
            

            //tableitems 
            var tableItemsSelectSubject = new List<TableItemsSelectSubject>();
            
            //I made an instance f a List<TableItems> where I use foreach loop to add data to this list
            foreach (var subject in semGroup)
            {
                tableItemsSelectSubject.Add(new TableItemsSelectSubject()
                {
                    Semester = subject.First().Semester.ToString(),
                    CheckItems = subject.Select(x => new SubjectCheckItems()
                    {
                        SubjectCode = x.SubjectCode,
                        SubjectDescription = x.SubjectDescription,
                        SubjectTitle = x.SubjectTitle,
                        Semester = x.Semester.ToString(),
                        Result = Student_StudyplanManager.FindStudentGrade(student.StudentId, 2, 2019, x.SubjectCode),
                        IsChecked = false
                    }).ToList(),
                });
            };
            //ViewModel instance 
            CrnVM viewModel = new CrnVM();

            //Pass all the data I need from the objects to the viewmodel
            viewModel.Name = student.Name;
            viewModel.TableItemsSelectSubject = tableItemsSelectSubject;
            viewModel.QualCode = student.QualCode;
            
            //pass the ViewModel to the View
            return View(viewModel);
        }

        public ActionResult GeneratePDF(CrnDetailsVM Model)
        {
            List<TableItemsCrn> items = Model.CrnTableItems;

            var results = new Rotativa.PartialViewAsPdf("Index", Model);
            return results;
        }

        public ActionResult Delete(CrnDetailsVM model)
        {
            var item = model.CrnTableItems.Where(x => x.isDeleted == true).FirstOrDefault();
            model.CrnTableItems.Remove(item);

            return RedirectToAction("Index", "CRN", model);
        }

    }
}