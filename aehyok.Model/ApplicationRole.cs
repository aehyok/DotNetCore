using aehyok.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace aehyok.Model
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public class ApplicationRole:IdentityRole
    {
        [NotMapped]
        public string Users { get; set; }
    }
}
