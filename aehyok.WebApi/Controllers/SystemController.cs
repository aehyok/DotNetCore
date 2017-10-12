using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using aehyok.Core.Data;

namespace aehyok.WebApi.Controllers
{
    /// <summary>
    /// 系统管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/System")]
    public class SystemController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]    
        [Route("Role")]
        public dynamic RoleList()
        {
            var roleList = _roleManager.Roles;
            foreach (var role in roleList)
            {
                //var userIds = _appIdentityDbContext.UserRoles.Where(item => item.RoleId == role.Id).Select(item => item.UserId).ToArray();
                //role.Users = string.Join(',', _userManager.Users.Where(item => userIds.Contains(item.Id)).Select(item => item.UserName).ToArray());
            }
            return roleList;
        }
    }
}