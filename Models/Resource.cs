using Accounts.Models;
using Groups.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Users.Models;

namespace Resources.Models
{
    public class Resource
    {
        [Key]
        public int ResourceID { get; set; }

        [Required]
        public string Account { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Website { get; set; }

        [InverseProperty(nameof(Resource))]
        public virtual ICollection<Account> Accounts { get; set; }

        public Resource()
        {
            Accounts = new HashSet<Account>();
        }

    }
}