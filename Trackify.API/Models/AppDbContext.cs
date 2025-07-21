
using Microsoft.EntityFrameworkCore;
using Trackify.API.Models;

namespace Trackify.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2); // Prevents silent truncation of decimal values

            base.OnModelCreating(modelBuilder);
        }
    }
}