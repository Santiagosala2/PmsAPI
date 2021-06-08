using System.ComponentModel.DataAnnotations;

namespace Resources.Dtos
{
    public class ResourceCreateDto
   {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}