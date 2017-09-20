using aehyok.Users.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace aehyok.Users.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "Access Denied" });
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// ValidateAntiForgeryToken注解属性，该属性与视图中的Html.AntiForgeryToken辅助器方法联合工作，防止Cross-Site Request Forgery（CSRF，跨网站请求伪造）的攻击
        /// </summary>
        /// <param name="details"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            //var appuser = new AppUser();
            //appuser.Id = "1";
            //appuser.UserName = "aehyok";
            //appuser.Email = "aehyok@vip.qq.com";
            //var results=await _userManager.CreateAsync(appuser, "Aehyok_Test0");


            if (ModelState.IsValid)
            {
                //检查用户是否存在
                var result = await _signInManager.PasswordSignInAsync(details.Name,
                            details.Password,false,false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "错误的用户名或密码");
                }
                else
                {

                    //登录用户检查通过后生成ClaimsIdentity对象
                    var user = await _userManager.FindByNameAsync(details.Name);
                    var ident = await _signInManager.CreateUserPrincipalAsync(user);
                    await _signInManager.SignOutAsync();

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    //https://digitalmccullough.com/posts/aspnetcore-auth-system-demystified.html

                    //await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, ident);

                    return RedirectToAction("Index","Home");
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(details);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult GoogleLogin(string returnUrl)
        //{
        //    var properties = new AuthenticationProperties
        //    {
        //        RedirectUri = Url.Action("GoogleLoginCallback",
        //             new { returnUrl = returnUrl })
        //    };
        //    //HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
        //    return new HttpUnauthorizedResult();
        //}

        //[AllowAnonymous]
        //public async Task<ActionResult> GoogleLoginCallback(string returnUrl)
        //{
        //    ExternalLoginInfo loginInfo = await AuthManager.GetExternalLoginInfoAsync();
        //    AppUser user = await UserManager.FindAsync(loginInfo.Login);
        //    if (user == null)
        //    {
        //        user = new AppUser
        //        {
        //            Email = loginInfo.Email,
        //            UserName = loginInfo.Email.Split('@')[0],
        //            City = Cities.London,
        //            Country = Countries.Uk
        //        };
        //        IdentityResult result = await UserManager.CreateAsync(user);
        //        if (!result.Succeeded)
        //        {
        //            return View("Error", result.Errors);
        //        }
        //        else
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
        //            if (!result.Succeeded)
        //            {
        //                return View("Error", result.Errors);
        //            }
        //        }
        //    }

        //    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
        //        DefaultAuthenticationTypes.ApplicationCookie);
        //    ident.AddClaims(loginInfo.ExternalIdentity.Claims);
        //    AuthManager.SignIn(new AuthenticationProperties
        //    {
        //        IsPersistent = false
        //    }, ident);
        //    return Redirect(returnUrl ?? "/");
        //}

        [Authorize]
        public ActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //private IAuthenticationManager AuthManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}
        //private AppUserManager UserManager
        //{
        //    get
        //    {
        //        //通过Owin及其扩展方法来获取AppUserManager用户管理器
        //        return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
        //    }
        //}
    }
}