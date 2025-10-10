using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
  
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Pet> Pets => Set<Pet>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                b.Property(x => x.Email).IsRequired();
                b.Property(x => x.Phone).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Pet>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired().HasMaxLength(100);
                b.Property(x => x.Species).IsRequired().HasMaxLength(100);
                b.Property(x => x.Breed).HasMaxLength(100);
                b.Property(x => x.sex).HasMaxLength(20);
            });

            modelBuilder.Entity<Appointment>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Reason).IsRequired().HasMaxLength(100);
                b.Property(x => x.Status).IsRequired().HasMaxLength(100);
            });
        }
    }
}