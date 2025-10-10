using Microsoft.EntityFrameworkCore;

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
        }
    }
}
