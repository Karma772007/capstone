using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class MaintenanceHistory
    {
        [Key]
        public int HistoryID { get; set; }

        [Required(ErrorMessage = "Machine ID required")]
        public int MachineID { get; set; }

        [Required(ErrorMessage = "Repair details required")]
        public string RepairDetails { get; set; }

        [Required(ErrorMessage = "Completion date required")]
        public DateTime? CompletionDate { get; set; }
    }
}