using FirstExam.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FirstExam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; } = null!;
    }
}
