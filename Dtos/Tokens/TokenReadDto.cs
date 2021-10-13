using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Users.Models;

namespace Tokens.Dtos
{
    public class TokenReadDto
    {
        public string TokenString { get; set; }

    }
}
