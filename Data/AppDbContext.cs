using FirstExam.Models;
<<<<<<< HEAD
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
=======
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
>>>>>>> capas/team03-owners
using System.Reflection.Emit;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
<<<<<<< HEAD
        public DbSet<Appointment> Books => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(b =>
            {
                /*
                b.HasKey(x => x.Id);
                b.Property(x => x.Title).IsRequired().HasMaxLength(200);
                b.Property(x => x.Year).IsRequired();
                */
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
>>>>>>> capas/team03-owners
            });
        }
    }
}
