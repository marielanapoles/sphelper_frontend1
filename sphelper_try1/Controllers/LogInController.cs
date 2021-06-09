using sphelper_try1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace sphelper_try1.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(student objStudent)
        {
            if (ModelState.IsValid)
            {
                using (SPHelperEntities db = new SPHelperEntities())
                {
                    var obj = db.students.Where(a => a.EmailAddress.Equals(objStudent.EmailAddress) && a.Password.Equals(objStudent.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["EmailAddress"] = obj.EmailAddress.ToString();
                        Session["StudentID"] = obj.StudentID.ToString();
                        Session["Student"] = obj;

                        return RedirectToAction("Index", "Home", obj);
                        //return View("Index", "Home", obj.StudentID.ToString());
                    }
                }
            }
            if (!Membership.ValidateUser(objStudent.EmailAddress, objStudent.Password))
            {
                ModelState.AddModelError(string.Empty, "Incorrect user ID or password. Type the correct user ID and password, and try again");
                return View();
            }

            return View();
        }
    }
}