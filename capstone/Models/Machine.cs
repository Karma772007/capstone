using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class Machine
    {
        [Key]
        public int MachineId { get; set; }

        [Required(ErrorMessage = "Machine Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Serial Number is required.")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } // Active, Inactive, Maintenance

        public DateTime LastMaintenanceDate { get; set; }

        public ICollection<MaintenanceHistory> MaintenanceHistory { get; set; } = new List<MaintenanceHistory>();
        public ICollection<CleaningLog> CleaningLogs { get; set; } = new List<CleaningLog>();
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
    }
}