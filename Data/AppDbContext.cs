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
            modelBuilder.Entity<Pet>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.OwnerId);
                b.Property(x => x.Name);
                b.Property(x => x.Species);
                b.Property(x => x.Breed);
                b.Property(x => x.BirthDate);
                b.Property(x => x.sex);
                b.Property(x => x.WeightKg);
            });
            modelBuilder.Entity<Owner>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Email);
                b.Property(x => x.FullName);
                b.Property(x => x.Phone);
                b.Property(x => x.Active);
            });
            modelBuilder.Entity<Appointment>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.PetId);
                b.Property(x => x.ScheduledAt);
                b.Property(x => x.Reason);
                b.Property(x => x.Status);
                b.Property(x => x.Notes);
            });
        }
    }
}
