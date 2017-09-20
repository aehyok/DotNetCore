using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;

namespace aehyok.Users.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Personal()
        {
            return View();
        }
    }
}