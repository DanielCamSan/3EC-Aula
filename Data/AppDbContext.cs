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
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Pet> Pets => Set<Pet>();


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

            modelBuilder.Entity<Owner>(o =>
            {
                o.HasKey(x => x.Id);
                o.Property(x => x.Email).IsRequired();
                o.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                o.Property(x => x.Phone).IsRequired().HasMaxLength(7);
                o.Property(x => x.Active);

                    o.ToTable(t =>
                    {
                        t.HasCheckConstraint("CK_Owner_PhoneLength", "LEN([Phone]) >= 7");
                    });
            });

            modelBuilder.Entity<Pet>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.OwnerId).IsRequired();
                p.Property(x => x.Name).IsRequired().HasMaxLength(100);
                p.Property(x => x.Species).IsRequired().HasMaxLength(100);
                p.Property(x => x.Breed).IsRequired().HasMaxLength(100);
                p.Property(x => x.BirthDate).IsRequired();
                p.Property(x => x.sex).IsRequired().HasMaxLength(20);
                p.Property(x => x.WeightKg);

                    p.ToTable(t =>
                    {
                        t.HasCheckConstraint("CK_Pet_WeightKg", "[WeightKg] >= 0 AND [WeightKg] <= 500");
                    });
            }); 

        }

    }
}