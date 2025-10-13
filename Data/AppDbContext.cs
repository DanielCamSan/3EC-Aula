using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Owner> Owners => Set<Owner>(); 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(a =>
            {
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedOnAdd();

                a.Property(x => x.OwnerId).IsRequired();

                a.Property(x => x.ScheduledAt)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                    );

                a.Property(x => x.Reason).IsRequired().HasMaxLength(100);
                a.Property(x => x.Status).IsRequired().HasMaxLength(100).HasDefaultValue("scheduled");
                a.Property(x => x.Notes);

                // Índices
                a.HasIndex(x => x.OwnerId);
                a.HasIndex(x => x.ScheduledAt);
                a.HasIndex(x => x.Status);
                a.HasIndex(x => new { x.OwnerId, x.ScheduledAt });
            });

        }
    }
}
