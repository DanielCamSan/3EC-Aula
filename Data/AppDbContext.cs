using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
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
            });
        }
    }
}
