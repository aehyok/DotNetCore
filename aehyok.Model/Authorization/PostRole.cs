using aehyok.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Model.Authorization
{
    /// <summary>
    /// 岗位角色关联表
    /// </summary>
    public class PostRole: EntityBase<int>
    {
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
}
}
