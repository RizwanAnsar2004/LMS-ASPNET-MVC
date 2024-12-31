using Microsoft.Ajax.Utilities;
using Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Project.Controllers
{
    public class facultyController : Controller
    {
        SDAprojectEntities db = new SDAprojectEntities();
        // GET: faculty
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult faculty_dashboard()
        {
            var result = AccountController.getDetailsoffaculty();
            ViewData["id"] = result[0].FacultyID;
            ViewData["name"] = result[0].Username;
            ViewData["email"] = result[0].Email;
            ViewData["password"] = result[0].Password;
            ViewData["phone"] = result[0].Phone;
            ViewData["address"] = result[0].Address;
            ViewData["dob"] = result[0].DateOfBirth;
            return View();
        }
        public ActionResult faculty_lecture()
        {
            var result = getCourses();
            return View(result);
        }

        public ActionResult upload_lecture()
        {
            return View();
        }
        public ActionResult faculty_assignment()
        {
            var result = getAssignment();

            return View(result);
        }

        public ActionResult grade_assignment()
        {
            var model = new assignment1();

            return View(model);
        }

        public ActionResult faculty_appointment()
        {
            var get_app = getApp();
            return View(get_app);
        }
        public ActionResult testview()
        {
            return View();
        }

        public List<assignment1> getAssignment()
        {
            using (var context = new SDAprojectEntities())
            {
                var result = context.Assignments.Select(t => new assignment1()
                {
                    AssignmentID = t.AssignmentID,
                    CourseID = t.CourseID ?? 0,
                    AssignmentTitle = t.AssignmentTitle,
                    AssignmentDescription = t.AssignmentDescription,
                    DueDate = t.DueDate.ToString(),
                    MaxPoints = t.MaxPoints ?? 0,
                    AssignmentType = t.AssignmentType,
                    AssignmentFilePath = t.AssignmentFilePath,
                }).ToList();

                return result;
            }
        }
        public List<course> getCourses()
        {
            using (var context = new SDAprojectEntities())
            {
                var result = context.Courses.Select(t => new course()
                {
                    CourseID = t.CourseID,
                    CourseName = t.CourseName,
                    CourseDescription = t.CourseDescription,
                    Schedule=t.Schedule,
                    
                }).ToList();

                return result;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public JsonResult Delete(int id)
        {
            try
            {
                using (var context = new SDAprojectEntities())
                {
                    var data = context.Assignments.SingleOrDefault(e => e.AssignmentID == id);
                    if (data != null)
                    {
                        context.Assignments.Remove(data);
                        context.SaveChanges(); // Make sure to save changes to the database
                        return Json(new { success = true, message = "Assignment deleted successfully." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Assignment not found." });
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (this would typically be done to a logging framework)
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult upload(IEnumerable<HttpPostedFileBase> files, Assignment assignment)
        {
            var path = "";

            // Loop through each uploaded file
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Get the file name and save it to the server
                        var fileName = Path.GetFileName(file.FileName);
                        path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                        file.SaveAs(path);
                    }
                }
            }

            // Save assignment details to the database
            using (var context = new SDAprojectEntities())
            {
                Assignment newAssignment = new Assignment()
                {
                    AssignmentTitle = assignment.AssignmentTitle,
                    AssignmentDescription = assignment.AssignmentDescription,
                    DueDate=assignment.DueDate,
                    MaxPoints=assignment.MaxPoints,
                    AssignmentFilePath = path,
                    // Assign other properties as needed
                };

                context.Assignments.Add(newAssignment);
                context.SaveChanges();
            }

            // Redirect to the Index action method
            return RedirectToAction("faculty_assignment");
        }

        [HttpPost]
        public ActionResult uploadLecture(IEnumerable<HttpPostedFileBase> files, Cours course)
        {
            var path = "";

            // Loop through each uploaded file
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Get the file name and save it to the server
                        var fileName = Path.GetFileName(file.FileName);
                        path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                        file.SaveAs(path);
                    }
                }
            }

            // Save assignment details to the database
            using (var context = new SDAprojectEntities())
            {
                var result = AccountController.getDetailsoffaculty();

                Cours newAssignment = new Cours()
                {
                    CourseName = course.CourseName,
                    CourseDescription = course.CourseDescription,
                    InstructorID = result[0].FacultyID,
                    Schedule = path,
                    // Assign other properties as needed
                };

                context.Courses.Add(newAssignment);
                context.SaveChanges();
            }

            // Redirect to the Index action method
            return RedirectToAction("faculty_lecture");
        }


        public List<app> getApp()
        {
            var resulto = AccountController.getDetailsoffaculty();
            int id=resulto[0].FacultyID;
            using (var context = new SDAprojectEntities())
            {
                var result = context.Appointments
                    
                    .Where(t=>t.FacultyID==id && t.Status=="Pending")
                    .Select(t => new app()
                {
                    AppointmentID = t.AppointmentID,
                    StudentID = t.StudentID,
                    AppointmentDescription = t.AppointmentDescription,
                    AppointmentDatetime = t.AppointmentDateTime,

                }).ToList();

                return result;
            }
        }

        [HttpPost]
        public ActionResult UpdateStatus(int id, string status)
        {
            try
            {
                using (var context = new SDAprojectEntities())
                {
                    var appointment = context.Appointments.Find(id);
                    if (appointment != null)
                    {
                        appointment.Status = status;
                        context.SaveChanges();
                        TempData["SuccessMessage"] = "Status updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Appointment not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the status: " + ex.Message;
            }

            return RedirectToAction("faculty_appointment"); // Replace "Index" with the action that lists the appointments
        }
        public ActionResult deleteAss(int id)
        {
            try
            {
                using (var context = new SDAprojectEntities())
                {
                    var appointment = context.Assignments.Find(id);
                    if (appointment != null)
                    {
                       context.Assignments.Remove(appointment);
                        context.SaveChanges();
                        TempData["SuccessMessage"] = "Status updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Appointment not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the status: " + ex.Message;
            }

            return RedirectToAction("faculty_assignment"); // Replace "Index" with the action that lists the appointments
        }


        public ActionResult deleteLecture(int id)
        {
            try
            {
                using (var context = new SDAprojectEntities())
                {
                    var appointment = context.Courses.Find(id);
                    if (appointment != null)
                    {
                        context.Courses.Remove(appointment);
                        context.SaveChanges();
                        TempData["SuccessMessage"] = "Status updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Appointment not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the status: " + ex.Message;
            }

            return RedirectToAction("faculty_lecture"); // Replace "Index" with the action that lists the appointments
        }
    }


}
