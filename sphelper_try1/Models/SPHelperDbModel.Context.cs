﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sphelper_try1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SPHelperEntities : DbContext
    {
        public SPHelperEntities()
            : base("name=SPHelperEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<campu> campus { get; set; }
        public virtual DbSet<combination_operator> combination_operator { get; set; }
        public virtual DbSet<competency> competencies { get; set; }
        public virtual DbSet<competency_qualification> competency_qualification { get; set; }
        public virtual DbSet<competency_type> competency_type { get; set; }
        public virtual DbSet<crn_detail> crn_detail { get; set; }
        public virtual DbSet<crn_session_timetable> crn_session_timetable { get; set; }
        public virtual DbSet<day_of_week> day_of_week { get; set; }
        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<lecturer> lecturers { get; set; }
        public virtual DbSet<prerequisite> prerequisites { get; set; }
        public virtual DbSet<prerequisite_combination> prerequisite_combination { get; set; }
        public virtual DbSet<qualification> qualifications { get; set; }
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<student_grade> student_grade { get; set; }
        public virtual DbSet<student_studyplan> student_studyplan { get; set; }
        public virtual DbSet<studyplan_qualification> studyplan_qualification { get; set; }
        public virtual DbSet<studyplan_subject> studyplan_subject { get; set; }
        public virtual DbSet<subject> subjects { get; set; }
        public virtual DbSet<subject_competency> subject_competency { get; set; }
        public virtual DbSet<subject_qualification> subject_qualification { get; set; }
        public virtual DbSet<term_datetime> term_datetime { get; set; }
    }
}
