<<<<<<< HEAD
﻿using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
=======
﻿using FirstExam.Data;
using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
>>>>>>> f6491646b1913dada099339a65f60ff7b98deac3

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

<<<<<<< HEAD
        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Appointment
            modelBuilder.Entity<Appointment>(a =>
            {
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedNever();

                a.Property(x => x.PetId).IsRequired();
                a.Property(x => x.ScheduledAt).IsRequired();
                a.Property(x => x.Reason).IsRequired().HasMaxLength(100);
                a.Property(x => x.Status).IsRequired().HasMaxLength(100).HasDefaultValue("scheduled");
                a.Property(x => x.Notes);

                // Índices para mejorar performance
                a.HasIndex(x => x.PetId);
                a.HasIndex(x => x.ScheduledAt);
                a.HasIndex(x => x.Status);
                a.HasIndex(x => new { x.PetId, x.ScheduledAt });
=======
        public DbSet<Owner> Owners => Set<Owner>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(o =>
            {
                o.HasKey(x => x.Id);
                o.Property(x => x.FullName)
                    .IsRequired()
                    .HasMaxLength(150);

                o.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                o.Property(x => x.Phone)
                    .HasMaxLength(20);
                o.Property(x => x.Active)
                    .IsRequired()
                    .HasDefaultValue(true);
>>>>>>> f6491646b1913dada099339a65f60ff7b98deac3
            });
        }
    }
}
