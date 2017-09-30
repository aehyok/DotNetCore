using aehyok.Users.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Mvc;
using aehyok.Users.Models;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using aehyok.SignalR.Client.Controllers;
using System.Threading;

namespace aehyok.SignalR.Client.Controllers
{
    [Authorize]
    public class RoleAdminController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppIdentityDbContext _appIdentityDbContext;


        public RoleAdminController(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager, AppIdentityDbContext appIdentityDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appIdentityDbContext = appIdentityDbContext;
        }


        public ActionResult Index()
        {
            var roleList = _roleManager.Roles;
            foreach(var role in roleList)
            {
                var userIds = _appIdentityDbContext.UserRoles.Where(item => item.RoleId == role.Id).Select(item=>item.UserId).ToArray();
                role.Users =string.Join( ',',_userManager.Users.Where(item => userIds.Contains(item.Id)).Select(item => item.UserName).ToArray());
            }

            return View(roleList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result
                   = await _roleManager.CreateAsync(new AppRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        public async Task<ActionResult> Edit(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            string[] memberIDs = _appIdentityDbContext.UserRoles.Where(item => item.RoleId == role.Id).Select(Item=>Item.UserId).ToArray(); //null; //role.Users?.Select(x => x.Id).ToArray(); ;
            IEnumerable<AppUser> members
                    = _userManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            IEnumerable<AppUser> nonMembers = _userManager.Users.Except(members);
            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {

                    var userRole = new IdentityUserRole<string>
                    {
                        RoleId = model.RoleId,
                        UserId = userId
                    };

                    await _appIdentityDbContext.UserRoles.AddAsync(userRole);
                    await _appIdentityDbContext.SaveChangesAsync();
                }

                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    var userRole = new IdentityUserRole<string>
                    {
                        RoleId = model.RoleId,
                        UserId = userId
                    };
                    _appIdentityDbContext.UserRoles.Remove(userRole);
                    _appIdentityDbContext.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View("Error", new string[] { "Role Not Found" });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Role Not Found" });
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}