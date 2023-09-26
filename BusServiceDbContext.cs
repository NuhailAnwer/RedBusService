using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RedBusService.Entities;

namespace RedBusService
{
    public class BusServiceDbContext: IdentityDbContext<IdentityUser>
    {
        public BusServiceDbContext(DbContextOptions<BusServiceDbContext> options) : base(options)
        {}
        public DbSet<Bus> Buses { get; set; } 
        public DbSet<Driver> Drivers { get; set; }
        public DbSet< BusDriver> BusDrivers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}
