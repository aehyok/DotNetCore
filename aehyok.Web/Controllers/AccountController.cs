using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            //return Redirect("http://localhost:5000");
            return View("../Account/Login");
        }
    }
}