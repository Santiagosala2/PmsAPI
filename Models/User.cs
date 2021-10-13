using Groups.Models;
using Entities.Models;
using Resources.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Tokens.Models;

namespace Users.Models
{
    public class User
    {    
        [Key]
        public int UserID { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string SaltedHashedPassword { get; set; }

        [Required]
        public string Salt { get; set;}

        [InverseProperty(nameof(User))]
        public virtual Entity Entity { get; set; }

        [InverseProperty(nameof(User))]
        public virtual ICollection<TokenReadDto> Tokens { get; set; }

        public User ()
        {
            Tokens = new HashSet<TokenReadDto>();
        }
    }


}

