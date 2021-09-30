using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Groups.Models
{
    public class Group
    {
        [Key]
        public int GroupID { get; set; }
        [Required]
        public string Name { get; set; }

        [InverseProperty(nameof(Group))]
        public virtual Entity Entity { get; set; }


    }
}
