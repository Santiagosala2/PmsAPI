using System.ComponentModel.DataAnnotations;

namespace Resources.Dtos
{
    public class ResourceReadDto
   {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
   }
}