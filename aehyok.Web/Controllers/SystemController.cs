using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Web.Controllers
{
    public class SystemController : Controller
    {
        public IActionResult RoleIndex()
        {
            return View();
        }

        public IActionResult UserIndex()
        {
            return View();
        }

        public IActionResult MenuIndex()
        {
            return View();
        }
    }
}