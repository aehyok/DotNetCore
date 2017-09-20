using aehyok.Users.Models;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Users.Infrastructure
{
    /// <summary>
    /// 验证用户信息规则，此处作为演示测试（具体可根据项目需要进行相应的改造）
    /// </summary>
    //public class CustomUserValidator : UserValidator<AppUser>
    //{
    //    public CustomUserValidator(AppUserManager mgr) : base(mgr)
    //    {
    //    }


    //    //对用户邮箱进行特殊的限制 （验证代码的有效性而已）
    //    public override async Task<IdentityResult> ValidateAsync(AppUser user)
    //    {
    //        IdentityResult result = await base.ValidateAsync(user);
    //        if (!user.Email.ToLower().EndsWith("@gmail.com"))
    //        {
    //            var errors = result.Errors.ToList();
    //            errors.Add("只有谷歌邮箱@gmail可作为注册邮箱");
    //            result = new IdentityResult(errors);
    //        }
    //        else //下面可以进行验证其他用户的基本属性
    //        {
                
    //        }
    //        return result;

    //    }
    //}
}
