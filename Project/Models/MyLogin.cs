using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class MyLogin
    {
        //public string username { get; set; }
        ////public string email { get; set; }
        ////public string password { get; set; }
        ////public string role { get; set; }
        //public int stu_id { get; set; }
        //public int f_id { get; set; }
        //public string f_password { get; set; }
        //public string stu_password { get; set; }
        //public int ad_id { get; set; }
        //public string ad_password { get; set; }

        public int AdminID { get; set; }
        public string UserPassword { get; set; }
        public int FacultyID { get; set; }
        public int StudentID { get; set; }
        public bool IsPageVisited { get; set; }

    }
}