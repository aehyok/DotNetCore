using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Users.SignalR
{
    public class UserContext
    {
        public UserContext()
        {
            Users = new List<User>();
            Rooms = new List<ConversationRoom>();
        }
        //用户集合
        public List<User> Users { get; set; }
        //房间集合
        public List<ConversationRoom> Rooms { get; set; }


        /// <summary>
        /// 房间类
        /// </summary>
        public class ConversationRoom
        {
            //房间名称
            [Key]
            public string RoomName { get; set; }
            //用户集合
            public virtual List<User> Users { get; set; }
            public ConversationRoom()
            {
                Users = new List<User>();
            }
        }

        public class User
        {
            [Key]
            //用户名
            public string UserName { get; set; }
            //用户房间集合
            public virtual List<ConversationRoom> Rooms { get; set; }
            public User()
            {
                Rooms = new List<ConversationRoom>();
            }
        }
    }
}
