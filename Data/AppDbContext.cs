using FirstExam.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Owner> Owners => Set<Owner>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Email).IsRequired().HasMaxLength(200);
                b.Property(x => x.FullName).IsRequired().HasMaxLength(200);
                b.Property(x => x.Phone).IsRequired().HasMaxLength(7);
                b.Property(x => x.Active).IsRequired();
                b.HasIndex(x => x.Email).IsUnique();
            });
        }
    }
}
