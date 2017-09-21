using aehyok.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;

namespace aehyok.SignalR.Client.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ChatController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: Chat
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Personal()
        {
            var userName = HttpContext.User.Identity.Name;
            var userList = _userManager.Users.Where(item => item.UserName != userName).ToList();
            return View(userList);
        }
    }
}