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
    
    public partial class Cours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cours()
        {
            this.Assignments = new HashSet<Assignment>();
            this.Enrollments = new HashSet<Enrollment>();
            this.StudentResults = new HashSet<StudentResult>();
        }
    
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public Nullable<int> Credits { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> InstructorID { get; set; }
        public string Schedule { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual Department Department { get; set; }
        public virtual Faculty Faculty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentResult> StudentResults { get; set; }
    }
}
