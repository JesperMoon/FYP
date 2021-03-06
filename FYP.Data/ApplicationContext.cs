﻿using FYP.Entities;
using FYP.Entities.Data;
using FYP.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYP.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base(ConstantHelper.AppSettings.Database.FYP)
        {
        }

        //Practitioner User Account
        public DbSet<Practitioner> Practitioner { get; set; }

        //Company Account
        public DbSet<Company> Company { get; set; }

        //Patient User Account
        public DbSet<Patient> Patient { get; set; }

        //Authorize Practitioner for Patient
        public DbSet<AuthorizePractitioner> AuthorizePractitioner { get; set; }

        //Appointment
        public DbSet<Appointment> Appointment { get; set; }

        //PatientRecord
        public DbSet<PatientRecord> RecordFile { get; set; }
        
        //Medicine
        public DbSet<Medicine> Medicine { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Entity)entityEntry.Entity).ModifiedOn = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity)entityEntry.Entity).CreatedOn = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
