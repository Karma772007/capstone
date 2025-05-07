using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class SparePartInventory
    {
        [Key]
        public int PartID { get; set; }
        public string PartName { get; set; }
        public int QuantityInStock { get; set; }
        public int MinimumRequired {  get; set; }
        public string ReorderStatus { get; set; }
    }
}
