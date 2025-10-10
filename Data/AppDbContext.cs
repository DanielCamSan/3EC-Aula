using FirstExam.Models;
using Microsoft.EntityFrameworkCore;

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
            // Configuración de Appointment
            modelBuilder.Entity<Appointment>(a =>
            {
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedNever();

                a.Property(x => x.PetId).IsRequired();
                a.Property(x => x.ScheduledAt).IsRequired();
                a.Property(x => x.Reason).IsRequired().HasMaxLength(100);
                a.Property(x => x.Status).IsRequired().HasMaxLength(100).HasDefaultValue("scheduled");
                a.Property(x => x.Notes);

                // Índices para mejorar performance
                a.HasIndex(x => x.PetId);
                a.HasIndex(x => x.ScheduledAt);
                a.HasIndex(x => x.Status);
                a.HasIndex(x => new { x.PetId, x.ScheduledAt });
            });
        }
    }
}
