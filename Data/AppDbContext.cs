using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace FirstExam.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Pet> Pets => Set<Pet>();

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


        }
    }
}
