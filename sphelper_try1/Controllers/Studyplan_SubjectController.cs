using sphelper_try1.Models;
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
            //student
            var student1 = new Student() {
                StudentId = "001061329", 
                GivenName = "Mariela",
                LastName = "Napoles",
                EmailAddress = "mariela.napoles@student.tafesa.edu.au"
            };
            var student2 = new Student()
            {
                StudentId = "001100853",
                GivenName = "Aru",
                LastName = "Lee",
                EmailAddress = "aru.lee@student.tafesa.edu.au"
            };

            //qualification
            var qualification = new Qualification()
            {
                QualCode = "C3_IDM15",
                NationalQualCode = "ICT30115",
                TafeQualCode = "TP00736",
                QualName = "Certificate III in Information, Digital Media and Technology",
                TotalUnits = 17,
                CoreUnits = 6,
                ElectedUnits = 2,
                ReqListedElectedUnits = 9
            };

            //term_datetime
            var term_dateTime1 = new Term_DateTime()
            {
                TermCode = 2019,
                TermYear = 1, 
                StartDate = new DateTime(2019, 02, 12),
                EndDate = new DateTime(2019, 04, 05)
            };
            var term_dateTime2 = new Term_DateTime()
            {
                TermCode = 2019,
                TermYear = 2, 
                StartDate = new DateTime(2019, 04, 08),
                EndDate = new DateTime(2019, 06, 14)
            };
            var term_dateTime3 = new Term_DateTime()
            {
                TermCode = 2019,
                TermYear = 3, 
                StartDate = new DateTime(2019, 07, 29),
                EndDate = new DateTime(2019, 09, 20)
            };
            var term_dateTime4 = new Term_DateTime()
            {
                TermCode = 2019,
                TermYear = 4, 
                StartDate = new DateTime(2019, 10, 14),
                EndDate = new DateTime(2019, 12, 06)
            };

            //student_studyplan
            var student_studyplan = new Student_Studyplan()
            {
                Student = student1,
                QualCode = qualification,
                TermCodeStart = term_dateTime1,
                TermYearStart = term_dateTime1, 
                EnrollementType = "WR"
            };

            //subjects
            var subject1 = new Subject()
            {
                SubjectCode = "4HTML",
                SubjectTitle = "Praesent porttitor ligula eu pharetra",
                SubjectDescription = "Fusce posuere, metus vitae gravida mattis, nulla urna ultricies purus, nec porta arcu augue quis massa. In a felis commodo, ullamcorper eros a, eleifend quam. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus",
                
            };
            var subject2 = new Subject()
            {
                SubjectCode = "3PRB",
                SubjectTitle = "Praesent porttitor ligula eu pharetra",
                SubjectDescription = "Fusce posuere, metus vitae gravida mattis, nulla urna ultricies purus, nec porta arcu augue quis massa. In a felis commodo, ullamcorper eros a, eleifend quam. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus",

            };
            var subject3 = new Subject()
            {
                SubjectCode = "4BUI",
                SubjectTitle = "Praesent porttitor ligula eu pharetra",
                SubjectDescription = "Fusce posuere, metus vitae gravida mattis, nulla urna ultricies purus, nec porta arcu augue quis massa. In a felis commodo, ullamcorper eros a, eleifend quam. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus",

            };
            var subject4 = new Subject()
            {
                SubjectCode = "4JAB",
                SubjectTitle = "Praesent porttitor ligula eu pharetra",
                SubjectDescription = "Fusce posuere, metus vitae gravida mattis, nulla urna ultricies purus, nec porta arcu augue quis massa. In a felis commodo, ullamcorper eros a, eleifend quam. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus",
            };
            var subject5 = new Subject()
            {
                SubjectCode = "5TST",
                SubjectTitle = "Praesent porttitor ligula eu pharetra",
                SubjectDescription = "Fusce posuere, metus vitae gravida mattis, nulla urna ultricies purus, nec porta arcu augue quis massa. In a felis commodo, ullamcorper eros a, eleifend quam. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus"

            };
            var subject6 = new Subject()
            {
                SubjectCode = "5DD",
                SubjectTitle = "Praesent porttitor ligula eu pharetra",
                SubjectDescription = "Fusce posuere, metus vitae gravida mattis, nulla urna ultricies purus, nec porta arcu augue quis massa. In a felis commodo, ullamcorper eros a, eleifend quam. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus"
            };

            //qualification:1  studyplan code:2
            var studyplan_qualification1 = new Studyplan_Qualification()
            {
                //dummydata 
                StudyPlanCode = "R001",
                QualCode = qualification,
                Priority = 1
            };
            var studyplan_qualification2 = new Studyplan_Qualification()
            {
                //dummydata 
                StudyPlanCode = "R002",
                QualCode = qualification,
                Priority = 1
            };

            //studyplan_subject code: r001 subject: 6
            var studyplan1_subject1 = new Studyplan_Subject()
            {
                StudyPlanCode = studyplan_qualification1,
                SubjectCode = subject1,
                TimingSemesterTerm = 1,
                TimingSemester = 1
            };


            var studyplan1_subject2 = new Studyplan_Subject()
            {
                StudyPlanCode = studyplan_qualification1,
                SubjectCode = subject2,
                TimingSemesterTerm = 1,
                TimingSemester = 1
            };
            var studyplan1_subject3 = new Studyplan_Subject()
            {
                StudyPlanCode = studyplan_qualification1,
                SubjectCode = subject3,
                TimingSemesterTerm = 1,
                TimingSemester = 1
            };
            var studyplan1_subject4 = new Studyplan_Subject()
            {
                StudyPlanCode = studyplan_qualification1,
                SubjectCode = subject4,
                TimingSemesterTerm = 1,
                TimingSemester = 2
            };
            var studyplan1_subject5 = new Studyplan_Subject()
            {
                StudyPlanCode = studyplan_qualification1,
                SubjectCode = subject5,
                TimingSemesterTerm = 2,
                TimingSemester = 2
            };
            var studyplan1_subject6 = new Studyplan_Subject()
            {
                StudyPlanCode = studyplan_qualification1,
                SubjectCode = subject6,
                TimingSemesterTerm = 2,
                TimingSemester = 2
            };
            //studyplan_subject code: r001 subject: 7
            var studyplan2_subject1 = new Studyplan_Subject()
            {
                StudyPlanCode = studyplan_qualification2,
                SubjectCode = subject1,
                TimingSemesterTerm = 1,
                TimingSemester = 1
            };
            var studyplan2_subject2 = new Studyplan_Subject()
            {
                StudyPlanCode = studyplan_qualification2,
                SubjectCode = subject2,
                TimingSemesterTerm = 1,
                TimingSemester = 1
            };
            var studyplan2_subject3 = new Studyplan_Subject()
            {
                StudyPlanCode = studyplan_qualification2,
                SubjectCode = subject3,
                TimingSemesterTerm = 1,
                TimingSemester = 1
            };
            var studyplan2_subject4 = new Studyplan_Subject()
            {
                StudyPlanCode = studyplan_qualification2,
                SubjectCode = subject4,
                TimingSemesterTerm = 1,
                TimingSemester = 2
            };
            var studyplan2_subject5 = new Studyplan_Subject()
            {
                StudyPlanCode = studyplan_qualification2,
                SubjectCode = subject5,
                TimingSemesterTerm = 2,
                TimingSemester = 2
            };

            //allsubject list
            var allStudyplan_Subject = new List<Studyplan_Subject>();
            //studyplancode: R001
            allStudyplan_Subject.Add(studyplan1_subject1);
            allStudyplan_Subject.Add(studyplan1_subject2);
            allStudyplan_Subject.Add(studyplan1_subject3);
            allStudyplan_Subject.Add(studyplan1_subject4);
            allStudyplan_Subject.Add(studyplan1_subject5);
            allStudyplan_Subject.Add(studyplan1_subject6);

            //tableitems 
            var tableItems = new List<TableItems>();

            //ViewModel instance 
            Studyplan_SubjectVM viewModel = new Studyplan_SubjectVM();

            //I made an instance of a List<TableItems> where I use foreach loop to add data to this list
            foreach (var subject in allStudyplan_Subject)
            {

                tableItems.Add(new TableItems() {
                    TermSemester = subject.TimingSemesterTerm.ToString(),
                    Semester = subject.TimingSemester.ToString(),
                    SubjectCode = subject.SubjectCode.SubjectCode,
                    SubjectTitle = subject.SubjectCode.SubjectTitle,
                    SubjectDescription = subject.SubjectCode.SubjectDescription,
                    Prerequisite = "NA", 
                    Status = "FC"
                });

                //add items to the dropdown list
                viewModel.Timing.Add(subject.TimingSemester.ToString() + " " + subject.TimingSemesterTerm.ToString());
            }

            //Pass all the data I need from the objects to the viewmodel
            viewModel.StudentName = student_studyplan.Student.GivenName + " " + student_studyplan.Student.LastName;
            viewModel.Qualification = student_studyplan.QualCode.QualName;
            viewModel.NationalCode = student_studyplan.QualCode.NationalQualCode;
            viewModel.TafeCode = student_studyplan.QualCode.TafeQualCode;
            viewModel.TableItems = tableItems;

            //pass the ViewModel to the View
            return View(viewModel);
        }
    }
}