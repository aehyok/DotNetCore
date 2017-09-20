using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace aehyok.Users.Infrastructure
{
    //public class CustomPasswordValidator : PasswordValidator
    //{
    //    /// <summary>
    //    /// 验证密码规则，此处作为演示测试（具体可根据项目需要进行相应的改造）
    //    /// </summary>
    //    /// <param name="pass"></param>
    //    /// <returns></returns>
    //    public override async Task<IdentityResult> ValidateAsync(string pass)
    //    {
    //        IdentityResult result = await base.ValidateAsync(pass);
    //        if (pass.Contains("aehyok"))
    //        {
    //            var errors = result.Errors.ToList();
    //            errors.Add("密码中不能包含aehyok");
    //            result = new IdentityResult(errors);
    //        }
    //        return result;
    //    }
    //}
}