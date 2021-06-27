using System.ComponentModel.DataAnnotations;

namespace Resources.Dtos
{
    public class ResourceUpdateDto
   {
        [Required]
        public string Account { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Website { get; set; }
    }
}