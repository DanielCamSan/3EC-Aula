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
                b.Property(x => x.OwnerId).IsRequired();
                b.Property(x => x.Name).IsRequired().HasMaxLength(100);
                b.Property(x => x.Species).IsRequired().HasMaxLength(100);
                b.Property(x => x.Breed).IsRequired().HasMaxLength(100);
                b.Property(x => x.BirthDate).IsRequired();
                b.Property(x => x.sex).IsRequired().HasMaxLength(20);
                b.Property(x => x.WeightKg);
            });
            modelBuilder.Entity<Owner>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Email).IsRequired();
                b.Property(x => x.FullName).IsRequired().HasMaxLength(200); ;
                b.Property(x => x.Phone).IsRequired().HasMaxLength(100); ;
                b.Property(x => x.Active);
            });
            modelBuilder.Entity<Appointment>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.PetId);
                b.Property(x => x.ScheduledAt);
                b.Property(x => x.Reason).IsRequired().HasMaxLength(100); ;
                b.Property(x => x.Status).IsRequired().HasMaxLength(100); ;
                b.Property(x => x.Notes);
            });
        }
    }
}
