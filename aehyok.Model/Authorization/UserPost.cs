using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace aehyok.Model.Authorization
{
    /// <summary>
    /// 用户与岗位的关联表
    /// </summary>
    public class UserPost : EntityBase<int>
    {
        public int UserId { get; set; }

        public IdentityUser User { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
