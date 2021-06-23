using System.ComponentModel.DataAnnotations;

namespace Resources.Dtos
{
    public class ResourceUpdateDto
   {
        [Required]
        public string Account { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}