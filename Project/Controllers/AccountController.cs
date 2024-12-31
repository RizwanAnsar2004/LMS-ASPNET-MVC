using Project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        SDAprojectEntities db = new SDAprojectEntities();
        static int studentId;
        static int facultyId;
        static int adminId;
        static string ttype;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult stuLogin()
        {
            if (Session["PageVisited"] != null)
            {
                return RedirectToAction("LoginAlreadyOpen");
            }
            return View();
        }

        [HttpPost]
        public ActionResult stuLogin(MyLogin l)
        {
            var query = db.Students.FirstOrDefault(m => m.StudentID == l.StudentID && m.UserPassword == l.UserPassword);
            if (query != null)
            {
                studentId = query.StudentID;
                ttype = "student";
                Session["PageVisited"] = true;
                return RedirectToAction("stu_dashboard", "Account");
            }
            else
            {
                Response.Write("<script>alert('Invalid Credentials')</script>");
            }
            return View();
        }

        public ActionResult fac_Login()
        {
            if (Session["PageVisited"] != null)
            {
                return RedirectToAction("LoginAlreadyOpen");
            }
            return View();
        }

        [HttpPost]
        public ActionResult fac_Login(MyLogin l)
        {
            var query = db.Faculties.FirstOrDefault(m => m.FacultyID == l.FacultyID && m.UserPassword == l.UserPassword);
            if (query != null)
            {
                Session["FacultyID"] = query.FacultyID;
                Session["type"] = "faculty";
                Session["PageVisited"] = true;
                Response.Write("<script>alert('Login Successfully')</script>");
                return RedirectToAction("faculty_dashboard", "faculty");
            }
            else
            {
                Response.Write("<script>alert('Invalid Credentials')</script>");
            }
            return View();
        }

        public ActionResult ad_Login()
        {
            if (Session["PageVisited"] != null)
            {
                return RedirectToAction("LoginAlreadyOpen");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ad_Login(MyLogin l)
        {
            var query = db.Admins.FirstOrDefault(t => t.AdminID == l.AdminID && t.UserPassword == l.UserPassword);
            if (query != null)
            {
                Session["AdminID"] = query.AdminID;
                Session["type"] = "admin";
                Session["PageVisited"] = true;
                Response.Write("<script>alert('Login Successfully')</script>");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Response.Write("<script>alert('Invalid Credentials')</script>");
            }
            return View();
        }

        public ActionResult LoginAlreadyOpen()
        {
            return View();
        }

        public ActionResult stu_dashboard()
        {
            var result = getDetails();
            ViewData["id"] = result[0].StudentID;
            ViewData["name"] = result[0].Username;
            ViewData["email"] = result[0].Email;
            ViewData["password"] = result[0].Password;
            ViewData["phone"] = result[0].Phone;
            ViewData["address"] = result[0].Address;
            ViewData["dob"] = result[0].DateOfBirth;

            return View();
        }

        public ActionResult Stu_assignment()
        {
            return View();
        }

        static public List<StudentDetailsViewModel> getDetails()
        {
            using (var context = new SDAprojectEntities())
            {
                var result = context.Students.Select(t => new StudentDetailsViewModel()
                {
                    StudentID = t.StudentID,
                    Username = t.UserName,
                    Email = t.UserEmail,
                    Password = t.UserPassword,
                    Phone = t.UserPhone,
                    Address = t.UserAddress,
                    DateOfBirth = t.DateOfBirth.ToString(),
                }).ToList();
                return result;
            }
        }

        static public List<FacultyDetailsViewModel> getDetailsoffaculty()
        {
            using (var context = new SDAprojectEntities())
            {
                var result = context.Faculties.Select(t => new FacultyDetailsViewModel()
                {
                    FacultyID = t.FacultyID,
                    Username = t.UserName,
                    Email = t.UserEmail,
                    Password = t.UserPassword,
                    Phone = t.UserPhone,
                    Address = t.UserAddress,
                    DateOfBirth = t.DateOfBirth.ToString(),
                }).ToList();
                return result;
            }
        }

        static public List<AdminDetailsViewModel> getDetailsofadmin()
        {
            using (var context = new SDAprojectEntities())
            {
                var result = context.Admins.Select(t => new AdminDetailsViewModel()
                {
                    AdminID = t.AdminID,
                    Username = t.UserName,
                    Email = t.UserEmail,
                    Password = t.UserPassword,
                    Phone = t.UserPhone,
                    Address = t.UserAddress,
                    DateOfBirth = t.DateOfBirth.ToString(),
                }).ToList();
                return result;
            }
        }
    }
}