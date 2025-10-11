using FirstExam.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Pet> Pets => Set<Pet>();
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
            modelBuilder.Entity<Owner>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Email).IsRequired().HasMaxLength(200);
                b.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                b.Property(x => x.Phone).IsRequired().HasMaxLength(7);
                b.Property(x => x.Active).IsRequired();
            });
            modelBuilder.Entity<Pet>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.OwnerId).IsRequired();
                p.Property(x => x.Breed).IsRequired().HasMaxLength(100);
                p.Property(x => x.sex).IsRequired().HasMaxLength(20);
                p.Property(x => x.Species).IsRequired().HasMaxLength(100);
                p.Property(x => x.BirthDate).IsRequired();
                p.Property(x => x.WeightKg).IsRequired();
                p.Property(x => x.Name).IsRequired().HasMaxLength(100);
            });
        }
    }
}
