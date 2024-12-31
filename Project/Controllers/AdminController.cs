using Microsoft.Ajax.Utilities;
using Project.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Xml.Linq;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Project.Controllers
{
    public class AdminController : Controller
    {
        Models.SDAprojectEntities data = new Models.SDAprojectEntities();
        // GET: Admin
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult testview()
        {
            return View();
        }

        public ActionResult admin_dashboard()
        {
            var stu = data.Faculties.ToList().Count;
            ViewData["f_count"] = stu;
            var su = data.Students.ToList().Count;
            ViewData["s_count"] = su;
            var result = AccountController.getDetailsofadmin();
            ViewData["id"] = result[0].AdminID;
            ViewData["name"] = result[0].Username;
            ViewData["email"] = result[0].Email;
            ViewData["password"] = result[0].Password;
            ViewData["phone"] = result[0].Phone;
            ViewData["address"] = result[0].Address;
            ViewData["dob"] = result[0].DateOfBirth;

            return View();
        }
        

        public ActionResult Manage_Student()
        {
            var stu = data.Students.ToList();
            return View(stu);
        }
        public ActionResult Create_Student()
        {

            return View(new Student());

        }
        public ActionResult Update_Student(int id)
        {
            var stu = data.Students.Find(id);

            return View(stu);
        }
        public ActionResult Update_Faculty(int id)
        {
            var stu = data.Faculties.Find(id);

            return View(stu);
        }

        public ActionResult Manage_Faculty()
        {
            var stu = data.Faculties.ToList();
            return View(stu);
        }
        public ActionResult Create_Faculty()
        {

            return View(new Faculty());

        }
        [HttpPost]
        public ActionResult Create_Faculty(Faculty s)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new SDAprojectEntities())
                    {
                        context.Faculties.Add(s);
                        context.SaveChanges();
                    }
                    return RedirectToAction("Manage_Faculty");
                }

                //ViewBag.MajorID = GetMajors();
                return View(s);
            }
            catch (Exception ex)
            {
                TempData["message"] = "<script>alert('AN UNEXPECTED ERROR OCCURED');</script>";
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create_Student(Student s)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new SDAprojectEntities())
                    {
                        context.Students.Add(s);
                        context.SaveChanges();
                    }
                    return RedirectToAction("Manage_Student");
                }

                //ViewBag.MajorID = GetMajors();
                return View(s);
            }
            catch (Exception ex)
            {
                TempData["message"] = "<script>alert('AN UNEXPECTED ERROR OCCURED');</script>";
            }
            return View();
        }
        [HttpPost]
        public List<StudentDetailsViewModel> getStudents()
        {
            using (var context = new SDAprojectEntities())
            {
                var result = context.Students.Select(t => new StudentDetailsViewModel()
                {
                    StudentID = t.StudentID,
                    Name = t.UserName,
                    Email = t.UserEmail,
                    Phone = t.UserPhone,
                    Address = t.UserAddress,
                    DateOfBirth = t.DateOfBirth.ToString(),


                }).ToList();

                return result;
            }
        }
        [HttpPost]
        public ActionResult Update_Student(Student s)
        {
            try
            {
                var existingStudent = data.Students.Find(s.StudentID);
                if (existingStudent != null)
                {
                    existingStudent.StudentID = s.StudentID;
                    existingStudent.UserName = s.UserName;
                    existingStudent.UserEmail = s.UserEmail;
                    existingStudent.UserPhone = s.UserPhone;
                    existingStudent.UserAddress = s.UserAddress;
                    existingStudent.DateOfBirth = s.DateOfBirth;



                    data.SaveChanges();
                    TempData["message"] = "<script>alert('Data updated Successfully');</script>";
                    return RedirectToAction("Manage_Student");
                }
                else
                {
                    TempData["message"] = "<script> alert('Record Doesnot Exist');</script>";
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<script>alert('An unexpected error occurred');</script>";
            }
            return View();

        }
        [HttpPost]
        public ActionResult Delete_Student(int id)
        {
            try
            {
                using (var context = new SDAprojectEntities())
                {
                    var appointment = context.Students.Find(id);
                    if (appointment != null)
                    {
                        context.Students.Remove(appointment);
                        context.SaveChanges();
                        TempData["SuccessMessage"] = "Record Removed successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "An Error not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the status: " + ex.Message;
            }

            return RedirectToAction("Manage_Student", "Admin"); // Replace "Index" with the action that lists the appointments
        }

        [HttpPost]
        public ActionResult Update_Faculty(Faculty s)
        {
            try
            {
                var existingStudent = data.Faculties.Find(s.FacultyID);
                if (existingStudent != null)
                {
                    existingStudent.FacultyID = s.FacultyID;
                    existingStudent.UserName = s.UserName;
                    existingStudent.UserEmail = s.UserEmail;
                    existingStudent.UserPhone = s.UserPhone;
                    existingStudent.UserAddress = s.UserAddress;
                    existingStudent.DateOfBirth = s.DateOfBirth;
                    existingStudent.OfficeLocation=s.OfficeLocation;
                    existingStudent.Title = s.Title;

                    data.SaveChanges();
                    TempData["message"] = "<script>alert('Data updated Successfully');</script>";
                    return RedirectToAction("Manage_Student");
                }
                else
                {
                    TempData["message"] = "<script> alert('Record Doesnot Exist');</script>";
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<script>alert('An unexpected error occurred');</script>";
            }
            return View();

        }
        [HttpPost]
        public ActionResult Delete_Faculty(int id)
        {
            try
            {
                using (var context = new SDAprojectEntities())
                {
                    var appointment = context.Faculties.Find(id);
                    if (appointment != null)
                    {
                        context.Faculties.Remove(appointment);
                        context.SaveChanges();
                        TempData["SuccessMessage"] = "Record Removed successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "An Error not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the status: " + ex.Message;
            }

            return RedirectToAction("Manage_Faculty", "Admin"); // Replace "Index" with the action that lists the appointments
        }
    }
}
    

