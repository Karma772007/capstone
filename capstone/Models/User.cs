using System.ComponentModel.DataAnnotations;

namespace capstone.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int Phone { get; set; }
        public string Role { get; set; }
    }
}
