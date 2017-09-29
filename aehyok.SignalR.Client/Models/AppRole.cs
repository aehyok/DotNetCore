//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aehyok.Users.Models
{
    public class AppRole : IdentityRole
    {

        public AppRole() : base() { }

        public AppRole(string name) : base(name) { }

        //public ICollection<AppUser> Users { get; set; }

        public ICollection<AppMenu> Menus { get; set; }
    }
}