using FirstExam.Models;
<<<<<<< HEAD
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
=======
>>>>>>> a709bdc39f5ff6fbce604d913416241f95b524bc
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
=======
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
>>>>>>> capas/team03-owners
using System.Reflection.Emit;
>>>>>>> 5e22b43037a80802e8265c9d930a9b5303147140

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
<<<<<<< HEAD
<<<<<<< HEAD
        public DbSet<Pet> Pets => Set<Pet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.Name).IsRequired().HasMaxLength(100);
                p.Property(x => x.Species).IsRequired().HasMaxLength(100);
                p.Property(x => x.Breed).IsRequired().HasMaxLength(100);
                p.Property(x => x.BirthDate).IsRequired();
                p.Property(x => x.sex).IsRequired().HasMaxLength(20);
            });
        }
    }
}
=======
=======
>>>>>>> a709bdc39f5ff6fbce604d913416241f95b524bc
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
>>>>>>> 5e22b43037a80802e8265c9d930a9b5303147140
