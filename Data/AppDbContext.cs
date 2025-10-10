using Microsoft.EntityFrameworkCore;
namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Appointment> Books => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasKey(x => x.PetId);
                b.Property(x => x.ScheduledAt).IsRequired();
                b.Property(x => x.Reason).IsRequired().HasMaxLength(100);
                b.Property(x => x.Status).IsRequired().HasMaxLength(100);
                b.Property(x => x.Notes).HasMaxLength(500);
            });
        }
    }
}