using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Owner> Owners => Set<Owner>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.FullName).IsRequired();
                b.Property(x => x.Email).IsRequired();
                b.Property(x => x.Phone).IsRequired();
                b.Property(x => x.Active).IsRequired();
            });
        }
    }
}


