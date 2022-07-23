using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.Models
{
    public class User : IdentityUser
    {
        public int YearOfBirth { get; set; }
    }
}
