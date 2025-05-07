using Microsoft.EntityFrameworkCore;
using capstone.Models;
namespace capstone.data
{
    public class Projectcontext:DbContext
    {
        public Projectcontext(DbContextOptions<Projectcontext> options) : base(options) { 

        }
        public DbSet<User>Users { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<CleaningLog> Cleaninglogs { get; set; }
        public DbSet<MaintenanceHistory> Maintenancehistories { get; set; }
        public DbSet<SparePartInventory> Sparepartinventories { get; set; }
        public DbSet<WorkOrder> Workorders { get; set; }
    }
}
