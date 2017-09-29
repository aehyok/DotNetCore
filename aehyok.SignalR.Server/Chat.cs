using aehyok.NLog;
using aehyok.Users.SignalR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.SignalR.Server
{
    public class Chat : Hub
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private static LogWriter Logger = new LogWriter();

        /// <summary>
        /// 用户列表
        /// </summary>
        public static List<UserInfo> UserList = new List<UserInfo>();

        /// <summary>
        /// 客户端开始与服务端进行握手 产生链接
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            Logger.Info("OnConnectedAsync");
            var user = UserList.SingleOrDefault(item => item.ConnectionId == Context.ConnectionId);  //查找已认证的用户是否存在于用户列表
            if (user != null)
            {
                user.ConnectionId = Context.ConnectionId;
            }
            else
            {
                // 查询用户。
                user = UserList.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);
                //判断用户是否存在,否则添加进集合
                if (user == null)
                {
                    user = new UserInfo()
                    {
                        UserName = Context.User.Identity.Name,
                        ConnectionId = Context.ConnectionId
                    };
                    UserList.Add(user);
                }
            }
            List<string> list = new List<string>
            {
                Context.ConnectionId
            };
            await Clients.Client(Context.ConnectionId).InvokeAsync("OnConnectionedMe", $"{Context.ConnectionId}");
        }

        /// <summary>
        /// 客户端与服务端握手后的处理（更新用户信息，以及通知在线用户）
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task OnConnectionedAfter(UserInfo parameter)
        {
            Logger.Info("OnConnectionedAfter");
            var user=UserList.SingleOrDefault(item => item.ConnectionId == parameter.ConnectionId);
            user.UserName = parameter.UserName;

            List<string> list = new List<string>
            {
                Context.ConnectionId
            };
            await Clients.All.InvokeAsync("OnConnectionedExcept", UserList);
        }

        /// <summary>
        /// 断开链接时在在线用户列表中移除用户
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            Logger.Info("OnDisconnectedAsync");
            var user = UserList.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);

            //判断用户是否存在,存在则删除
            if (user != null)
            {
                UserList.Remove(user);
            }
            await Clients.All.InvokeAsync("OnDisconneted", $"{Context.ConnectionId}");
        }

        /// <summary>
        /// 发送消息并通知接收用户接收消息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task SendMessage(MessageContext context)
        {
            Logger.Info("SendMessage");
            context.SendTime = DateTime.Now.ToString();
            var user = UserList.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
            context.UserName = user.UserName;
            context.ReceiveConnectionId = context.ReceiveConnectionId;
            //判断用户是否存在,存在则发送
            if (user != null)
            {
                //给指定用户发送,把自己的ID传过去
                await Clients.Client(context.ReceiveConnectionId).InvokeAsync("ReceiveMessage", context);  //接收用户接收消息
                await Clients.Client(Context.ConnectionId).InvokeAsync("ReceiveMessage", context);         //将消息发送给自己（此处也可以直接通过前台JS变更消息记录）
            }
        }
    }
}
