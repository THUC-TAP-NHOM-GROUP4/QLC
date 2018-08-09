using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLC.Models.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using User.Models;

namespace QLC.Controllers.UserModelsControllers
{
    [Authorize(Roles="Administrator")]
    public class ApplicationRoleController : Controller
    {
        private readonly RoleManager<Roles> roleManager;
        private readonly UserManager<Users> userManager;
        private readonly UserDbContext userDb;

        public ApplicationRoleController(RoleManager<Roles> roleManager, UserDbContext context)
        {
            this.roleManager = roleManager;
            userDb = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ApplicationRoleListViewModel> model = new List<ApplicationRoleListViewModel>();
            model = roleManager.Roles.Select(r => new ApplicationRoleListViewModel
            {
                RoleName = r.Name,
                Description = r.Description,
                Id = r.Id,
                NumberOfUsers = userDb.UserRoles.Where(c => c.RoleId == r.Id).Count()
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddEditApplicationRole(string id)
        {
            ApplicationRoleViewModel model = new ApplicationRoleViewModel();
            if (!String.IsNullOrEmpty(id))
            {
                Roles applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    applicationRole = new Roles
                    {
                        Id = model.Id,
                        Name = model.IsAdmin ? "Administrator" : string.Empty,
                        Description = model.Description,
                        IsAdmin = model.IsAdmin,
                    };

                }
            }
            return PartialView("_AddEditApplicationRole", model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEditApplicationRole(string id, ApplicationRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Roles applicationRole = await roleManager.FindByIdAsync(id);
                    applicationRole.Description = model.Description;
                    applicationRole.IsAdmin = model.IsAdmin;
                    IdentityResult roleRuslt = applicationRole != null ? await roleManager.UpdateAsync(applicationRole) : await roleManager.CreateAsync(applicationRole);
                    if (roleRuslt.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteApplicationRole(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                Roles applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    name = applicationRole.Name;
                }
            }
            return PartialView("_DeleteApplicationRole", name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteApplicationRole(string id, IFormCollection form)
        {
            if (!String.IsNullOrEmpty(id))
            {
                Roles applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    IdentityResult roleRuslt = roleManager.DeleteAsync(applicationRole).Result;
                    if (roleRuslt.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
    }
}
