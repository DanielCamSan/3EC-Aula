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

        public DbSet<Owner> Owners => Set<Owner>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(o =>
            {
                o.HasKey(x => x.Id);
                o.Property(x => x.FullName)
                    .IsRequired()
                    .HasMaxLength(150);

                o.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                o.Property(x => x.Phone)
                    .HasMaxLength(20);
                o.Property(x => x.Active)
                    .IsRequired()
                    .HasDefaultValue(true);
            });
        }
    }
}
