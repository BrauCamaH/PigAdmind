﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1.DatabaseFirst
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Births> Births { get; set; }
        public virtual DbSet<Inseminations> Inseminations { get; set; }
        public virtual DbSet<PigGroups> PigGroups { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Sicks> Sicks { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Weaning> Weaning { get; set; }
        public virtual DbSet<Females> Females { get; set; }
    }
}
