using Microsoft.EntityFrameworkCore;
using FirstExam.Models;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Pet> Pets => Set<Pet>();   
        public DbSet<Owner> Owners => Set<Owner>();   
        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
