using System.ComponentModel.DataAnnotations;

namespace Users.Dtos
{
    public class UserCreateDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}