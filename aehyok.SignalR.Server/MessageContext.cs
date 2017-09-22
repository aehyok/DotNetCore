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
        /// 发送人链接
        /// </summary>
        public string SendConnectionId { get; set; }

        /// <summary>
        /// 接收人链接
        /// </summary>
        public string ReceiveConnectionId { get; set; }

        /// <summary>
        /// 发送人消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送消息时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 发送人用户名
        /// </summary>
        public string UserName { get; set; }
    }
}
