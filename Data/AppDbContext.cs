using System.Collections.Generic;
using FirstExam.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstExam.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Pet> Pets => Set<Pet>();
        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Owner>(o =>
            {
                o.HasKey(x => x.Id);
                o.Property(x => x.Email).IsRequired();
                o.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                o.Property(x => x.Phone).IsRequired().HasMaxLength(7);
                o.Property(x => x.Active).HasDefaultValue(true);
            });

            
            modelBuilder.Entity<Pet>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.Name).IsRequired().HasMaxLength(100);
                p.Property(x => x.Species).IsRequired().HasMaxLength(100);
                p.Property(x => x.Breed).IsRequired().HasMaxLength(100);
                p.Property(x => x.sex).IsRequired().HasMaxLength(20);
                p.Property(x => x.BirthDate).IsRequired();
                p.Property(x => x.WeightKg).HasPrecision(2, 5);
            });


            modelBuilder.Entity<Appointment>(a =>
            {
                a.HasKey(x => x.Id);
                a.Property(x => x.Reason).IsRequired().HasMaxLength(100);
                a.Property(x => x.Status).IsRequired().HasMaxLength(100).HasDefaultValue("scheduled");
                a.Property(x => x.ScheduledAt).IsRequired();
                //entidad 
                a.HasIndex(x => x.PetId);
                a.HasIndex(x => x.ScheduledAt);

                a.HasOne(x => x.Pet)
                 .WithMany(p => p.Appointments)
                 .HasForeignKey(x => x.PetId)
                 .OnDelete(DeleteBehavior.Cascade);

                a.HasOne(a => a.Owner)
                  .WithMany(o => o.Appointments)
                  .HasForeignKey(a => a.OwnerId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Owner>()
                .HasOne(o => o.Pet)
                .WithOne(p => p.Owner)
                .HasForeignKey<Pet>(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
