using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.SignalR.Server
{
    /// <summary>
    /// 消息传递
    /// </summary>
    public class MessageContext
    {
        /// <summary>
        /// 发送人链接ID
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
    }
}
