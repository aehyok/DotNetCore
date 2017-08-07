using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using aehyok.Web.Model;

namespace aehyok.Web.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string userName = string.Empty;
            string ticket = string.Empty;
            if (filterContext.ActionArguments.Count > 0)
            {
                ticket = filterContext.ActionArguments["ticket"] as string;
                if (!string.IsNullOrEmpty(ticket))
                {
                    userName = filterContext.ActionArguments["userName"] as string;
                    CookieModel model = new CookieModel();
                    model.Token = ticket;
                    model.UserName = userName;
                    //.net core  cookies
                    //string json = JsonConvert.SerializeObject(model, Formatting.Indented);
                    //HttpCookie recookie = new HttpCookie("Token", model.Token);
                    //recookie.Expires = DateTime.Now.AddDays(7);
                    //Response.Cookies.Add(recookie);
                    //HttpCookie userCookie = new HttpCookie("User", model.UserName);
                    //userCookie.Expires = DateTime.Now.AddDays(7);
                    //Response.Cookies.Add(userCookie);
                }
            }
            var cookie = Request.Cookies["Token"];
            //if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            //{
            //    string sessionTicket = cookie.Value;
            //    userName = Request.Cookies["User"].Value;
            //    if (!string.IsNullOrEmpty(ticket) || !string.IsNullOrEmpty(sessionTicket))
            //    {
            //        ViewBag.Ticket = ticket ?? sessionTicket;
            //        ViewBag.UserName = userName;
            //        base.OnActionExecuting(filterContext);
            //    }
            //}
            //else
            //{
            //    var url = "http://" + Request.Url.Authority + "/Account/Login";
            //    filterContext.Result = new RedirectResult(url);
            //    return;
            //}
        }
    }
}