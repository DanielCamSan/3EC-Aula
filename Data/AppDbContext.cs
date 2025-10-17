using FirstExam.Models;
using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Active).IsRequired();
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Species).HasMaxLength(100);
                entity.Property(p => p.Breed).HasMaxLength(100);
                entity.Property(p => p.sex).HasMaxLength(20);
                entity.Property(p => p.WeightKg);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Reason).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Notes).HasMaxLength(500); 
            });

            modelBuilder.Entity<Owner>()
                .HasMany(owner => owner.Pets)
                .WithOne(pet => pet.Owner)
                .HasForeignKey(pet => pet.OwnerId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Owner>()
                .HasMany(owner => owner.Appointments)
                .WithOne(appointment => appointment.Owner)
                .HasForeignKey(appointment => appointment.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pet>()
                .HasMany(pet => pet.Appointments)
                .WithOne(appointment => appointment.Pet)
                .HasForeignKey(appointment => appointment.PetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}