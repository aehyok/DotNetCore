﻿using aehyok.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using aehyok.Model;
using aehyok.Core.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using System.Linq;
using aehyok.Core;

namespace aehyok.Services
{
    /// <summary>
    /// 系统管理服务
    /// </summary>
    public class SystemService : ISystemContract
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly CodeFirstDbContext _dbContext;


        public SystemService( 
            CodeFirstDbContext dbContext)
        {
            //_userManager = userManager;
            //_roleManager = roleManager;
            _dbContext = dbContext;
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public List<ApplicationRole> GetRoleList()
        {
            var roleList = _dbContext.Roles.ToList();
            foreach (var role in roleList)
            {
                var userIds = _dbContext.UserRoles.Where(item => item.RoleId == role.Id).Select(item => item.UserId).ToArray();
                role.Users = string.Join(',', _dbContext.Users.Where(item => userIds.Contains(item.Id)).Select(item => item.UserName).ToArray());
            }
            return roleList;
        }

        public int GetRoleListCount()
        {
            var count = _dbContext.Roles.Count();
            return count;
        }
    }
}
