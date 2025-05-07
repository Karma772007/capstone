using Microsoft.EntityFrameworkCore;
using capstone.Models;
namespace capstone.data
{
    public class Projectcontext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Cycle;Integrated Security=True;Encrypt=False;TrustServerCertificate=True");
        }

        public DbSet<User>Users { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<CleaningLog> Cleaninglogs { get; set; }
        public DbSet<MaintenanceHistory> Maintenancehistories { get; set; }
        public DbSet<SparePartInventory> Sparepartinventories { get; set; }
        public DbSet<WorkOrder> Workorders { get; set; }





    }
}
