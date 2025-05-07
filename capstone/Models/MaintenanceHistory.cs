using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class MaintenanceHistory
    {
        [Key]
        public int HistoryID { get; set; }
        public int MachineID { get; set; }
        public Machine Machine { get; set; }
        public string RepairDetails { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
