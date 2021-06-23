using System.ComponentModel.DataAnnotations;

namespace Resources.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Account { get; set; }
        
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}