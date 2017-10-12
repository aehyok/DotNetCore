using aehyok.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using aehyok.Model;

namespace aehyok.Services
{
    public class SystemService : ISystemContract
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SystemService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<ApplicationRole> GetRoleList()
        {
            return null;
        }
    }
}
