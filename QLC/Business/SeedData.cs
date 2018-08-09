﻿using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using User.Models;

namespace Business
{
    public static class SeedData
    {
        public static void Seeding(UserManager<Users> userManager, RoleManager<Roles> roleManager, UserDbContext context)
        {
            SeedRoles(roleManager, context);
            SeedUsers(userManager, context);
        }
        public static void SeedRoles(RoleManager<Roles> roleManager, UserDbContext context)
        {
            if (!roleManager.RoleExistsAsync("Requester").Result)
            {
                Roles role = new Roles()
                {
                    Name = "Requester",
                    Description = "Request",
                    IsAdmin = false,
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Approver").Result)
            {
                Roles role = new Roles()
                {
                    Name = "Approver",
                    Description = "Approval",
                    IsAdmin = false,
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                Roles role = new Roles()
                {
                    Name = "Administrator",
                    Description = "Admin",
                    IsAdmin = true,
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            context.SaveChanges();
        }

        public static void SeedUsers(UserManager<Users> userManager, UserDbContext context)
        {
            if (userManager.FindByNameAsync("Administrator").Result == null)
            {
                Users user = new Users()
                {
                    UserName = "Administrator",
                    Email = "admin@nccsoft.vn"
                };

                IdentityResult result = userManager.CreateAsync(user, "Password1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator");
                }
                context.SaveChanges();
            }
        }
    }
}