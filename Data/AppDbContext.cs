using Microsoft.EntityFrameworkCore;
using FirstExam.Models;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Pet> Pets => Set<Pet>();   
        public DbSet<Owner> Owners => Set<Owner>();   
        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Email).IsRequired();
                b.Property(x => x.FullName).IsRequired().HasMaxLength(200); ;
                b.Property(x => x.Phone).IsRequired().HasMaxLength(100); ;
                b.Property(x => x.Active);

                b.HasOne(o => o.Pet)
                    .WithOne(p => p.Owner)
                    .HasForeignKey<Pet>(p => p.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Pet>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.OwnerId).IsRequired();
                b.Property(x => x.Name).IsRequired().HasMaxLength(100);
                b.Property(x => x.Species).IsRequired().HasMaxLength(100);
                b.Property(x => x.Breed).IsRequired().HasMaxLength(100);
                b.Property(x => x.BirthDate).IsRequired();
                b.Property(x => x.Sex).IsRequired().HasMaxLength(20);
                b.Property(x => x.WeightKg);

                b.HasMany(p => p.Appointments)
                    .WithOne(a => a.Pet)
                    .HasForeignKey(a => a.PetId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Appointment>(b =>
            {
                b.HasKey(x => x.Id);

                b.Property(x => x.OwnerId).IsRequired();
                b.Property(x => x.PetId).IsRequired();
                b.Property(x => x.ScheduledAt).IsRequired();
                b.Property(x => x.Reason).IsRequired().HasMaxLength(100); ;
                b.Property(x => x.Status).IsRequired().HasMaxLength(100); ;
                b.Property(x => x.Notes);

                b.HasOne(a => a.Owner)
                    .WithMany(o => o.Appointments)
                    .HasForeignKey(a => a.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
