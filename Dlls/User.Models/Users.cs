using Microsoft.AspNetCore.Identity;
using System;

namespace User.Models
{
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
