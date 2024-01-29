using Microsoft.EntityFrameworkCore;

namespace Trucks.API
{
    public class TrucksDbContext : DbContext
    {
        public TrucksDbContext(DbContextOptions<TrucksDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Truck> Trucks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

