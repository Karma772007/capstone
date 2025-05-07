using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class CleaningLog
    {
        [Key]
        public int LogID { get; set; }

        [Required(ErrorMessage = "The Machine field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid machine.")]
        [Display(Name = "Machine")]
        public int MachineID { get; set; }

        public Machine Machine { get; set; }

        [Required(ErrorMessage = "The Cleaning Date field is required.")]
        [Display(Name = "Cleaning Date")]
        public DateTime CleaningDate { get; set; }

        [Required(ErrorMessage = "The Cleaning Method field is required.")]
        [Display(Name = "Cleaning Method")]
        public string CleaningMethod { get; set; }

        [Required(ErrorMessage = "The Status field is required.")]
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}