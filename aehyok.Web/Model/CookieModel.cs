using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Web.Model
{
    /// <summary>
    /// Cookie中存储的对象
    /// </summary>
    public class CookieModel
    {
        /// <summary>
        /// 暂时的用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 验证Token
        /// </summary>
        public string Token { get; set; }
    }
}
