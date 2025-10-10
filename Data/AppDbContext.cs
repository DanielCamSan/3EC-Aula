using System.Security.Cryptography.X509Certificates;
using FirstExam.Controllers;
using FirstExam.Models;
using Microsoft.EntityFrameworkCore; 

namespace FirstExam.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Appointment> Appointments => Set<Appointment>(); 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(a =>
            {
                a.HasKey(x => x.Id);
                a.Property(x => x.PetId);
                a.Property(x => x.ScheduledAt);
                a.Property(x => x.Reason).IsRequired().HasMaxLength(100);
                a.Property(x => x.Status).IsRequired().HasMaxLength(100);
                a.Property(x => x.Notes);
            });
        }

    }
}