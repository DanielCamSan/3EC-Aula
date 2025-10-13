using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Pet> Pets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(o =>
            {
                o.HasKey(x => x.Id);
                o.Property(x => x.Email).IsRequired();
                o.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                o.Property(x => x.Phone).IsRequired();
                o.Property(x => x.Active);
                o.HasMany(x => x.Appointments).WithOne(a => a.owner).HasForeignKey(a => a.OwnerId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Appointment>(a =>
            {
                a.HasKey(x => x.Id);
                a.Property(x => x.Status).IsRequired().HasMaxLength(200);
                a.Property(x => x.Notes).IsRequired();
                a.HasIndex(x => x.PetId);
                a.Property(x => x.ScheduledAt).IsRequired();
                a.Property(x => x.Reason).IsRequired();
                a.HasIndex(x => x.OwnerId);

                a.HasOne(x => x.Pet)                      
                 .WithMany(p => p.Appointments)           
                 .HasForeignKey(x => x.PetId)            
                 .OnDelete(DeleteBehavior.Restrict);      

                a.HasOne<Owner>().WithMany(o => o.Appointments).HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Pet>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.OwnerId);
                b.Property(x => x.Name);
                b.Property(x => x.Species);
                b.Property(x => x.Breed);
                b.Property(x => x.BirthDate);
                b.Property(x => x.Sex);
                b.Property(x => x.WeightKg);
                b.HasMany(p => p.Appointments)
                 .WithOne(a => a.Pet)
                 .HasForeignKey(a => a.PetId)
                 .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(o => o.Owners) 
                    .WithMany()
                    .HasForeignKey(b => b.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);     
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
