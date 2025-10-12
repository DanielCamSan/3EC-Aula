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
            // Configuración para PostgreSQL timezone
            modelBuilder.Entity<Appointment>(a =>
            {
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedOnAdd();

                a.Property(x => x.PetId).IsRequired();

                // ✅ Configuración IMPORTANTE para DateTime
                a.Property(x => x.ScheduledAt)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToUniversalTime(), // Convertir a UTC al guardar
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Especificar como UTC al leer
                    );

                a.Property(x => x.Reason).IsRequired().HasMaxLength(100);
                a.Property(x => x.Status).IsRequired().HasMaxLength(100).HasDefaultValue("scheduled");
                a.Property(x => x.Notes);

                // Índices
                a.HasIndex(x => x.PetId);
                a.HasIndex(x => x.ScheduledAt);
                a.HasIndex(x => x.Status);
                a.HasIndex(x => new { x.PetId, x.ScheduledAt });
            });
        }

        // Opcional: Configurar globalmente para todas las fechas
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>()
                .HaveConversion<DateTimeToUtcConverter>();
        }
    }

    // Converter personalizado para UTC
    public class DateTimeToUtcConverter : Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>
    {
        public DateTimeToUtcConverter() : base(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
        {
        }
    }
}