<<<<<<< HEAD
﻿using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FirstExam.Data
=======
﻿using apiwithdb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace apiwithdb.Data
>>>>>>> 99e531e4d6daf8c647512293ca4090505b2f442b
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
<<<<<<< HEAD
        public DbSet<Appointment> appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(a =>
            {
                a.HasKey(x => x.Id);
                a.Property(x => x.PetId).IsRequired();
                a.Property(x => x.ScheduledAt).IsRequired();
                a.Property(x => x.Reason).IsRequired().HasMaxLength(100);
                a.Property(x => x.Status).IsRequired().HasMaxLength(100);
                a.Property(x => x.Notes).HasMaxLength(500);
=======

        public DbSet<Owner> Owners => Set<Owner>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Email).IsRequired().HasMaxLength(200);
                b.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                b.Property(x => x.Phone).IsRequired().HasMaxLength(7);
                b.Property(x => x.Active).IsRequired();
>>>>>>> 99e531e4d6daf8c647512293ca4090505b2f442b
            });
        }
    }
}
