using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class Machine
    {
        [Key]
        public int MachineId { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public string LastMaintenanceStatus { get; set; }

        public string MachinePhotoURL { get; set; }
        public ICollection<MaintenanceHistory> MaintenanceHistory { get; set; }
        public ICollection<CleaningLog> CleaningLogs { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; }


    }
}
