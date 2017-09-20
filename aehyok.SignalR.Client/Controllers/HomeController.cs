using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aehyok.SignalR.Client.Models;
using Microsoft.AspNetCore.Authorization;

namespace aehyok.SignalR.Client.Controllers
{

    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View(GetData("Index"));
        }

        [Authorize(Roles = "aehyok")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict
                = new Dictionary<string, object>();
            dict.Add("Action", actionName);
            dict.Add("User", HttpContext.User.Identity.Name);
            dict.Add("Authenticated", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Auth Type", HttpContext.User.Identity.AuthenticationType);
            dict.Add("In Users Role", HttpContext.User.IsInRole("Users"));
            return dict;
        }

        [Authorize]
        public ActionResult UserProps()
        {
            //return View(CurrentUser);
            return View();
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<ActionResult> UserProps(Cities city)
        //{
        //    AppUser user = CurrentUser;
        //    user.City = city;
        //    user.SetCountryFromCity(city);
        //    await UserManager.UpdateAsync(user);
        //    return View(user);
        //}

        //private AppUser CurrentUser
        //{
        //    get
        //    {
        //        return UserManager.FindByName(HttpContext.User.Identity.Name);
        //    }
        //}

        //private AppUserManager UserManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
        //    }
        //}
    }
}
