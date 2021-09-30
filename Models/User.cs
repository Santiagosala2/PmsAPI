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
        public string Password { get; set; }

        [InverseProperty(nameof(User))]
        public virtual Entity Entity { get; set; }

        [InverseProperty(nameof(User))]
        public virtual ICollection<Token> Tokens { get; set; }

        public User ()
        {
            Tokens = new HashSet<Token>();
        }
    }


}

