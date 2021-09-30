using Accounts.Models;
using Groups.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;

namespace Entities.Models
{
    public class Entity
    {
        [Key]
        public int EntityID { get; set; }
        public int? UserID { get; set; }

        public int? GroupID { get; set; }

        [ForeignKey(nameof(GroupID))]
        [InverseProperty(nameof(Entity))]
        public virtual Group Group { get; set; }

        [ForeignKey(nameof(UserID))]
        [InverseProperty(nameof(Entity))]
        public virtual User User { get; set; }

        [InverseProperty(nameof(Entity))]
        public virtual ICollection<Account> Accounts { get; set; }

        public Entity()
        {
            Accounts = new HashSet<Account>();
        }
    }
}
