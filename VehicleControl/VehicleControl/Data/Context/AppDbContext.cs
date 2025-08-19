using Microsoft.EntityFrameworkCore;
using VehicleControl.Models;

namespace VehicleControl.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<VehicleModel> Vehicles { get; set; }
        public DbSet<DriverModel> Drivers { get; set; }
        public DbSet<RentalModel> Rentals { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }
    }
}
