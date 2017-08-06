using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace aehyok.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        ///  π”√HttpContext
        /// </summary>
        private IHttpContextAccessor _accessor;
        public HomeController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        //public IActionResult Index()
        //{
        //    ViewData["Title"] = "aehyok";
        //    var context=_accessor.HttpContext;
        //    return View();
        //}

        public IActionResult Index(string userName, string ticket, string type, string menuId)
        {
            ViewBag.Type = type;
            ViewBag.UserName = userName;
            ViewBag.Title = "Home Page";
            ViewBag.MenuId = menuId;
            return View();
        }

        public IActionResult CallBack()
        {
            return View();
        }
    }
}