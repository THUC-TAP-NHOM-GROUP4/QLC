using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Models
{
    public class Roles : IdentityRole
    {
        public bool IsSystemAdmin { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
