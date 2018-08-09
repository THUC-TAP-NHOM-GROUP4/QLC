using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLC.Models.UserViewModel;
using User.Models;

namespace QLC.Controllers.UserModelsControllers
{
    [Authorize(Roles ="Administrator")]
    public class UserController : Controller
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<Roles> roleManager;

        public UserController(UserManager<Users> _userManager, RoleManager<Roles> _roleManager)
        {
            this.userManager = _userManager;
            this.roleManager = _roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<UserListViewModel> model = new List<UserListViewModel>();
            model = userManager.Users.Select(u => new UserListViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email,
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            UserViewModel model = new UserViewModel();
            model.ApplicationRoles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();
            return PartialView("_AddUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    PasswordHash = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                if (model.Password == model.ConfirmPassword && !string.IsNullOrEmpty(model.Password))
                {
                    IdentityResult result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        Roles Roles = await roleManager.FindByIdAsync(model.ApplicationRoleId);
                        if (Roles != null)
                        {
                            IdentityResult roleResult = await userManager.AddToRoleAsync(user, Roles.Name);
                            if (roleResult.Succeeded)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return RedirectToAction("_AddUser", model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            EditUserViewModel model = new EditUserViewModel();
            model.ApplicationRoles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            }).ToList();

            if (!String.IsNullOrEmpty(id))
            {
                Users user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    model.FirstName = user.FirstName;
                    model.Email = user.Email;
                    model.ApplicationRoleId = roleManager.Roles.Single(r => r.Name == userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }
            return PartialView("_EditUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.Email = model.Email;
                    string existingRole = userManager.GetRolesAsync(user).Result.Single();
                    string existingRoleId = roleManager.Roles.Single(r => r.Name == existingRole).Id;
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (existingRoleId != model.ApplicationRoleId)
                        {
                            IdentityResult roleResult = await userManager.RemoveFromRoleAsync(user, existingRole);
                            if (roleResult.Succeeded)
                            {
                                Roles Roles = await roleManager.FindByIdAsync(model.ApplicationRoleId);
                                if (Roles != null)
                                {
                                    IdentityResult newRoleResult = await userManager.AddToRoleAsync(user, Roles.Name);
                                    if (newRoleResult.Succeeded)
                                    {
                                        return RedirectToAction("Index");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return PartialView("_EditUser", model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            string email = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                Users Users = await userManager.FindByIdAsync(id);
                if (Users != null)
                {
                    email = Users.Email;
                }
            }
            return PartialView("_DeleteUser", email);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id, IFormCollection form)
        {
            if (!String.IsNullOrEmpty(id))
            {
                Users Users = await userManager.FindByIdAsync(id);
                if (Users != null)
                {
                    IdentityResult result = await userManager.DeleteAsync(Users);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
    }
}