using Microsoft.EntityFrameworkCore;
using DCXAir.Domain.Entities;

namespace DCXAir.Infrastructure.Persistence
{
    public class DCXAirDbContext : DbContext
    {
        public DCXAirDbContext(DbContextOptions<DCXAirDbContext> options) : base(options) {}

        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
