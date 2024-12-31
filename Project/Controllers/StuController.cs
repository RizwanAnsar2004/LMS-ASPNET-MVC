using Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class StuController : Controller
    {
        // GET: Stu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult stu_lectures()
        {
            var result = getCourses();
            return View(result);
        }
        public ActionResult stu_appointment()
        {
            //var result=getApp();
            return View();
            //result);
        }
        [HttpPost]
        public ActionResult uploadAppointment(IEnumerable<HttpPostedFileBase> files, Appointment app)
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

                Appointment newAssignment = new Appointment()
                {   AppointmentID=app.AppointmentID,
                    AppointmentDatetime = app.AppointmentDateTime,
                    AppointmentDescription = app.AppointmentDescription,
                   // Assign other properties as needed
                };

                context.Appointments.Add(newAssignment);
                context.SaveChanges();
            }

            // Redirect to the Index action method
            return RedirectToAction("stu_appointment");
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
                    Schedule = t.Schedule,

                }).ToList();

                return result;
            }
        }


        public List<Appointment> getApp()
        {
            using (var context = new SDAprojectEntities())
            {
                var result = context.Appointments.Select(t => new Appointment()
                {
                    AppointmentID = t.AppointmentID,
                    AppointmentDescription = t.AppointmentDescription,
                    AppointmentDatetime = t.AppointmentDateTime,
                    FacultyID=t.FacultyID,

                }).ToList();

                return result;
            }
        }



    }
}