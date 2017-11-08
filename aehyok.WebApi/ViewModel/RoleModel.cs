using aehyok.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.WebApi.ViewModel
{
    public class RoleModel
    {
        /// <summary>
        /// 文章List
        /// </summary>
        public List<ApplicationRole> RoleList { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public RoleModel()
        {
            Count = 0;
        }
    }
}
