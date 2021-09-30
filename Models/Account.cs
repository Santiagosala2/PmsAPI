using Entities.Models;
using Resources.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Models
{
    public class Account
    {

        [Key]
        public int AccountID { get; set; }
        [Required]
        public int ResourceID { get; set; }

        [Required]
        public int EntityID { get; set; }

        [ForeignKey(nameof(ResourceID))]
        [InverseProperty("Accounts")]
        public virtual Resource Resource { get; set; }

        [ForeignKey(nameof(EntityID))]
        [InverseProperty("Accounts")]
        public virtual Entity Entity { get; set; }

    }
}
