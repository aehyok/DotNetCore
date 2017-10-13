using aehyok.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using aehyok.Model;
using aehyok.Core.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace aehyok.Services
{
    public class SystemService : ISystemContract
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly CodeFirstDbContext _dbContext;


        public SystemService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, CodeFirstDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public List<ApplicationRole> GetRoleList()
        {
            var roleList = _roleManager.Roles;
            foreach (var role in roleList)
            {
                //var userIds = _dbContext.UserRoleswhere.where.(item => item.RoleId == role.Id).Select(item => item.UserId).ToArray();
                //role.Users = string.Join(',', _userManager.Users.Where(item => userIds.Contains(item.Id)).Select(item => item.UserName).ToArray());
            }
            return null ;
        }
    }
}
