using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aehyok.Users.Models;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin;

namespace aehyok.Users.Infrastructure
{
    /// <summary>
    /// 用户管理器，继承自Microsoft.AspNet.Identity空间的UserManager
    /// </summary>
    //public class AppUserManager : UserManager<AppUser>
    //{

    //    public AppUserManager(IUserStore<AppUser> store)
    //        : base(store)
    //    {
    //    }

    //    public static AppUserManager Create(
    //            IdentityFactoryOptions<AppUserManager> options,
    //            IOwinContext context)
    //    {
    //        //通过Owin获取EF的DbContext
    //        AppIdentityDbContext db = context.Get<AppIdentityDbContext>();

    //        AppUserManager manager = new AppUserManager(new UserStore<AppUser>(db));

    //        //CustomPasswordValidator重写密码验证规则，然后进行实例化
    //        manager.PasswordValidator = new CustomPasswordValidator
    //        {
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = false,
    //            RequireDigit = false,
    //            RequireLowercase = true,
    //            RequireUppercase = true
    //        };

    //        //UserName和Email Identity都已经封装好。
    //        //manager.UserValidator = new CustomUserValidator(manager)
    //        //{
    //        //    AllowOnlyAlphanumericUserNames = true,   //启用用户名为大小写字母 以及数字
    //        //    RequireUniqueEmail = true                //验证邮箱的唯一性
    //        //};
    //        return manager;
    //    }
    //}
}