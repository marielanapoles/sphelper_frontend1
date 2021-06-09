using sphelper_try1.Models;
using sphelper_try1.Models.DataManager;
using sphelper_try1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;
using System.Net.Mail;

namespace sphelper_try1.Controllers
{
    public class HomeController : Controller
    {
        private string[] Lecturers = { "IT Studies", "Kym Bond", "KT Lau" };
        private string s_id = "001061329";

        public ActionResult RedirectToIndex()
        {
            var studentInSession = Session["Student"] as student;
            return RedirectToAction("Index", studentInSession);
        }


        //public ActionResult Index(string studentId)
        public ActionResult Index(student obj)
        {
            string studentId = obj.StudentID;
            if (Session["StudentID"] != null)
            {
                //get student name
                var studentName = Student_StudyplanManager.FindstudentName(studentId);

                //get qualification, tafecode & national code
                var qualification = Student_StudyplanManager.FindQualificationByStudentId(studentId);

                //get study plan for each student
                var studyPlanCode = Student_StudyplanManager.FindStudyplanByQualificaitonCode(qualification.QualCode);

                //get gradeslist for student 
                List<SubjectResults> subjectResultsList = Student_StudyplanManager.FindStudentGradeList(studentId);

                var date = DateTime.Now;

                var student = new studentinfo();
                student.StudentId = studentId;
                student.Name = studentName;
                student.Qualification = qualification;
                student.StudyPlanCode = studyPlanCode;
                student.StudyPlanSubjects = Student_StudyplanManager.GetStudyPlanByStudentId(studyPlanCode);
                student.SemesterNow = Student_StudyplanManager.GetCurrentSemester(date);
                student.SubjectResultsList = Student_StudyplanManager.FindStudentGradeList(studentId);

                var viewModel = new HomeVM();
                viewModel.StudentInfo = student;

                //memory cache
                var expirydate = date.AddDays(1);
                MemoryCache.Default.Add("studentinfo", student, expirydate);

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult Error()
        {
            var studentInSession = Session["Student"] as student;
            
            return View(studentInSession);
        }

        public ActionResult SeekAdvice()
        {
            ViewBag.mensage = "Test from ViewBag ActionResult SeekAdvice() ";
            ViewData["vd"] = "Test ViewData sending";

            //1.LIST FOR ATTRIBUTE ISSUE://////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //List<string> issueList = new List<string>();
            List<SelectListItem> issueList = new List<SelectListItem>();

            issueList.Add(new SelectListItem { Text = "Select", Value = "No selecion." });
            issueList.Add(new SelectListItem { Text = "I don't know the steps to enrol my subjects.", Value = "I don't know the steps to enrol my subjects." });
            issueList.Add(new SelectListItem { Text = "I don't know which study path select for each subject.", Value = "I don't know which study path select for each subject." });
            issueList.Add(new SelectListItem { Text = "I don't know how many subject must register this semester.", Value = "I don't know how many subject must register this semester." });
            issueList.Add(new SelectListItem { Text = "A subject shows an errors when I try to register it.", Value = "I don't know which study path select for each subject." });
            

            //Lleva la lista issueList ala view seekadvice:
            ViewBag.issueListFromController = issueList;
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                

            SPHelperEntities db = new SPHelperEntities();
            var query = from lec in db.lecturers.ToList()
                            select new
                            {
                                lec.LecturerID,
                                lec.GivenName,
                                lec.LastName,
                                lec.EmailAddress
                            };

            var lec_list = new List<lecturer>();

            foreach (var q in query)
            {
                var l = new lecturer()
                {
                    LecturerID = q.LecturerID,
                    GivenName = q.GivenName,
                    LastName = q.LastName,
                    EmailAddress = q.EmailAddress
                };
                lec_list.Add(l);

            }

            //3.LIST FOR ATTRIBUTE LECTURER:///////////////////////////////////////////////////////////////////////////////////////////

            //Lleva la lista issueList ala view seekadvice:
            List<SelectListItem> lecturerList = new List<SelectListItem>();
            lecturerList.Add(new SelectListItem { Text = "Select", Value = "No selection" });
            lecturerList.Add(new SelectListItem { Text = "Heath Barratt", Value = "Heath Barratt" });
            lecturerList.Add(new SelectListItem { Text = "Jimmy Roa", Value = "jimmy.roa@hotmail.com" });
            lecturerList.Add(new SelectListItem { Text = "Mariela Napoles", Value = "mhiel063@gmail.com" });
            lecturerList.Add(new SelectListItem { Text = "Kym Bond", Value = "Kym Bond" });
            lecturerList.Add(new SelectListItem { Text = "Jackie Brooks", Value = "Jackie Brooks" });
            lecturerList.Add(new SelectListItem { Text = "Heath Barratt", Value = "Heath Barratt" });
            lecturerList.Add(new SelectListItem { Text = "Paul Burke", Value = "Paul Burke" });
            lecturerList.Add(new SelectListItem { Text = "Roberto Cevallos", Value = "Roberto Cevallos" });
            lecturerList.Add(new SelectListItem { Text = "James Corbett", Value = "James Corbett" });
            lecturerList.Add(new SelectListItem { Text = "Deb Farrell", Value = "Deb Farrell" });
            lecturerList.Add(new SelectListItem { Text = "Mel Huikeshoven", Value = "Mel Huikeshovent" });
            lecturerList.Add(new SelectListItem { Text = "Greg Lync", Value = "Greg Lync" });
            lecturerList.Add(new SelectListItem { Text = "Glenn McCallum", Value = "Glenn McCallum" });
            lecturerList.Add(new SelectListItem { Text = "Michelle Minkoffl", Value = "Michelle Minkoff" });
            lecturerList.Add(new SelectListItem { Text = "Craig Moody", Value = "Craig Moody" });
            lecturerList.Add(new SelectListItem { Text = "Prem Paelchen", Value = "Prem Paelchen" });
            lecturerList.Add(new SelectListItem { Text = "Daryn Piltz", Value = "Daryn Piltz" });
            lecturerList.Add(new SelectListItem { Text = "Julie Ruiz", Value = "Julie Ruiz" });
            lecturerList.Add(new SelectListItem { Text = "Ruslan Sagirov", Value = "Ruslan Sagirov" });
            lecturerList.Add(new SelectListItem { Text = "Danny Sarrist", Value = "Danny Sarris" });
            lecturerList.Add(new SelectListItem { Text = "Dale Van Heer", Value = "Dale Van Heer" });
            lecturerList.Add(new SelectListItem { Text = "Alex Worrallt", Value = "Alex Worrall" });
            lecturerList.Add(new SelectListItem { Text = "Alex Zhao", Value = "Alex Zhao" });
            lecturerList.Add(new SelectListItem { Text = "Joe Liang", Value = "Joe Liang" });
            lecturerList.Add(new SelectListItem { Text = "Chris Wiren", Value = "Chris Wiren" });


            ViewBag.lecturerListFromController = lecturerList;


            //CREATING SEEKADVICE OBJECT:
            SeekAdvice s = new SeekAdvice(issueList, lecturerList, "here email lecturer", " here Student enquiry", "here Text body", lec_list);


            return View(s);

        }

        public ActionResult SendMail()
        {
            //Envia un email al cargar la action SendEmail en la View SeekAdvice y abre la view SendEmail.cshtml al final:

            //Datos del Remitente del envio:
            string EmailOrigen = "jimmy.roa@hotmail.com";
            string contrasena = "minena2012x";

            //ATRIBUTOS DEL OBJETO A ENVIAR:



            string EmailDestino = Request["lecturerListFromController"]; //"jimmy.roa@hotmail.com";//reconoce el campo html o helper.html con el name = lecturerListFromController del from en la view
            //string subject = Request["selectIssue"];//reconoce el campo html con el name = selectIssue del from en la view
            string body = Request["TextAreaFromHtml"];//reconoce el campo html o helper.html con el name = TextAreaFromHtml del from en la view
            //Trae el item seleccionado de la lista:  ViewBag.issueListFromController = issueList; en Action SeekAdvice()
            string subject = Request["issueListFromController"];


            //INSTANCIA DEL OBJETO A ENVIAR:
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, subject, body);
            oMailMessage.IsBodyHtml = true;

            //Datos del host del cliente: ex; hotmail:
            SmtpClient oSmtpClient = new SmtpClient("smtp.live.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            //oSmtpClient.Host = "smtp.live.com";
            oSmtpClient.Port = 25;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, contrasena);

            //Enviar email:
            oSmtpClient.Send(oMailMessage);

            //Despues borra todo:
            oSmtpClient.Dispose();

            //Resultado: si envio pero puede sacar un error acerca de no encontrar el view SendEmail.cshtml,
            //se debb crear su correspondiente View: 


            //return View();
            var studentInSession = Session["Student"] as student;

            return View(studentInSession);
        }
    }
}