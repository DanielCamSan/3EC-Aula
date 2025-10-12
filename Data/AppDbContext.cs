using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace FirstExam.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Pet> Pets => Set<Pet>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Owner> Owners => Set<Owner>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>(p =>
            {
                p.HasKey(x => x.Id);
                p.HasKey(x => x.OwnerId);
                p.Property(x => x.Name).IsRequired().HasMaxLength(200);
                p.Property(x => x.Species).IsRequired();
                p.Property(x => x.Breed).IsRequired();
                p.Property(x => x.BirthDate).IsRequired();
                p.Property(x => x.sex);
                p.Property(x => x.WeightKg).HasPrecision(5, 2);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PetId).IsRequired();
                entity.Property(e => e.ScheduledAt).IsRequired();
                entity.Property(e => e.Reason).HasMaxLength(500);
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Notes).HasMaxLength(1000);
            });

            modelBuilder.Entity<Owner>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Email).IsRequired().HasMaxLength(200);
                b.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                b.Property(x => x.Phone).IsRequired().HasMaxLength(7);
                b.Property(x => x.Active).IsRequired();
                b.HasIndex(x => x.Email).IsUnique();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
