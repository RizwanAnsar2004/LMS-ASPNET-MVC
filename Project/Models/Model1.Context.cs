﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SDAprojectEntities : DbContext
    {
        public SDAprojectEntities()
            : base("name=SDAprojectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<StudentResult> StudentResults { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Submission> Submissions { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
