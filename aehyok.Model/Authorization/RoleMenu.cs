using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Base;

namespace aehyok.Model.Authorization
{
    /// <summary>
    /// 角色与菜单关联表
    /// </summary>
    public class RoleMenu : EntityBase<int>
    {
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int MenuId { get; set; }

        public Menu Menu{ get; set; }
    }
}
