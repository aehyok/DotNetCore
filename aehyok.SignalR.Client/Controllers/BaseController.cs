using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace aehyok.SignalR.Client.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                ViewData["UserName"] = context.HttpContext.User.Identity.Name;
            }
            base.OnActionExecuting(context);
        }
    }
}