using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Appointment> Appointments => Set<Appointment>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Status).IsRequired().HasMaxLength(200);
                b.Property(x => x.Notes).IsRequired();
            });

      
        }
    }
}