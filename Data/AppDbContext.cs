using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Owner> Owners => Set<Owner>();

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
        }
    }
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=firstexamdb;Username=admin;Password=admin123");

        return new AppDbContext(optionsBuilder.Options);
    }
}

}