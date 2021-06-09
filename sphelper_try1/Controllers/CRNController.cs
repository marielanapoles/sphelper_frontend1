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

        //[HttpGet]
        // GET: CRN
        public ActionResult Index(CrnDetailsVM model)
        {
            if (Session["StudentID"] != null)
            {

                if (model.CrnTableItems.Count != 0)
                {
                    var viewModel = new CrnDetailsVM() { CrnTableItems = model.CrnTableItems };
                    return View(viewModel);

                }
                else
                {
                    var dataInMemory = MemoryCache.Default["mycrns"] as CrnDetailsVM;
                    if (dataInMemory != null)
                    {
                        return View(dataInMemory);
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult SavedCRNs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SelectSubjectForCRN()
        {
            if (Session["StudentID"] != null)
            {
                var studentInfo = MemoryCache.Default["studentinfo"] as studentinfo;
                var studyplansubject = studentInfo.StudyPlanSubjects;

                var semGroup = studyplansubject.GroupBy(x => x.Semester).ToList();

                //tableitems 
                var tableItemsSelectSubject = new List<TableItemsSelectSubject>();

                //I made an instance f a List<TableItems> where I use foreach loop to add data to this list
                foreach (var group in semGroup)
                {
                    tableItemsSelectSubject.Add(new TableItemsSelectSubject()
                    {
                        Semester = group.First().Semester.ToString(),
                        CheckItems = group.Select(x => new SubjectCheckItems()
                        {
                            SubjectCode = x.SubjectCode,
                            SubjectDescription = x.SubjectDescription,
                            SubjectTitle = x.SubjectTitle,
                            Semester = x.Semester.ToString(),
                            Result = Student_StudyplanManager.FindSubjectGrade(x.SubjectCode), //can be stored in student info
                            Prerequisite_SubjectCode = Student_StudyplanManager.FindPrerequisite(x.SubjectCode), //for presentation only
                            IsPrerequisiteSatisfied = Student_StudyplanManager.IsPrerequisiteCompleted(x.SubjectCode), //logic
                            IsChecked = false
                        }
                        ).ToList(),
                    });
                };

                //ViewModel instance 
                CrnVM viewModel = new CrnVM();

                //Pass all the data I need from the objects to the viewmodel
                viewModel.Name = studentInfo.Name;
                viewModel.TableItemsSelectSubject = tableItemsSelectSubject;
                viewModel.QualCode = studentInfo.Qualification.QualCode;

                //pass the ViewModel to the View
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }


        [HttpPost]
        public ActionResult GeneratePDF(CrnDetailsVM Model)
        {
            var items = Model.CrnTableItems.Where(x => x.isDeleted == false && x.CRN != "N/A").ToList();
            var crnDetailsModel = new CrnDetailsVM()
            {
                CrnTableItems = items
            };
            var results = new Rotativa.PartialViewAsPdf("Index", crnDetailsModel);



            //save last item to memory 
            var dateNow = DateTime.Now;
            var expriryDate = dateNow.AddDays(14);
            //MemoryCache.Default.Add("mycrns", crnDetailsModel , expriryDate);
            MemoryCache.Default.Remove("mycrns");
            MemoryCache.Default.Set("mycrns", crnDetailsModel, expriryDate);

            return results;
        }


        public ActionResult GetCRNs(CrnVM model)
        {
            var studentInMemoryCache = MemoryCache.Default["studentinfo"] as studentinfo;
            var semNow = studentInMemoryCache.SemesterNow.Semester;
            var yearNow = studentInMemoryCache.SemesterNow.Year;

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
            var qualCode = studentInMemoryCache.Qualification;
            var tafeCompSubjCode = CrnManager.GetTafeComptencyCodeBySubjectCodeQualCode(selectedSubjects, qualCode.QualCode);
            var crnDetails = CrnManager.GetCrn_Details(tafeCompSubjCode, semNow, yearNow);

            var viewModel = new CrnDetailsVM() { CrnTableItems = crnDetails };

            //save last item to memory 
            var dateNow = DateTime.Now;
            //var expriryDate = dateNow.AddDays(14);
            var expriryDate = dateNow.AddMinutes(2);
            MemoryCache.Default.Add("mycrns", viewModel, expriryDate);

            return View("Index", viewModel);
        }

        public ActionResult Error()
        {
            return View();
        }


    }
}