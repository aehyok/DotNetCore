using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Mvc;
using aehyok.Users.Infrastructure;
using aehyok.Users.Models;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace aehyok.SignalR.Client.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        public ActionResult Index()
        {
            return View(_userManager.Users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
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
                return View("Error", new string[] { "User Not Found" });
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }



        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                //IdentityResult validEmail
                //    =  await _userManager.(user); //UserManager.UserValidator.ValidateAsync(user);
                //if (!validEmail.Succeeded)
                //{
                //    AddErrorsFromResult(validEmail);
                //}
                if (password != string.Empty)
                {

                    user.PasswordHash =
                        _userManager.PasswordHasher.HashPassword(user, password);
                }
                if (password != string.Empty)
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(user);
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