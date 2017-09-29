using aehyok.NLog;
using aehyok.Users.SignalR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.SignalR.Server
{
    //User.Identity.Name
    //https://stackoverflow.com/questions/22002092/context-user-identity-name-is-null-with-signalr-2-x-x-how-to-fix-it/22028296#22028296
    public class GroupChat:Hub
    {
        private static LogWriter Logger = new LogWriter();
        public static GroupContext Db = new GroupContext();

        /// <summary>
        /// 客户端与服务端进行握手 产生链接
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            Logger.Info("OnConnectedAsync");
            // 查询用户。
            var user = Db.Users.SingleOrDefault(u => u.ConnectionId == Context.ConnectionId);

            //判断用户是否存在,否则添加
            if (user == null)
            {
                user = new GroupContext.User()
                {
                    ConnectionId = Context.ConnectionId
                };
                Db.Users.Add(user);
            }

            //发送房间列表
            var roomList = from a in Db.Rooms
                       select new { a.RoomName };
            //Clients.Client(this.Context.ConnectionId).getRoomlist(JsonConvert.SerializeObject(itme.ToList()));
            await Clients.Client(this.Context.ConnectionId).InvokeAsync("GetRoomlist",(JsonConvert.SerializeObject(roomList.ToList())));
        }

        public async Task OnConnectionedAfter(string userName)
        {
            Logger.Info("OnConnectionedAfter");
            var user = Db.Users.SingleOrDefault(item => item.ConnectionId == Context.ConnectionId);
            user.UserName = userName;
        }

        /// <summary>
        /// 更新所有用户的房间列表
        /// </summary>
        private async Task GetRoomList()
        {
            Logger.Info("GetRoomList");
            var itme = from a in Db.Rooms
                       select new { a.RoomName };
            string jsondata = JsonConvert.SerializeObject(itme.ToList());
            await Clients.All.InvokeAsync("GetRoomlist",jsondata);
        }

        /// <summary>
        /// 创建聊天室
        /// </summary>
        /// <param name="roomName"></param>
        public async Task CreateRoom(string roomName)
        {
            Logger.Info("CreateRoom");
            var room = Db.Rooms.Find(a => a.RoomName == roomName);
            if (room == null)
            {
                GroupContext.ConversationRoom cr = new GroupContext.ConversationRoom()
                {
                    RoomName = roomName
                };
                //将房间加入列表
                Db.Rooms.Add(cr);
                await AddToRoom(roomName);
                await Clients.Client(Context.ConnectionId).InvokeAsync("ShowMessage","房间创建完成!");
                await GetRoomList();
            }
            else
            {
                await Clients.Client(Context.ConnectionId).InvokeAsync("ShowMessage","房间名重复!");
            }
        }

        /// <summary>
        /// 给分组内所有的用户发送消息
        /// </summary>
        /// <param name="room">分组名</param>
        /// <param name="message">信息</param>
        public void SendMessage(string room, string message)
        {
            Logger.Info("SendMessage");
            var user = Db.Users.FirstOrDefault(item => item.ConnectionId == Context.ConnectionId);
            var userName = user.UserName;
            var obj = new
            {
                UserName= userName,
                RoomName = room,
                Content = message,
                SendTime = DateTime.Now.ToString()
            };
            Clients.Group(room).InvokeAsync("ReceiveMessage", obj);
        }

        /// <summary>
        /// 加入聊天室
        /// </summary>
        /// <param name="roomName"></param>
        public async Task AddToRoom(string roomName)
        {
            Logger.Info("AddToRoom");
            //查询聊天室
            var room = Db.Rooms.Find(a => a.RoomName == roomName);
            //存在则加入
            if (room != null)
            {
                //查找房间中是否存在此用户
                var isuser = room.Users.FirstOrDefault(a => a.ConnectionId == Context.ConnectionId);
                //不存在则加入
                if (isuser == null)
                {
                    var user = Db.Users.Find(a => a.ConnectionId == Context.ConnectionId);
                    user.Rooms.Add(room);
                    room.Users.Add(user);
                    await Groups.AddAsync(Context.ConnectionId, roomName);
                    //调用此连接用户的本地JS(显示房间)
                    await Clients.Client(Context.ConnectionId).InvokeAsync("AddRoom",roomName);
                }
                else
                {
                    await Clients.Client(Context.ConnectionId).InvokeAsync("ShowMessage",roomName);
                }
            }
        }

        /// <summary>
        /// 断开链接 移除当前用户 退出聊天室
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Logger.Info("OnDisconnectedAsync");
            var user = Db.Users.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);

            //判断用户是否存在,存在则删除
            if (user != null)
            {
                //删除用户
                Db.Users.Remove(user);
                // 循环用户的房间,删除用户
                foreach (var item in user.Rooms)
                {
                    await RemoveFromRoom(item.RoomName);
                }
            }
        }

        /// <summary>
        /// 退出聊天室
        /// </summary>
        /// <param name="roomName"></param>
        public async Task RemoveFromRoom(string roomName)
        {
            Logger.Info("RemoveFromRoom");
            //查找房间是否存在
            var room = Db.Rooms.Find(a => a.RoomName == roomName);
            //存在则进入删除
            if (room != null)
            {
                //查找要删除的用户
                var user = room.Users.FirstOrDefault(a => a.ConnectionId == Context.ConnectionId);
                //移除此用户
                room.Users.Remove(user);
                //如果房间人数为0,则删除房间
                if (room.Users.Count <= 0)
                {
                    Db.Rooms.Remove(room);

                }
                await Groups.RemoveAsync(Context.ConnectionId, roomName);
                //提示客户端
                await Clients.Client(Context.ConnectionId).InvokeAsync("removeRoom",("退出成功!"));
            }
        }
    }
}
