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
    public class GroupChat:Hub
    {
        public static GroupContext Db = new GroupContext();

        public override async Task OnConnectedAsync()
        {
            // 查询用户。
            var user = Db.Users.SingleOrDefault(u => u.UserName == Context.ConnectionId);

            //判断用户是否存在,否则添加
            if (user == null)
            {
                user = new GroupContext.User()
                {
                    UserName = Context.ConnectionId
                };
                Db.Users.Add(user);
            }

            //发送房间列表
            var itme = from a in Db.Rooms
                       select new { a.RoomName };
            //Clients.Client(this.Context.ConnectionId).getRoomlist(JsonConvert.SerializeObject(itme.ToList()));
            await Clients.Client(this.Context.ConnectionId).InvokeAsync("getRoomlist",(JsonConvert.SerializeObject(itme.ToList())));
        }


        /// <summary>
        /// 更新所有用户的房间列表
        /// </summary>
        private async Task GetRoomList()
        {
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
            var obj = new
            {
                RoomName = room,
                Content = message,
                SendTime = DateTime.Now.ToString()
            };
            Clients.Group(room).InvokeAsync("SendMessage", obj);
        }

        /// <summary>
        /// 加入聊天室
        /// </summary>
        /// <param name="roomName"></param>
        public async Task AddToRoom(string roomName)
        {
            //查询聊天室
            var room = Db.Rooms.Find(a => a.RoomName == roomName);
            //存在则加入
            if (room != null)
            {
                //查找房间中是否存在此用户
                var isuser = room.Users.FirstOrDefault(a => a.UserName == Context.ConnectionId);
                //不存在则加入
                if (isuser == null)
                {
                    var user = Db.Users.Find(a => a.UserName == Context.ConnectionId);
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


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = Db.Users.FirstOrDefault(u => u.UserName == Context.ConnectionId);

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

        ///// <summary>
        ///// 加入聊天室
        ///// </summary>
        ///// <param name="roomName"></param>
        //public  async Task AddToRoom(string roomName)
        //{
        //    //查询聊天室
        //    var room = Db.Rooms.Find(a => a.RoomName == roomName);
        //    //存在则加入
        //    if (room != null)
        //    {
        //        //查找房间中是否存在此用户
        //        var isuser = room.Users.FirstOrDefault(a => a.UserName == Context.ConnectionId);
        //        //不存在则加入
        //        if (isuser == null)
        //        {
        //            var user = Db.Users.Find(a => a.UserName == Context.ConnectionId);
        //            user.Rooms.Add(room);
        //            room.Users.Add(user);
        //            await Groups.AddAsync(Context.ConnectionId, roomName);
        //            //调用此连接用户的本地JS(显示房间)
        //           await Clients.Client(Context.ConnectionId).InvokeAsync("AddRoom",roomName);
        //        }
        //        else
        //        {
        //            await Clients.Client(Context.ConnectionId).InvokeAsync("showMessage",roomName);
        //        }
        //    }
        //}

        /// <summary>
        /// 退出聊天室
        /// </summary>
        /// <param name="roomName"></param>
        public async Task RemoveFromRoom(string roomName)
        {
            //查找房间是否存在
            var room = Db.Rooms.Find(a => a.RoomName == roomName);
            //存在则进入删除
            if (room != null)
            {
                //查找要删除的用户
                var user = room.Users.FirstOrDefault(a => a.UserName == Context.ConnectionId);
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
