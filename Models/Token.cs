using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;

namespace Tokens.Models
{
    public class TokenReadDto
    {
        public TokenReadDto()
        {

        }

        public TokenReadDto(int usrId)
        {

            UserID = usrId;
            TokenString = Guid.NewGuid().ToString();
            ExpiryDate = DateTime.Now.AddMinutes(30);
        }
                
        [Key]
        public int TokenID {get; set;}
        
        public int UserID { get; }
        [Required]
        public string TokenString { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }

        [ForeignKey(nameof(UserID))]
        [InverseProperty("Tokens")]
        public virtual User User { get; set; }
    }
}
