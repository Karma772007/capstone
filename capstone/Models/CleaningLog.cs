using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class CleaningLog
    {
        [Key]
        public int LogID { get; set; }
        public int MachineID { get; set; }
        public Machine Machine { get; set; }
        public DateTime CleaningDate { get; set; }
        public string CleaningMethod { get; set; }
        public string Status { get; set; }
    }
}
