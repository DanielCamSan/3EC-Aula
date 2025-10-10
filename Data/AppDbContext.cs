using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
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
            modelBuilder.Entity<Pet>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.Name).IsRequired().HasMaxLength(100);
                p.Property(x => x.Species).IsRequired().HasMaxLength(100);
                p.Property(x => x.Breed).IsRequired().HasMaxLength(100);
                p.Property(x => x.BirthDate).IsRequired();
                p.Property(x => x.sex).IsRequired().HasMaxLength(20);
                p.Property(x => x.WeightKg).HasPrecision(5, 2); // opcional
            });
        }
    }
}
