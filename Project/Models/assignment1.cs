using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class assignment1
    {
        public int AssignmentID { get; set; }
        public int CourseID { get; set; }

        public string AssignmentTitle { get; set; }
        public string AssignmentDescription { get; set; }
        public string DueDate { get; set; }
        public int MaxPoints { get; set; }

        public string AssignmentType { get; set; }

        public string AssignmentFilePath { get; set; }
        //public string DueDate { get; set; }

    }
}