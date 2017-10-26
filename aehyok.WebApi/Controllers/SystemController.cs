using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using aehyok.Core.Data;
using aehyok.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using aehyok.Core.Data.Entity;

namespace aehyok.WebApi.Controllers
{
    /// <summary>
    /// 系统管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/System")]
    public class SystemController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly CodeFirstDbContext _dbContext;

        private readonly IServiceCollection _serviceCollection;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemController(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager, CodeFirstDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            
            this._dbContext = dbContext;
            //可直接通过CodeFirstDbContext来操作用户角色中间表
            //var data = _dbContext.UserRoles.Add(new IdentityUserRole<string>() { UserId = "111", RoleId = "111" });
            //_dbContext.SaveChanges();
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