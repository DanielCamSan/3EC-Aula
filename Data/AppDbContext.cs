using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Pet> Pets => Set<Pet>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(o =>
            {
                o.HasKey(x => x.Id);
                o.Property(x => x.Email).IsRequired();
                o.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                o.Property(x => x.Phone).IsRequired();
                o.Property(x => x.Active);
            });

            modelBuilder.Entity<Appointment>(a =>
            {
                a.HasKey(x => x.Id);
                a.Property(x => x.Status).IsRequired().HasMaxLength(200);
                a.Property(x => x.Notes).IsRequired();
                a.HasIndex(x => x.PetId);
                a.Property(x => x.ScheduledAt).IsRequired();
                a.Property(x => x.Reason).IsRequired();
            });
            modelBuilder.Entity<Pet>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasKey(x => x.OwnerId);
                b.Property(x => x.Name);
                b.Property(x => x.Species);
                b.Property(x => x.Breed);
                b.Property(x => x.BirthDate);
                b.Property(x => x.sex);
                b.Property(x => x.WeightKg);
            });
        }
    }
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ownersdb;Username=ownersuser;Password=supersecret");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
