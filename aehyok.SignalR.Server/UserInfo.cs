using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aehyok.Users.SignalR
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        [Key]
        public string ConnectionId { get; set; }
        public string UserName { get; set; }

        //此处可以考虑添加当前与谁聊天的用户信息（后期考虑添加）
    }
}