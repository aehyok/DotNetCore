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
using aehyok.Contracts;
using aehyok.WebApi.ViewModel;

namespace aehyok.WebApi.Controllers
{
    /// <summary>
    /// 系统管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SystemController : BaseController
    {
        private readonly ISystemContract _systemService;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemController(ISystemContract systemService)
        {
            _systemService = systemService;
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]    
        [Route("Role/{pageIndex:int}/{pageSize:int}")]
        public RoleModel RoleList(int pageIndex, int pageSize)
        {
            var roleList = _systemService.GetRoleList();
            RoleModel model = new RoleModel()
            {
                RoleList = roleList,
                Count = _systemService.GetRoleListCount()
            };
            return model;
        }
    }
}