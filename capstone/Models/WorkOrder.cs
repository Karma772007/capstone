using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class WorkOrder
    {
        [Key]
        public int WorkOrderID { get; set; }
        public int MachineID { get; set; }
        public Machine Machine { get; set; }
        public string TaskDetails { get; set; }
        public string Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
