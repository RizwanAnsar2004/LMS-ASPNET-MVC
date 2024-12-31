using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class app
    {
        public int AppointmentID { get; set; }

        public DateTime AppointmentDatetime { get; set; }
        public string AppointmentDescription { get; set; }
        
        public int StudentID { get; set; }
        public int FacultyID { get; set; }

        public string Status { get; set; }


    }
}