//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Grade
    {
        public int GradeID { get; set; }
        public Nullable<int> SubmissionID { get; set; }
        public Nullable<decimal> GradeValue { get; set; }
        public string Feedback { get; set; }
        public Nullable<System.DateTime> GradingDate { get; set; }
        public Nullable<int> GraderID { get; set; }
    
        public virtual Faculty Faculty { get; set; }
        public virtual Submission Submission { get; set; }
    }
}
