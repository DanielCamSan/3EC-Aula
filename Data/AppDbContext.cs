using FirstExam.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Pet> Pets => Set<Pet>();

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

        }
    }
}
