﻿using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Web;
using aehyok.SignalR.Client.Models;
using aehyok.Users.Models;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aehyok.Users.Infrastructure
{
    /// <summary>
    /// IdentityDbContext最终还是继承DbContext
    /// </summary>
    public class AppIdentityDbContext : IdentityDbContext<AppUser,AppRole,string>
    {

        public AppIdentityDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppRole>().HasMany(item => item.Menus);
            builder.Entity<AppMenu>().HasMany(item => item.Roles);

            builder.Entity<ApplicationLog>(m =>
            {
                m.HasKey(c => c.Id);
                m.Property(c => c.Application).IsRequired().HasMaxLength(50);
                m.Property(c => c.Level).IsRequired().HasMaxLength(50);
                m.Property(c => c.Message).IsRequired();
                m.Property(c => c.Logger).HasMaxLength(250);
            });

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}